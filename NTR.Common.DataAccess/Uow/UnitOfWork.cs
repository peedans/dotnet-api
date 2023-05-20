using System;
using NTR.Common.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace NTR.Common.DataAccess.Uow
{
    public class UnitOfWork : UnitOfWorkBase<DbContext>, IUnitOfWork
    {
        public UnitOfWork(DbContext context, IServiceProvider provider) : base(context, provider)
        { }
    }
}
