using _00_CryptoWorkerService.Helper;
using _00_CryptoWorkerService.Models;
using _00_CryptoWorkerService.Service;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json.Nodes;

namespace _00_CryptoWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public Worker(ILogger<Worker> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            CryptoService _CryptoService = new CryptoService();
            KafkaService _KafkaService = new KafkaService();
            while (!stoppingToken.IsCancellationRequested)
            {
                MCryptoData _mCryptoData = await _CryptoService.GetPriceData(_httpClientFactory, "bitcoin");
                bool _produced_status = await _KafkaService.ProduceMessage(_mCryptoData);

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(3000, stoppingToken);
            }
        }
    }
}