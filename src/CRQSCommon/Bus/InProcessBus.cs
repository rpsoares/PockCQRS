using CRQSCommon.Commands;
using CRQSCommon.Event;
using CRQSCommon.Infrastructure;
using CRQSCommon.Messages;
using System;
using System.Collections.Generic;

namespace CRQSCommon.Bus
{
    public class InProcessBus : IBus
    {
        private readonly Dictionary<Type, List<Action<IMessage>>> _routes = new Dictionary<Type, List<Action<IMessage>>>();


        private InProcessBus() { }

        private static IBus _singleton;

        public static IBus CreateInstance()
        {
            if (_singleton == null) _singleton = new InProcessBus();

            return _singleton;
        }




        public void RegisterHandler<T>(Action<T> handler) where T : IMessage
        {
            List<Action<IMessage>> handlers;
            if (!_routes.TryGetValue(typeof(T), out handlers))
            {
                handlers = new List<Action<IMessage>>();
                _routes.Add(typeof(T), handlers);
            }
            handlers.Add(DelegateAdjuster.CastArgument<IMessage, T>(x => handler(x)));
        }

        public void Send<T>(T command) where T : ICommand
        {
            List<Action<IMessage>> handlers;
            if (_routes.TryGetValue(typeof(T), out handlers))
            {
                if (handlers.Count != 1) throw new InvalidOperationException("Cannot send to more than one handler");
                handlers[0](command);
            }
            else
            {
                throw new InvalidOperationException("No handler registered");
            }
        }

        public void Publish<T>(T @event) where T : IEvent
        {
            List<Action<IMessage>> handlers;
            if (!_routes.TryGetValue(@event.GetType(), out handlers)) return;
            foreach (var handler in handlers)
                handler(@event);

        }
    }
}
