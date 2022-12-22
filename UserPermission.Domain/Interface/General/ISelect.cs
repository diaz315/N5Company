using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserPermission.Domain.Interface.General
{
    public interface ISelect<T>
    {
        IList<T> Select(T entity);
    }
}
