using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KnightBus.Messaging
{
    public interface IHandleMessages<T>
    {
        Task Handle(T message);
    }
}
