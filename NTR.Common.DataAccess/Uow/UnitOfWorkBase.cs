using System.Diagnostics;
using System.Reflection;
using NTR.Common.DataAccess.Entities;
using NTR.Common.DataAccess.Exceptions;
using NTR.Common.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace NTR.Common.DataAccess.Uow
{
    public abstract class UnitOfWorkBase<TContext> : IUnitOfWorkBase where TContext : DbContext
    {
        private List<Action<ChangeTracker>> OnCommitActions;
        private List<Action<UowLogInfo>> OnCommitActionLogs;
        protected internal UnitOfWorkBase(TContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        protected TContext _context;
        protected readonly IServiceProvider _serviceProvider;

        public int SaveChanges()
        {
            CheckDisposed();
            var tracker = _context.ChangeTracker;//.DeepClone();
            var iSaveChanged= _context.SaveChanges();

            if (!ReferenceEquals(null, OnCommitActions))
            {
                foreach (var onCommitAction in OnCommitActions)
                    onCommitAction.Invoke(tracker);
                OnCommitActions.Clear();
            }

            if (!ReferenceEquals(null, OnCommitActionLogs))
            {
                var logInfo = (new StackTrace()).GetFrame(1).GetMethod().GetCustomAttribute<UowLogInfo>();
                if (ReferenceEquals(logInfo, null))
                {
                    OnCommitActionLogs.Clear();
                    return iSaveChanged;
                }

                foreach (var onCommitAction in OnCommitActionLogs)
                    onCommitAction.Invoke(logInfo);
                OnCommitActionLogs.Clear();
            }

            return iSaveChanged;
        }

        public Task<int> SaveChangesAsync()
        {
            CheckDisposed();
            var tracker = _context.ChangeTracker;
            var iSaveChanged = _context.SaveChangesAsync();

            if (!ReferenceEquals(null, OnCommitActions))
            {
                foreach (var onCommitAction in OnCommitActions)
                    onCommitAction.Invoke(tracker);
                OnCommitActions.Clear();
            }

            if (!ReferenceEquals(null, OnCommitActionLogs))
            {
                var logInfo = (new StackTrace()).GetFrame(1).GetMethod().GetCustomAttribute<UowLogInfo>();
                if (ReferenceEquals(logInfo, null))
                {
                    OnCommitActionLogs.Clear();
                    return iSaveChanged;
                }

                foreach (var onCommitAction in OnCommitActionLogs)
                    onCommitAction.Invoke(logInfo);
                OnCommitActionLogs.Clear();
            }

            return iSaveChanged;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            CheckDisposed();
            var tracker = _context.ChangeTracker;
            var iSaveChanged= _context.SaveChangesAsync(cancellationToken);

            if (!ReferenceEquals(null, OnCommitActions))
            {
                foreach (var onCommitAction in OnCommitActions)
                    onCommitAction.Invoke(tracker);
                OnCommitActions.Clear();
            }

            if (!ReferenceEquals(null, OnCommitActionLogs))
            {
                var logInfo = (new StackTrace()).GetFrame(1).GetMethod().GetCustomAttribute<UowLogInfo>();
                if (ReferenceEquals(logInfo, null))
                {
                    OnCommitActionLogs.Clear();
                    return iSaveChanged;
                }

                foreach (var onCommitAction in OnCommitActionLogs)
                    onCommitAction.Invoke(logInfo);
                OnCommitActionLogs.Clear();
            }

            return iSaveChanged;
        }

        public DbContext GetContext<TEntity>()
        {
            return _context;
        }

        public void OnCompleteTransaction(Action<ChangeTracker> func)
        {
            if (ReferenceEquals(OnCommitActions, null))
                OnCommitActions = new List<Action<ChangeTracker>>();

            OnCommitActions.Add(func);
        }

        public void OnCompleteTransactionAndLog(Action<UowLogInfo> func)
        {
            if (ReferenceEquals(OnCommitActionLogs, null))
                OnCommitActionLogs = new List<Action<UowLogInfo>>();

            OnCommitActionLogs.Add(func);
        }

        public IRepository<TEntity> GetRepository<TEntity>()
        {
            CheckDisposed();
            var repositoryType = typeof(IRepository<TEntity>);
            var repository = (IRepository<TEntity>)_serviceProvider.GetService(repositoryType);
            if (repository == null)
            {
                throw new RepositoryNotFoundException(repositoryType.Name, String.Format("Repository {0} not found in the IOC container. Check if it is registered during startup.", repositoryType.Name));
            }

            ((IRepositoryInjection)repository).SetContext(_context);
            return repository;
        }

        public TRepository GetCustomRepository<TRepository>()
        {
            CheckDisposed();
            var repositoryType = typeof(TRepository);
            var repository = (TRepository)_serviceProvider.GetService(repositoryType);
            if (repository == null)
            {
                throw new RepositoryNotFoundException(repositoryType.Name, String.Format("Repository {0} not found in the IOC container. Check if it is registered during startup.", repositoryType.Name));
            }

            ((IRepositoryInjection)repository).SetContext(_context);
            return repository;
        }

        #region IDisposable Implementation

        protected bool _isDisposed;

        protected void CheckDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException("The UnitOfWork is already disposed and cannot be used anymore.");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (_context != null)
                    {
                        _context.Dispose();
                        _context = null;
                        // TODO Check with server
                        // SqlConnection.ClearAllPools();
                    }
                }
            }
            _isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void CompleteTransaction()
        {
            //throw new NotImplementedException();
        }
        ~UnitOfWorkBase()
        {
            Dispose(false);
        }

        #endregion
    }
}