using CRQSCommon.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRQSCommon.Commands
{
    public interface ICommandHandler<T> : IHandler<T> where T : ICommand
    {
    }
}
