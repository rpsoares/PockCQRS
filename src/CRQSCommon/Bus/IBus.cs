using CRQSCommon.Commands;
using CRQSCommon.Event;

namespace CRQSCommon.Bus
{
    public interface IBus : ICommandSender, IEventPublisher, IHandlerRegistrar
    {
    }
}
