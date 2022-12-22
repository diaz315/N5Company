using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserPermission.Domain.Interface.Business
{
    public interface IKafkaService
    {
        Task<bool> SaveOnKafka(string topic, string message);
    }
}
