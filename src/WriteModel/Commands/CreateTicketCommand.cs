using CRQSCommon.Commands;

namespace WriteModel.Commands
{
    public class CreateTicketCommand: ICommand
    {
        public string TradeType { get; set; }
        public int Party { get; set; }
        public int CounterParty { get; set; }
    }
}
