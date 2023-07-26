using _00_CryptoWorkerService.Helper;
using _00_CryptoWorkerService.Models;
using _00_CryptoWorkerService.Service.Repo;
using _00_CryptoWorkerService.Service.Repo.Interface;
using _00_CryptoWorkerService.Service.UOW;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json.Nodes;

namespace _00_CryptoWorkerService
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
            double _price_count = 0;

            var scope = _serviceProvider.CreateScope();
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                    bool _status = await _unitOfWork.BitcoinPriceProducer.get_price_and_producemessage("bitcoin", _price_count);

                    Console.WriteLine($"Worker running at: {DateTimeOffset.Now}, Status is {_status}");
                }
                catch(Exception ex)
                {

                }

                _price_count++;

                Task.Delay(200, stoppingToken).Wait();
            }
        }
    }
}