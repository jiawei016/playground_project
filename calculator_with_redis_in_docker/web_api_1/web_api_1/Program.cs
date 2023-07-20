using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using web_api_1.Contexts;
using web_api_1.Services;
using web_api_1.Services.Interfaces;
using web_api_1.Services.Modules;
using web_api_1.Services.Modules.Interfaces;
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

builder.Services.AddDbContext<PostgreDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetSection("PostgreDBConfiguration").GetSection("ConnectionString").Value);
});

//builder.Services.Configure<CassandraDBConfigurationSetting>(
//    builder.Configuration.GetSection("CassandraDBConfiguration")
//);

builder.Services.AddScoped<IItemService, ItemService>();

builder.Services.AddScoped<IRedisService, RedisService>();
builder.Services.AddScoped<IPostgreService, PostgreService>();

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
