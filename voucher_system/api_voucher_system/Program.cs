using api_voucher_system.Context;
using api_voucher_system.Services.Repositories.Interfaces;
using api_voucher_system.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using api_voucher_system.Controllers.Attributes;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
    policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddControllers(
    mvcOptions => mvcOptions.Conventions.Add(new CustomValueRoutingConvention())
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetSection("PostgresConfiguration").GetSection("ConnectionString").Value);
});

builder.Services.AddScoped<IKafkaService, KafkaService>();
builder.Services.AddScoped<IVoucherService, VoucherService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
