using Microsoft.EntityFrameworkCore;
using UserPermission.Domain.Interface.Repository;

namespace UserPermission.Repository
{
    public partial class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly UserPermissionContext _dbContext;
        private DbSet<T> _dbSet => (DbSet<T>)_dbContext.Set<T>();
        public IQueryable<T> Entities => _dbSet;
        public GenericRepository(UserPermissionContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }
    }
}
