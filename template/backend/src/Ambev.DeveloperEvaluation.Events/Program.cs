using Ambev.DeveloperEvaluation.Common.Logging;
using Ambev.DeveloperEvaluation.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSerilog(new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger());

builder.Services.AddMassTransit(x =>
{
    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
        .Where(a => a.GetName().Name?.StartsWith("Ambev.DeveloperEvaluation") == true)
        .ToArray();

    x.AddConsumers(assemblies);

    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbitUrl = builder.Configuration["RabbitMq:Url"] ?? "localhost";
        cfg.Host(rabbitUrl, h =>
        {
            h.Username(builder.Configuration["RabbitMq:Username"] ?? "guest");
            h.Password(builder.Configuration["RabbitMq:Pass"] ?? "guest");
        });

        cfg.UseMessageRetry(retry =>
        {
            retry.Exponential(
              retryLimit: 2,
              minInterval: TimeSpan.FromSeconds(1),
              maxInterval: TimeSpan.FromSeconds(30),
              intervalDelta: TimeSpan.FromSeconds(3));
        });

        cfg.UseCircuitBreaker(cb =>
        {
            cb.TrackingPeriod = TimeSpan.FromMinutes(1);
            cb.TripThreshold = 1;
            cb.ActiveThreshold = 10;
            cb.ResetInterval = TimeSpan.FromMinutes(5);
        });

        cfg.UseInMemoryOutbox(context);
        cfg.ConfigureEndpoints(context);

    });
});

var host = builder.Build();
host.Run();
