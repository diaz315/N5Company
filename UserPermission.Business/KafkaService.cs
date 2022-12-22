using Confluent.Kafka;
using System.Net;
using UserPermission.Domain.Interface.Business;
using Microsoft.Extensions.Logging;

namespace UserPermission.Business
{
    public class KafkaService : IKafkaService
    {
        private readonly ILogger<KafkaService> _logger;

        public KafkaService(ILogger<KafkaService> logger) {
            _logger = logger;
        }

        public async Task<bool> SaveOnKafka(string topic,string message)
        {
            ProducerConfig config = new ProducerConfig
            {
                BootstrapServers = Environment.GetEnvironmentVariable("URLKAFKA")!,
                ClientId = Dns.GetHostName()
            };

            try
            {
                using (var producer = new ProducerBuilder
                <Null, string>(config).Build())
                {
                    var result = await producer.ProduceAsync
                    (topic, new Message<Null, string>
                    {
                        Value = message
                    });

                    _logger.LogDebug($"Delivery Timestamp: { result.Timestamp.UtcDateTime}");
                return await Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured: {ex.Message}");
            }

            return await Task.FromResult(false);
        }
    }
}