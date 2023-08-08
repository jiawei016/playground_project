using api_voucher_system.Services.Repositories.Interfaces;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace api_voucher_system.Services.Repositories
{
    public class KafkaService : IKafkaService
    {
        private string _kafka_BootstrapServers;
        private string _kafka_Topic;
        public KafkaService(IConfiguration configuration)
        {
            _kafka_BootstrapServers = configuration.GetSection("KafkaConfiguration").GetSection("ConnectionString").Value;
            _kafka_Topic = configuration.GetSection("KafkaConfiguration").GetSection("Topic").Value;
        }
        public async Task<bool> ProduceMessage(string _message_data)
        {
            bool _message_produced = false;
            var _producerConfig = new ProducerConfig
            {
                BootstrapServers = _kafka_BootstrapServers,
                ClientId = "kafka-message-producer-app",
            };

            try
            {
                var topic = _kafka_Topic;
                var message = new Message<Null, string>
                {
                    Value = _message_data,
                };

                using (var producer = new ProducerBuilder<Null, string>(_producerConfig).Build())
                {
                    var result = await producer.ProduceAsync(topic, message);

                    if (result.Status == PersistenceStatus.Persisted)
                    {
                        _message_produced = true;
                    }

                    Console.WriteLine($"Message delivered to Topic {topic} , Message {_message_data}");

                    producer.Flush(TimeSpan.FromSeconds(10));
                    producer.Dispose();
                }
            }
            catch (Exception ex)
            {
                _message_produced = false;
                throw;
            }

            return _message_produced;
        }
    }
}
