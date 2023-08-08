using Confluent.Kafka;
using KafkaConsumer.Services.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsumer.Services.Repositories
{
    public class KafkaService : IKafkaService
    {
        private string _kafka_BootstrapServers;
        private string _kafka_Topic;
        private readonly IRedisService _IRedisService;
        public KafkaService(IConfiguration configuration, IRedisService redisService)
        {
            _kafka_BootstrapServers = configuration.GetSection("KafkaConfiguration").GetSection("ConnectionString").Value;
            _kafka_Topic = configuration.GetSection("KafkaConfiguration").GetSection("Topic").Value;
            _IRedisService = redisService;
        }

        public async Task SubscribeMessage()
        {
            try
            {
                var config = new ConsumerConfig
                {
                    BootstrapServers = _kafka_BootstrapServers,
                    GroupId = "kafka-voucher-consumer",
                    ClientId = "kafka-voucher-listener-app",
                    AutoOffsetReset = AutoOffsetReset.Earliest
                };
                
                using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
                {
                    consumer.Subscribe(_kafka_Topic);

                    Console.WriteLine("Consumer is now listening for messages...");

                    while (true)
                    {
                        var consumeResult = consumer.Consume();

                        if (consumeResult != null)
                        {
                            string _consumed_message = consumeResult.Message.Value;

                            Console.WriteLine($"Consumed Message: {_consumed_message}");

                            bool _redis_Status = SaveToRedis(_consumed_message).Result;

                            Console.WriteLine($"Save to redis: {_redis_Status}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<bool> SaveToRedis(string _messageValue)
        {
            try
            {
                bool _status = _IRedisService.Save("redis_voucher_app", _messageValue).Result;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
