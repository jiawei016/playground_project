using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using worker_service_subscriber.Models;

namespace worker_service_subscriber
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly ConnectionMultiplexer _redisConnection;
        private ISubscriber _redisSubscriber;
        private readonly IHttpClientFactory _httpClientFactory;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _configuration = configuration;
            
            _redisConnection = ConnectionMultiplexer.Connect(configuration.GetSection("RedisConfiguration").GetSection("ConnectionString").Value);
            _redisSubscriber = _redisConnection.GetSubscriber();

            _httpClientFactory = httpClientFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string Channel = "test_channel";

            _redisSubscriber.Subscribe(Channel, async (channel, message) => {
                var db = _redisConnection.GetDatabase();
                string _message = db.StringGet(Channel);
                await SetIsProcessed(_message);
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

        private async Task SetIsProcessed(string json)
        {
            string _Id, _ChannelId, _Content;
            int _IsProcessed;
            try
            {
                var _object = JsonConvert.DeserializeObject<ItemModel>(json);

                var apiUrl = $"api/v1/ExpressApi/SetIsProcessed?key={_object.ChannelId}&Id={_object.Id}";

                var httpClient = _httpClientFactory.CreateClient("http_client");

                HttpContent body = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(apiUrl, body);

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public override void Dispose()
        {
            _redisSubscriber?.UnsubscribeAll();
            _redisConnection?.Dispose();
        }
    }
}