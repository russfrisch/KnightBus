using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KnightBus.Interfaces
{
    internal interface ISubscription
    {
        Task NotifyAsync(IMessage message);
    }
}
