using System;

namespace CRQSCommon.Bus
{
    public interface IBusEventHandler
    {
        Type HandlerType { get; }
    }

    public interface IBusEventHandler<T> : IBusEventHandler
    {

        void Handle(T @event);
    }
}
