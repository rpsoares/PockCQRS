using CRQSCommon.Bus;
using CRQSCommon.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CRQSCommon.Event;

namespace CRQSCommon.Config
{
    public class RegisterBus
    {
        private readonly IServiceLocator _serviceLocator;
        private readonly IBus _bus;

        public RegisterBus(IBus bus, IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator ?? throw new ArgumentNullException("serviceLocator");
            _bus = bus ?? throw new ArgumentNullException("bus");
        }

        public void Register()
        {

            var handlers = _serviceLocator.GetHandlers(new Type[2]
                {
                    typeof(ICommandHandler<>),
                    typeof(IEventHandler<>)
             } );

            foreach (var typesFromAssemblyContainingMessage in handlers)
            {
                var t = ResolveMessageHandlerInterface(typesFromAssemblyContainingMessage);

                foreach (var @interface in t)
                      InvokeHandler(@interface, _bus, typesFromAssemblyContainingMessage);

            }
        }

        private void InvokeHandler(Type @interface, IHandlerRegistrar bus, Type executorType)
        {
            var commandType = @interface.GetGenericArguments()[0];

            var registerExecutorMethod = bus
                .GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .Where(mi => mi.Name == "RegisterHandler")
                .Where(mi => mi.IsGenericMethod)
                .Where(mi => mi.GetGenericArguments().Count() == 1)
                .Single(mi => mi.GetParameters().Count() == 1)
                .MakeGenericMethod(commandType);

            var del = new Action<dynamic>(x =>
            {
                dynamic handler = _serviceLocator.GetService(executorType);
                handler.Handle(x);
            });

            registerExecutorMethod.Invoke(bus, new object[] { del });
        }

        private static IEnumerable<Type> ResolveMessageHandlerInterface(Type type)
        {
            return type
                .GetInterfaces()
                .Where(i => i.IsGenericType && ((i.GetGenericTypeDefinition() == typeof(ICommandHandler<>))
                                                 || i.GetGenericTypeDefinition() == typeof(IEventHandler<>)));
        }
    }
}
