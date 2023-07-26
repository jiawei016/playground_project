using Confluent.Kafka;
using KafkaConsumer.Contexts;
using KafkaConsumer.Models;
using KafkaConsumer.Services.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsumer.Services.Repos
{
    public class KafkaService : IKafkaService
    {
        private IRedisService _IRedisService;
        private readonly PostgreDbContext _postgreDbContext;
        private string _kafkaBootstrapServers;
        public KafkaService(IRedisService redisService, IConfiguration configuration, PostgreDbContext postgreDbContext) 
        {
            _IRedisService = redisService;
            _postgreDbContext = postgreDbContext;
            _kafkaBootstrapServers = configuration.GetSection("KafkaConfiguration").GetSection("ConnectionString").Value;
        }

        public async Task ConsumeTopic(string kafkaTopic)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _kafkaBootstrapServers,
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
                            string _consumed_message = consumeResult.Message.Value;

                            bool _redis_Status = await SaveToRedis(_consumed_message);

                            Console.WriteLine($"Save to redis: {_redis_Status}");

                            if (_redis_Status)
                            {
                                bool _postgres_Status = await SaveToPostgres(_consumed_message);

                                Console.WriteLine($"Save to postgres: {_postgres_Status}");
                            }
                            // Process the consumed message here
                            Console.WriteLine($"Received message: {_consumed_message}");
                        }

                        Task.Delay(500).Wait();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private async Task<bool> SaveToRedis(string _messageValue)
        {
            try
            {
                string _value = _IRedisService.Get("bitcoin_consumer_key").Result;
                MCryptoData _message = JsonConvert.DeserializeObject<MCryptoData>(_messageValue);

                List<MCryptoData> _data = null;

                if (_value != null)
                {
                    _data = JsonConvert.DeserializeObject<List<MCryptoData>>(_value);
                    _data.Add(_message);
                }
                else
                {
                    _data = new List<MCryptoData>();
                    _data.Add(_message);
                }
                string _redis_value = JsonConvert.SerializeObject(_data);
                decimal megabyteSize = ((decimal)Encoding.Unicode.GetByteCount(_redis_value) / 1048576);
                Console.WriteLine($"Size in Mbs of redis cached: {megabyteSize}");

                _IRedisService.Save("bitcoin_consumer_key", _redis_value).Wait();

                return true;
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"Redis error message: {ex.ToString()}");
                return false;
            }
        }

        private async Task<bool> SaveToPostgres(string _messageValue)
        {
            try
            {
                MCryptoData _messageData = JsonConvert.DeserializeObject<MCryptoData>(_messageValue);
                DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;

                BitcoinPriceModel _model = new BitcoinPriceModel
                {
                    Id = now.ToUnixTimeMilliseconds(),
                    Symbol = _messageData.symbol,
                    Name = _messageData.name,
                    Price = _messageData.market_data.current_price.myr
                };

                await _postgreDbContext.AddAsync(_model);

                _postgreDbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Postgres error message: {ex.ToString()}");
                return false;
            }
        }
    }
}
