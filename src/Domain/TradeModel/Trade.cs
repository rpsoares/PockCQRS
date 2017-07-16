namespace Domain.TradeModel
{
    public class Trade
    {
        private ulong Id { get; set; }
        private int Version { get; set; }
        private string TradeType { get; set; }
        private int Party { get; set; }
        private int CounterParty { get; set; }

        public ulong TicketId { get; set; }

        public Trade (ulong id,int version,string tradeType,int party, int counterParty, ulong ticketId)
        {
            Id = id;
            Version = version;
            TradeType = tradeType;
            Party = party;
            CounterParty = counterParty;
            TicketId = ticketId;
            
        }



    }
}
