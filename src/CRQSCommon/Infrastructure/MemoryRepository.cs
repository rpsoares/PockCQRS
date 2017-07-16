using CRQSCommon.Domain;
using CRQSCommon.Event;
using System;

namespace CRQSCommon.Infrastructure
{
    public class MemoryRepository : IRepository
    {
        private readonly IEventStore _eventStore;
        private readonly IEventPublisher _publisher;

        public MemoryRepository(IEventStore eventStore, IEventPublisher publisher)
        {
            _eventStore = eventStore ?? throw new ArgumentNullException("eventStore");
            _publisher = publisher ?? throw new ArgumentNullException("publisher");
        }
        public T Get<T>(Guid aggregateId) where T : AggregateRoot
        {
            throw new NotImplementedException();
        }

        public void Save<T>(T aggregate) where T : AggregateRoot
        {
            var i = 0;
            foreach (var @event in aggregate.GetUncommittedChanges())
            {
                i++;
                @event.Id = Guid.NewGuid();
                @event.TimeStamp = DateTimeOffset.UtcNow;
                _eventStore.Save(@event);
                _publisher.Publish(@event);
            }
            aggregate.MarkChangesAsCommitted();
        }
    }
}
