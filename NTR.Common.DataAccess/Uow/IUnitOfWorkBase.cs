using NTR.Common.DataAccess.Entities;
using NTR.Common.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace NTR.Common.DataAccess.Uow
{
    public interface IUnitOfWorkBase : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        IRepository<TEntity> GetRepository<TEntity>();
        TRepository GetCustomRepository<TRepository>();

        void OnCompleteTransaction(Action<ChangeTracker> func);
        void OnCompleteTransactionAndLog(Action<UowLogInfo> func);
    }
}