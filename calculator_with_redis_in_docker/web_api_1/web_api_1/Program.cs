using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using web_api_1.Contexts;
using web_api_1.Services;
using web_api_1.Services.Interfaces;
using web_api_1.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(builder.Configuration.GetSection("RedisConfiguration").GetSection("ConnectionString").Value)
);
builder.Services.Configure<CassandraDBConfigurationSetting>(
    builder.Configuration.GetSection("CassandraDBConfiguration")
);
builder.Services.AddSingleton(
    new CassandraDbContext(
        builder.Configuration.GetSection("CassandraDBConfiguration").GetSection("ConnectionString").Value
    )
);

builder.Services.AddScoped<IRedisService, RedisService>();
builder.Services.AddScoped<ICassandraService, CassandraService>();
builder.Services.AddScoped<IExpressService, ExpressService>();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var MyAllowedOrigins = "_myAllowedOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowedOrigins, builder =>
    {
        builder.WithOrigins("http://localhost:5138")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
