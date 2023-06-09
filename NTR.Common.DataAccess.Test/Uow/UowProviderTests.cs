﻿using Castle.Core.Logging;
using NTR.Common.DataAccess;
using NTR.Common.DataAccess.Context;
using NTR.Common.DataAccess.Repositories;
using NTR.Common.DataAccess.UnitTest._TestObjects;
using NTR.Common.DataAccess.Uow;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NTR.Common.DataAccess.UnitTest.Uow
{
    public class UowProviderTests
    {
        [Fact]
        public void TestGetCustomRepository()
        {

            var logger = new Moq.Mock<ILogger<DataAccess>>();
            var sp = new Moq.Mock<IServiceProvider>();

            var myContext = new InMemoryContext(new Microsoft.EntityFrameworkCore.DbContextOptions<InMemoryContext>());

            sp.Setup((o) => o.GetService(typeof(IEntityContext))).Returns(myContext);
            sp.Setup((o) => o.GetService(typeof(IFooRepository))).Returns(new FooRepository(logger.Object, myContext));

            var provider = new UowProvider(logger.Object, sp.Object);

            var uow = provider.CreateUnitOfWork();

            uow.GetCustomRepository<IFooRepository>();
        }

        [Fact]
        public void TestGetGenericRepository()
        {

            var logger = new Moq.Mock<ILogger<DataAccess>>();
            var sp = new Moq.Mock<IServiceProvider>();

            var myContext = new InMemoryContext(new Microsoft.EntityFrameworkCore.DbContextOptions<InMemoryContext>());

            sp.Setup((o) => o.GetService(typeof(IEntityContext))).Returns(myContext);
            sp.Setup((o) => o.GetService(typeof(IRepository<Foo>))).Returns(new GenericEntityRepository<Foo>(logger.Object));

            var provider = new UowProvider(logger.Object, sp.Object);

            var uow = provider.CreateUnitOfWork();

            uow.GetRepository<Foo>();
        }

    }
}
