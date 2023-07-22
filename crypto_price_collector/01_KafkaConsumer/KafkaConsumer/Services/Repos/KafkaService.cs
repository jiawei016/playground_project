using Confluent.Kafka;
using KafkaConsumer.Services.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsumer.Services.Repos
{
    public class KafkaService : IKafkaService
    {
        public KafkaService() { }

        public async Task ConsumeTopic(string kafkaTopic)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "bitcoin-price-kafka-consumer",
                ClientId = "bitcoin-price-listener-app",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe(kafkaTopic);

                Console.WriteLine("Consumer is now listening for messages...");

                try
                {
                    while (true)
                    {
                        var consumeResult = consumer.Consume();

                        if (consumeResult != null)
                        {
                            // Process the consumed message here
                            Console.WriteLine($"Received message: {consumeResult.Message.Value}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
