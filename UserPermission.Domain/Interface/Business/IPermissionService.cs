using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserPermission.Domain.Interface.General;

namespace UserPermission.Domain.Interface.Business
{
    public interface IPermissionService<T>: IRemove<T>, IAdd<T>, ISelect<T> where T : class
    {
    }
}
