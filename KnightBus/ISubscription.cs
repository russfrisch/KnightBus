using KnightBus.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KnightBus
{
    internal interface ISubscription
    {
        Task NotifyAsync(IMessage message);
    }
}
