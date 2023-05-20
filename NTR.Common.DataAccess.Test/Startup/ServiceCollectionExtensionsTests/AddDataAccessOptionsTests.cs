using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NTR.Common.DataAccess.Context;
using NTR.Common.DataAccess.Options;
using NTR.Common.DataAccess.Paging;
using NTR.Common.DataAccess.Repositories;
using NTR.Common.DataAccess.Uow;
using Xunit;
using NTR.Common.DataAccess;

namespace NTR.Common.DataAccess.UnitTest.Startup.ServiceCollectionExtensionsTests
{
    public class AddDataAccessOptionsTests
    {
        [Fact]
        private void UowProviderIsRegisteredAsScoped()
        {
            var services = new ServiceCollection();

            services.AddDataAccess<TestContext>();

            var registrations = services.Where(sd => sd.ServiceType == typeof(IUowProvider)
                                               && sd.ImplementationType == typeof(UowProvider))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Scoped, registrations[0].Lifetime);
        }

        [Fact]
        private void GenericRepositoryIsRegisteredAsTransient()
        {
            var services = new ServiceCollection();

            services.AddDataAccess<TestContext>();

            var registrations = services.Where(sd => sd.ServiceType == typeof(IRepository<>)
                                               && sd.ImplementationType == typeof(GenericEntityRepository<>))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Transient, registrations[0].Lifetime);
        }

        [Fact]
        private void DataPagerIsRegisteredAsTransient()
        {
            var services = new ServiceCollection();

            services.AddDataAccess<TestContext>();

            var registrations = services.Where(sd => sd.ServiceType == typeof(IDataPager<>)
                                               && sd.ImplementationType == typeof(DataPager<>))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
            Assert.Equal(ServiceLifetime.Transient, registrations[0].Lifetime);
        }
    }
}
