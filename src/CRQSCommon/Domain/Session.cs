using CRQS.SharedCore.Exception;
using System;
using System.Collections.Generic;

namespace CRQSCommon.Domain
{
    public class Session : ISession
    {
        private readonly IRepository _repository;
        private readonly Dictionary<Guid, AggregateDescriptor> _trackedAggregates;

        public Session(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException("repository");
            _trackedAggregates = new Dictionary<Guid, AggregateDescriptor>();
        }

        public void Add<T>(T aggregate) where T : AggregateRoot
        {
            if (!IsTracked(aggregate.Id))
                _trackedAggregates.Add(aggregate.Id,
                    new AggregateDescriptor
                    {
                        Aggregate = aggregate,
                    });
            else if (_trackedAggregates[aggregate.Id].Aggregate != aggregate)
                throw new ConcurrencyException(aggregate.Id);
        }

        public T Get<T>(Guid id, int? expectedVersion = null) where T : AggregateRoot
        {
            throw new NotImplementedException();
        }

        private bool IsTracked(Guid id)
        {
            return _trackedAggregates.ContainsKey(id);
        }

        public void Commit()
        {
            foreach (var descriptor in _trackedAggregates.Values)
            {
                _repository.Save(descriptor.Aggregate);
            }
            _trackedAggregates.Clear();
        }

        private class AggregateDescriptor
        {
            public AggregateRoot Aggregate { get; set; }
            public int Version { get; set; }
        }
    }
}
