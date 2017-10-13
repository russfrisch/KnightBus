using KnightBus.DependencyInjection;
using KnightBus.Logging;
using KnightBus.Persistence;
using KnightBus.Serialization;
using KnightBus.Transport;
using System;
using Xunit;

namespace KnightBus.Tests
{
    public class BusConfigurationTests
    {
        [Fact]
        public void TestDefaults()
        {
            IBus bus = Bus.Create().Start();
        }

        [Fact]
        public void TestSpecifyDefaults()
        {
            IBus bus = Bus.Create()
                .WithLogger<MSExtensionsLogging>()
                .WithDependencyInjector<MSExtensionsDependencyInjection>()
                .WithSerializer<JSONSerializer>()
                .WithTransport<InMemoryTransport>()
                .WithPersistence<InMemoryPersistence>()
                .Start();
        }
    }
}
