using System;

namespace CRQSCommon.Domain
{
    public interface IRepository
    {
        void Save<T>(T aggregate) where T : AggregateRoot;
        T Get<T>(Guid aggregateId) where T : AggregateRoot;
    }
}
