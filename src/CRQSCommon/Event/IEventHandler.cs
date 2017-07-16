using CRQSCommon.Messages;

namespace CRQSCommon.Event
{
    public interface IEventHandler<T> : IHandler<T> where T : IEvent
    {
    }
}
