using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace worker_service_subscriber
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly ConnectionMultiplexer _redisConnection;
        private ISubscriber _redisSubscriber;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            
            _redisConnection = ConnectionMultiplexer.Connect(configuration.GetSection("RedisConfiguration").GetSection("ConnectionString").Value);
            _redisSubscriber = _redisConnection.GetSubscriber();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string Channel = "test_channel";

            _redisSubscriber.Subscribe(Channel, (channel, message) => {
                var db = _redisConnection.GetDatabase();
                string _message = db.StringGet(Channel);
                Console.WriteLine("Message received from test-channel_web_api : " + _message);
                Console.WriteLine("Message received from test-channel : " + message);
            });

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(intercept: true);
                        Console.WriteLine("\nExiting the program...");
                        throw new Exception();
                    }
                }
            }
            catch (Exception)
            {
                Dispose();
            }
        }

        public override void Dispose()
        {
            _redisSubscriber?.UnsubscribeAll();
            _redisConnection?.Dispose();
        }
    }
}