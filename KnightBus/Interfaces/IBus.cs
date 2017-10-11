using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KnightBus.Interfaces
{
    public interface IBus
    {
        Task SendAsync(ICommand message);
        Task PublishAsync(IEvent message);
        void Subscribe<T>(Func<T, Task> handler) where T : class, IMessage;
    }
}
