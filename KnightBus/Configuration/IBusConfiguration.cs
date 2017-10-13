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
    public interface IBusConfiguration
    {
        BusConfiguration WithLogger<T>() where T : ILogAdapter, new();
        BusConfiguration WithDependencyInjector<T>() where T : IDependencyInjectionAdapter, new();
        BusConfiguration WithSerializer<T>() where T : ISerializationAdapter, new();
        BusConfiguration WithTransport<T>() where T : ITransportAdapter, new();
        BusConfiguration WithPersistence<T>() where T : IPersistenceAdapter, new();
    }
}
