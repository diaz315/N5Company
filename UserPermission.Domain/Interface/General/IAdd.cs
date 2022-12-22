using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserPermission.Domain.Interface.General
{
    public interface IAdd<T>
    {
        void Add(T entity);
    }
}
