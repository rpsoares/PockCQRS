using CRQS.SharedCore.Exception;
using CRQSCommon.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace WriteModel.Bus
{
    public class ServiceLocator : IServiceLocator
    {
        public Type[] GetHandlers(Type[] interfaces)
        {
            DirectoryInfo Dir = new DirectoryInfo("C:\\Projetos\\PockFTS\\PockFTS\\bin\\Debug\\");
            FileInfo[] Files = Dir.GetFiles("*.dll", SearchOption.AllDirectories);

            IEnumerable<Type> handlers = new List<Type>();

            foreach (FileInfo File in Files)
            {
                var tmp = Assembly.LoadFrom(File.FullName).GetTypes()
                .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && interfaces.Contains(y.GetGenericTypeDefinition()))).ToArray();

                if (tmp != null)
                {
                    handlers=handlers.Union(tmp.ToList());
                }

            }

            return handlers.ToArray();

        }

        public object GetService(Type type)
        {
            try
            {
                return Activator.CreateInstance(type);
            }
            catch (MissingMethodException)
            {
                throw new MissingParameterLessConstructorException(type);
            }
        }


    }
}
