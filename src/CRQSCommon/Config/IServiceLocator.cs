using System;

namespace CRQSCommon.Config
{
    public interface IServiceLocator
    {
        Type[] GetHandlers(Type[] interfaces);
        object GetService(Type type);
    }
}
