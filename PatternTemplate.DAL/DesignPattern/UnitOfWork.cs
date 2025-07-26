using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PatternTemplate.DTO;
using PatternTemplate.Tables;

namespace PatternTemplate.DAL
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : DbContext
    {
        private readonly T _dbContext;
        private readonly ILogger<UnitOfWork<T>> _logger;
        private readonly IServiceProvider _serviceProvider;
        private bool _disposed;
        private IDbContextTransaction? _transaction;

        public T PatternTemplateDbContext => _dbContext;
        public IDbContextTransaction DbContextTransaction
        {
            get => _transaction;
            set => _transaction = value;
        }
        public bool IsDisposed => _disposed;

        public UnitOfWork(T dbContext, ILogger<UnitOfWork<T>> logger, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IBaseEntity
        {
            var repository = _serviceProvider.GetService<IRepository<TEntity>>();
            return repository ?? throw new InvalidOperationException($"IRepository<{typeof(TEntity).Name}> is not registered.");
        }

        public async Task<IDbContextTransaction> CreateTransactionAsync(CancellationToken cancellationToken = default)
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            return _transaction;
        }

        public bool SaveChanges()
        {
            try
            {
                return PatternTemplateDbContext.SaveChanges() >= 0;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return false;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return (await PatternTemplateDbContext.SaveChangesAsync()) > 0;
            }
            catch
            {
                return false;
            }
        }

        public SaveChangeDto SaveChangesWithMessage()
        {
            try
            {
                return new SaveChangeDto
                {
                    Status = PatternTemplateDbContext.SaveChanges() >= 0,
                    Message = "Saved Success"
                };
            }
            catch (Exception ex)
            {
                return new SaveChangeDto
                {
                    Status = false,
                    Message = ex.InnerException.Message
                };
            }
        }


        public void Commit()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
            }
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null)
                return;

            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
        }

        public bool HasChanges()
        {
            return _dbContext.ChangeTracker.HasChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _dbContext.Dispose();
                if (_transaction != null)
                {
                    _transaction.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
