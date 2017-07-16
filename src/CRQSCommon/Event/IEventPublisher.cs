namespace CRQSCommon.Event
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : IEvent;
    }
}
