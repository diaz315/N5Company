using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserPermission.Domain.Interface.Business
{
    public interface ICLog<T>
    {
        void Error(string info);
        void Info(string info);
        void Debug(string info);
    }
}
