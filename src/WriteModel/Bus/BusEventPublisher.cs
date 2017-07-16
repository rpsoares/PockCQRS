﻿using CRQS.SharedCore.Exception;
using CRQSCommon.Bus;
using CRQSCommon.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WriteModel.Bus
{
    public class BusEventPublisher : IEventPublisher
    {
        private readonly IBusEventHandler[] _handlers;
        private Dictionary<Type, MethodInfo> methodLookups =
            new Dictionary<Type, MethodInfo>();

        public BusEventPublisher(IBusEventHandler[] handlers)
        {
            _handlers = handlers;

            foreach (var handler in _handlers)
            {
                var meth = (from m in handler.GetType()
                        .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                            let prms = m.GetParameters()
                            where prms.Count() == 1 && m.Name.Contains("Handle")
                            select new
                            {
                                EventType = prms.First().ParameterType,
                                Method = m
                            }).FirstOrDefault();
                if (meth != null)
                {
                    methodLookups.Add(meth.EventType, meth.Method);
                }

            }

        }

        public void Publish<T>(T @event) where T : IEvent
        {

            var theHandler = _handlers.SingleOrDefault(
                x => x.HandlerType == @event.GetType());

            if (theHandler == null)
                throw new BusinessLogicException(
                    string.Format("Handler for {0} could not be found",
                    @event.GetType().Name));

            Task.Run(() =>
            {
                methodLookups[@event.GetType()].Invoke(
                    theHandler, new[] { (object)@event });
            }).Wait();

        }
    }
}