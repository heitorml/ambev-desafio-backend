using Ambev.DeveloperEvaluation.Events.Consumers.Products;
using Ambev.DeveloperEvaluation.Events.Consumers.Sales;
using Ambev.DeveloperEvaluation.Events.Consumers.Users;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();

    x.AddConsumer<ItemCancelledConsumer>();
    x.AddConsumer<ProductCreatedConsumer>();
    x.AddConsumer<ProductModifiedConsumer>();
    x.AddConsumer<SaleCancelledConsumer>();
    x.AddConsumer<SaleCreatedConsumer>();
    x.AddConsumer<SaleModifiedConsumer>();
    x.AddConsumer<UserRegisteredConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Url"], "/", h =>
        {
            h.Username(builder.Configuration["RabbitMq:Username"]!);
            h.Password(builder.Configuration["RabbitMq:Pass"]!);
        });

        cfg.ConfigureEndpoints(context);

    });
});

var host = builder.Build();
host.Run();
