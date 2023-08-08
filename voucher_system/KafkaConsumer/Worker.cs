using KafkaConsumer.Services.Repositories;
using KafkaConsumer.Services.Repositories.Interfaces;
using StackExchange.Redis;
using System.Net.NetworkInformation;

namespace KafkaConsumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IKafkaService _IKafkaService;
        private IServiceProvider _serviceProvider;
        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine($"Worker waiting to start at: {DateTimeOffset.Now}");

            //Task.Delay(30000, stoppingToken).Wait();

            Console.WriteLine($"Worker start at: {DateTimeOffset.Now}");

            while (!stoppingToken.IsCancellationRequested)
            {
                bool _status = KafSubscriber();
                Console.WriteLine($"Worker restart running at: {DateTimeOffset.Now}");
                Task.Delay(1000, stoppingToken).Wait();
            }
        }

        private bool KafSubscriber()
        {
            try
            {
                var scope = _serviceProvider.CreateScope();
                _IKafkaService = scope.ServiceProvider.GetService<IKafkaService>();
                _IKafkaService.SubscribeMessage().Wait();

                Console.WriteLine($"Worker stop running at: {DateTimeOffset.Now}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error at: {ex}");
                return false;
            }

            return true;
        }
    }
}