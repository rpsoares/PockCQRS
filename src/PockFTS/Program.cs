using CRQSCommon.Bus;
using CRQSCommon.Config;
using WriteModel.Bus;
using WriteModel.Commands;

namespace PockFTS
{
    class Program
    {
        static void Main(string[] args)
        {

            IBus bus =  InProcessBus.CreateInstance();
            var reg = new RegisterBus(bus, new ServiceLocator());
            reg.Register();

            var command = new CreateTicketCommand()
            {
                CounterParty = 1,
                Party=1,
                TradeType="Trade"


                
            };

            bus.Send(command);

         

        }
    }
}
