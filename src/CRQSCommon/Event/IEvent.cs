using CRQSCommon.Messages;
using System;

namespace CRQSCommon.Event
{
    public interface IEvent : IMessage
    {
        Guid Id { get; set; }
        DateTimeOffset TimeStamp { get; set; }
    }
}
