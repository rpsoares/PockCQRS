using CRQSCommon.Event;
using CRQSCommon.Infrastructure;
using System;
using System.Collections.Generic;

namespace CRQSCommon.Domain
{
    public abstract class AggregateRoot
    {
        private readonly List<IEvent> _changes = new List<IEvent>();

        public Guid Id { get; protected set; }
        //public int Version { get; protected set; }

        public IEnumerable<IEvent> GetUncommittedChanges()
        {
            lock (_changes)
            {
                return _changes.ToArray();
            }
        }

        public void MarkChangesAsCommitted()
        {
            lock (_changes)
            {
                //Version = Version + _changes.Count;
                _changes.Clear();
            }
        }

        protected void ApplyChange(IEvent @event)
        {
            lock (_changes)
            {
                _changes.Add(@event);

            }
        }


    }
}
