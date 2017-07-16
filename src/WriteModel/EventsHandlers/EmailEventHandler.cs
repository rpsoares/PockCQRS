using CRQSCommon.Bus;
using CRQSCommon.Event;
using Domain.Events;
using System;

namespace WriteModel.EventsHandlers
{
    public class EmailEventHandler : IEventHandler<CreateTradeEvent>
    {
        public Type HandlerType
        {
            get { return typeof(EmailEventHandler); }
        }

        public void Handle(CreateTradeEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
