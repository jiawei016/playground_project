using _00_CryptoWorkerService.Models;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00_CryptoWorkerService.Service
{
    public class KafkaService
    {
        public async Task<bool> ProduceMessage(MCryptoData _mCryptoData)
        {
            bool _message_produced = false;
            // Kafka producer configuration
            var _producerConfig = new ProducerConfig
            {
                BootstrapServers = "your-bootstrap-servers",
                ClientId = "your-client-id",
                // Additional configuration options
            };

            try
            {
                // Produce messages to Kafka topic
                var topic = "your-topic";
                var message = new Message<Null, string>
                {
                    Value = JsonConvert.SerializeObject(_mCryptoData),
                };

                // Handle delivery reports and errors
                using (var producer = new ProducerBuilder<Null, string>(_producerConfig).Build())
                {
                    var result = await producer.ProduceAsync(topic, message);

                    if (result.Status == PersistenceStatus.Persisted)
                    {
                        _message_produced = true;
                    }

                    Console.WriteLine($"Message delivered to {result.TopicPartitionOffset}");

                    // Gracefully close the producer
                    producer.Flush(TimeSpan.FromSeconds(10));
                    producer.Dispose();
                }
            } 
            catch(Exception ex)
            {
                _message_produced = false;
            }

            return _message_produced;
        }
    }
}
