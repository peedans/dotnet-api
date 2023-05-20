using NTR.Common.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NTR.Common.DataAccess.Entities;

namespace NTR.Common.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        IRepository<TEntity> GetRepository<TEntity>();
        TRepository GetCustomRepository<TRepository>();

        DbContext GetContext<TRepository>();

        void OnCompleteTransaction(Action<ChangeTracker> func);
        void OnCompleteTransactionAndLog(Action<UowLogInfo> func);
    }
}
