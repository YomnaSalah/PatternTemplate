using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PatternTemplate.DTO;

namespace PatternTemplate.DAL
{
    /// <summary>
    /// <see cref="IUnitOfWork"/> is used to manage transaction , commit , rollback ,save changes
    /// on operations
    /// </summary>
    public interface IUnitOfWork<T> : IDisposable where T : DbContext
    {
        #region Props
        public T PatternTemplateDbContext { get; }
        IDbContextTransaction DbContextTransaction { get; set; }
        public bool IsDisposed { get; }
        #endregion

        #region Methods
        bool SaveChanges();
        SaveChangeDto SaveChangesWithMessage();
        Task<bool> SaveChangesAsync();
        void Commit();
        public Task RollbackAsync(CancellationToken cancellationToken);
        public bool HasChanges();
        #endregion
    }
}
