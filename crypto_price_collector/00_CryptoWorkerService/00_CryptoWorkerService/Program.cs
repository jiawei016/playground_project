using _00_CryptoWorkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddHttpClient("GitHub", httpClient =>
        {
            httpClient.BaseAddress = new Uri("https://api.github.com/");
        });
    })
    .Build();


await host.RunAsync();
