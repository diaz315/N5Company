using UserPermission.Domain.Interface.General;

namespace UserPermission.Domain.Interface.Repository
{
    public interface IRepository<T>:IRemove<T>, IAdd<T>, IEntities<T> where T : class
    {
    }
}
