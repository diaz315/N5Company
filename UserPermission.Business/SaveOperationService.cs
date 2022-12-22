using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserPermission.Domain;
using UserPermission.Domain.Interface.Business;

namespace UserPermission.Business
{
    public class SaveOperationService: ISaveOperationService
    {
        private readonly IKafkaService _kafkaService;
        public SaveOperationService(IKafkaService kafkaService)
        {
            _kafkaService = kafkaService;
        }

        public async Task<bool> Save(object operation)
        {
            string message = JsonSerializer.Serialize(operation);
            string topic = Environment.GetEnvironmentVariable("TOPIC")!;
            return await _kafkaService.SaveOnKafka(topic, message);
        }
    }
}
