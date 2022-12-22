using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserPermission.Domain.Interface.Business
{
    public interface IElasticSearchService<T>
    {
        void Register(T entity);
    }
}
