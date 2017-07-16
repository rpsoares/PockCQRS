namespace Domain.Events
{
    public class CreateTradeEvent : EventBase
    {

        public CreateTradeEvent(ulong ticketId, int ticketVersion)
        {
            TicketId = ticketId;
            TicketVersion = ticketVersion;
        }

        private ulong TicketId;
        private int TicketVersion;
    }
}
