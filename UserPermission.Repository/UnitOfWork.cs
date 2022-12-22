using Microsoft.EntityFrameworkCore;
using UserPermission.Domain;
using UserPermission.Domain.Interface.Repository;

namespace UserPermission.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserPermissionContext _dbContext;
        #region Repositories
        public IRepository<Permission> PermissionRepository => new GenericRepository<Permission>(_dbContext);
        public IRepository<PermissionType> PermissionTypeRepository => new GenericRepository<PermissionType>(_dbContext);
        #endregion
        public UnitOfWork(UserPermissionContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Commit()
        {
            _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
        public void RejectChanges()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries()
                  .Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }
    }
}
