using NTR.Common.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTR.Common.DataAccess.UnitTest
{
    public class TestContext : EntityContextBase<TestContext>
    {
        public TestContext(DbContextOptions<TestContext> options)
            : base(options)
        { }
    }
}
