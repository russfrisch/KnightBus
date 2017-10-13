using KnightBus.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KnightBus
{
    internal class Subscription<T> : ISubscription where T : class, IMessage
    {
        private readonly Func<T, Task> _action;

        public Subscription(Func<T, Task> action)
        {
            _action = action;
        }

        public async Task NotifyAsync(IMessage message)
        {
            await Task.Run(() => _action(message as T));
        }
    }
}
