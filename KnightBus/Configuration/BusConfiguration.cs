using KnightBus.DependencyInjection;
using KnightBus.Logging;
using KnightBus.Persistence;
using KnightBus.Serialization;
using KnightBus.Transport;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnightBus.Configuration
{
    public class BusConfiguration : IBusConfiguration
    {
        internal ILogAdapter Logger { get; set; }
        internal IDependencyInjectionAdapter DependencyInjector { get; set; }
        internal ISerializationAdapter Serializer { get; set; }
        internal ITransportAdapter Transport { get; set; }
        internal IPersistenceAdapter Persistence { get; set; }
        private IBusConfiguration _busConfiguration;

        public BusConfiguration()
        {
            _busConfiguration = WithLogger<StartupLogger>();
        }

        public IBus Start()
        {
            if (Logger == null || Logger is StartupLogger)
                _busConfiguration = _busConfiguration.WithLogger<MSExtensionsLogging>();
            if (DependencyInjector == null)
                _busConfiguration = _busConfiguration.WithDependencyInjector<MSExtensionsDependencyInjection>();
            if (Serializer == null)
                _busConfiguration = _busConfiguration.WithSerializer<JSONSerializer>();
            if (Transport == null)
                _busConfiguration = _busConfiguration.WithTransport<InMemoryTransport>();
            if (Persistence == null)
                _busConfiguration = _busConfiguration.WithPersistence<InMemoryPersistence>();

            return new Bus(_busConfiguration).Start();
        }

        public BusConfiguration WithLogger<T>() where T : ILogAdapter, new()
        {
            if (Logger != null && Logger is StartupLogger)
            {
                var startupLogger = Logger;
                Logger = new T();
            } else
            {
                Logger = new T();
            }
            return this;
        }

        public BusConfiguration WithDependencyInjector<T>() where T : IDependencyInjectionAdapter, new()
        {
            DependencyInjector = new T();
            return this;
        }

        public BusConfiguration WithSerializer<T>() where T : ISerializationAdapter, new()
        {
            Serializer = new T();
            return this;
        }

        public BusConfiguration WithTransport<T>() where T : ITransportAdapter, new()
        {
            Transport = new T();
            return this;
        }

        public BusConfiguration WithPersistence<T>() where T : IPersistenceAdapter, new()
        {
            Persistence = new T();
            return this;
        }
    }
}
