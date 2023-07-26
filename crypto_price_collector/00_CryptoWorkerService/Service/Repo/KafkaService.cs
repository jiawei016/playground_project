using _00_CryptoWorkerService.Models;
using _00_CryptoWorkerService.Service.Repo.Interface;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00_CryptoWorkerService.Service.Repo
{
    public class KafkaService : IKafkaService
    {
        private string kafka_BootstrapServers;
        public KafkaService(IConfiguration configuration)
        {
            kafka_BootstrapServers = configuration.GetSection("KafkaConfiguration").GetSection("ConnectionString").Value;
        }
        public async Task<bool> ProduceMessage(MCryptoData _mCryptoData)
        {
            bool _message_produced = false;
            var _producerConfig = new ProducerConfig
            {
                BootstrapServers = kafka_BootstrapServers,
                ClientId = "bitcoin-price-listener-app",
            };

            try
            {
                var topic = "bitcoin-price-topic";
                var message = new Message<Null, string>
                {
                    Value = JsonConvert.SerializeObject(_mCryptoData),
                };

                using (var producer = new ProducerBuilder<Null, string>(_producerConfig).Build())
                {
                    var result = producer.ProduceAsync(topic, message).Result;

                    if (result.Status == PersistenceStatus.Persisted)
                    {
                        _message_produced = true;
                    }

                    Console.WriteLine($"Message delivered to {result.TopicPartitionOffset}");

                    producer.Flush(TimeSpan.FromSeconds(10));
                    producer.Dispose();
                }
            }
            catch (Exception ex)
            {
                _message_produced = false;
            }

            return _message_produced;
        }
    }
}
