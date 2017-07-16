using CRQSCommon.Bus;
using CRQSCommon.Commands;
using CRQSCommon.Domain;
using CRQSCommon.Infrastructure;
using Domain.TicketModel;
using WriteModel.Infrastructure.Event;

namespace WriteModel.Commands
{
    public class SaveTicketHandler : ICommandHandler<CreateTicketCommand>
    {
        private readonly ISession _session;

        public SaveTicketHandler()
        {
            _session = new Session(new MemoryRepository(new InMemoryEventStore(), InProcessBus.CreateInstance()));
        }
        public void Handle(CreateTicketCommand message)
        {
            var ticket = new Ticket();
            ticket.Save(message.TradeType, message.Party, message.CounterParty);
            _session.Add(ticket);


            _session.Commit();
        }
    }
}
