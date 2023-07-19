using StackExchange.Redis;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace worker_service_publisher
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly ConnectionMultiplexer _redisConnection;
        private readonly ConsoleKeyInfo cki;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _redisConnection = ConnectionMultiplexer.Connect(configuration.GetSection("RedisConfiguration").GetSection("ConnectionString").Value);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    if (cki.Key == ConsoleKey.Q)
                    {
                        var key = Console.ReadKey(intercept: true);
                        Console.WriteLine("\nExiting the program...");
                        throw new Exception();
                    }
                    else
                    {
                        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                        string _message = "This is a test message!!";

                        string Channel = "test_channel";

                        var pubsub = _redisConnection.GetSubscriber();

                        pubsub.PublishAsync(Channel, _message, CommandFlags.FireAndForget);

                        Console.WriteLine("Message Successfully sent to test-channel");
                        //Console.ReadLine();
                    }

                    //await Task.Delay(1000, stoppingToken);
                }
            }
            catch (Exception)
            {
                Dispose();
            }
        }

        public override void Dispose()
        {
            _redisConnection?.Dispose();
        }
    }
}