using System;
using NTR.Common.DataAccess.Exceptions;
using Xunit;

namespace NTR.Common.DataAccess.UnitTest.Exceptions.EntityNotFoundExceptionTests
{
    public class InstantiationTests
    {
        [Fact]
        private void EntityNameIsSet()
        {
            var ex = new EntityNotFoundException("entity", 123);
            Assert.Equal("entity", ex.EntityName);
        }

        [Fact]
        private void EntityKeyIsSet()
        {
            var ex = new EntityNotFoundException("entity", 123);
            Assert.Equal(123, ex.EntityKey);
        }
    }
}
