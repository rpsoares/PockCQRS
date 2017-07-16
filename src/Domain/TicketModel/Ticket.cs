using CRQSCommon.Domain;
using Domain.Events;
using System;

namespace Domain.TicketModel
{
    public class Ticket : AggregateRoot
    {
        private ulong TicketId { get; set; }
        private int Version { get; set; }
        private string TradeType { get; set; }
        private int Party { get; set; }
        private int CounterParty { get; set; }


        public Ticket()
        {

            Id = Guid.NewGuid();
            Version = 0;
        }

        public void Save( string tradeType, int party, int counterParty)
        {
            TicketId = 1;
             Version = Version +1;
            TradeType = tradeType;
            Party = party;
            CounterParty = counterParty;

            ApplyChange(new CreateTradeEvent(TicketId, Version));
        }

        public void Release()
        {
            ApplyChange(new ReleaseEvent(TicketId, Version));
        }
    }
}
