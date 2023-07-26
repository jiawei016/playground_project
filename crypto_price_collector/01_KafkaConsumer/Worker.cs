using KafkaConsumer.Services.UOW;
using System.Net.NetworkInformation;

namespace KafkaConsumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IUnitOfWork _unitOfWork;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine($"Worker waiting to start at: {DateTimeOffset.Now}");

            Task.Delay(60000, stoppingToken).Wait();

            Console.WriteLine($"Worker start at: {DateTimeOffset.Now}");

            var scope = _serviceProvider.CreateScope();

            try
            {
                _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                bool _status = await _unitOfWork.BitcoinPriceConsumer.get_price_from_kafka_topic("bitcoin-price-topic");

                Console.WriteLine($"Worker running at: {DateTimeOffset.Now}, Status is {_status}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error at: ");
                Console.WriteLine(ex.ToString());
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                Task.Delay(5000, stoppingToken).Wait();
            }
        }
    }
}