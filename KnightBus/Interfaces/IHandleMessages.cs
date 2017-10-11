using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KnightBus.Interfaces
{
    public interface IHandleMessages<T>
    {
        Task Handle(T message);
    }
}
