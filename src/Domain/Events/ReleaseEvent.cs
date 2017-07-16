namespace Domain.Events
{
    public class ReleaseEvent : EventBase
    {

        public ReleaseEvent(ulong ticketId, int ticketVersion)
        {
            TicketId = ticketId;
            TicketVersion = ticketVersion;
        }

        private ulong TicketId;
        private int TicketVersion;
    }
}
