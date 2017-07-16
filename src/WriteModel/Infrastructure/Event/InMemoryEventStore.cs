﻿using CRQSCommon.Event;
using System;
using System.Collections.Generic;

namespace WriteModel.Infrastructure.Event
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly Dictionary<Guid, List<IEvent>> _inMemoryDB =
            new Dictionary<Guid, List<IEvent>>();

        public IEnumerable<IEvent> Get(Guid aggregateId)
        {
            List<IEvent> events;
            _inMemoryDB.TryGetValue(aggregateId, out events);
            return events != null
                ? events
                : new List<IEvent>();
        }

        public void Save(IEvent @event)
        {
            List<IEvent> list;
            _inMemoryDB.TryGetValue(@event.Id, out list);
            if (list == null)
            {
                list = new List<IEvent>();
                _inMemoryDB.Add(@event.Id, list);
            }
            list.Add(@event);
        }
    }
}
