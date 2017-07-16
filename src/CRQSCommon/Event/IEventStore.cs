using System;
using System.Collections.Generic;

namespace CRQSCommon.Event
{
    public interface IEventStore
    {
        void Save(IEvent @event);
        IEnumerable<IEvent> Get(Guid aggregateId);
    }
}
