using KnightBus.Configuration;
using KnightBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightBus
{

    public class Bus : IBus
    {
        private IEnumerable<ICommand> Commands { get; set; }
        private IEnumerable<IHandleMessages<ICommand>> CommandHandlers { get; set; }
        private Dictionary<Type, IList<ISubscription>> _subscriptions { get; set; }

        internal Bus(IBusConfiguration busConfiguration)
        {
            _subscriptions = new Dictionary<Type, IList<ISubscription>>();
            //Console.WriteLine("Initialzing Service Bus...");

            //Console.WriteLine("Loading Commands...");
            //Commands = FindCommands();
            //Console.WriteLine($"Found {Commands.Count()} Commands.");

            //Console.WriteLine("Loading Command Handlers...");
            //CommandHandlers = FindCommandHandlers();
            //Console.WriteLine($"Found {CommandHandlers.Count()} Command Handlers.");
        }
        internal IBus Start()
        {
            throw new NotImplementedException();
        }

        public void Subscribe<T>(Func<T, Task> handler) where T : class, IMessage
        {
            if (!_subscriptions.ContainsKey(typeof(T)) || typeof(T) is ICommand)
                _subscriptions.Add(typeof(T), new List<ISubscription>());

            _subscriptions[typeof(T)].Add(new Subscription<T>(handler));

            Console.WriteLine($"Subscribed {typeof(T).Name} with handler {handler.GetType().Name}.");
        }

        public async Task PublishAsync(IEvent message)
        {
            Console.WriteLine($"Publishing {message.GetType().Name}.");

            var subscriptions = _subscriptions[message.GetType()];
            await Task.WhenAll(subscriptions.Select(s => s.NotifyAsync(message)));
        }

        public async Task SendAsync(ICommand message)
        {
            Console.WriteLine($"Sending {message.GetType().Name}.");

            var subscription = _subscriptions[message.GetType()].FirstOrDefault();
            await subscription.NotifyAsync(message);
        }

        public static BusConfiguration Create()
        {
            return new BusConfiguration();
        }

        private IEnumerable<ICommand> FindCommands()
        {
            return from assembly in AppDomain.CurrentDomain.GetAssemblies()
                            from t in assembly.GetTypes()
                            where t.GetInterfaces().Contains(typeof(ICommand))
                            select t as ICommand;
        }

        private IEnumerable<IHandleMessages<ICommand>> FindCommandHandlers()
        {
            return from assembly in AppDomain.CurrentDomain.GetAssemblies()
                            from t in assembly.GetTypes()
                            where t.GetInterfaces().Contains(typeof(IHandleMessages<ICommand>))
                            select t as IHandleMessages<ICommand>;
        }

        

        Task IBus.SendAsync(ICommand message)
        {
            throw new NotImplementedException();
        }

        Task IBus.PublishAsync(IEvent message)
        {
            throw new NotImplementedException();
        }

        void IBus.Subscribe<T>(Func<T, Task> handler)
        {
            throw new NotImplementedException();
        }
    }
}
