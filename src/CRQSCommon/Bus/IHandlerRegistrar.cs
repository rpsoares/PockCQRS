using CRQSCommon.Messages;
using System;

namespace CRQSCommon.Bus
{
    public interface IHandlerRegistrar
    {
        void RegisterHandler<T>(Action<T> handler) where T : IMessage;
    }
}
