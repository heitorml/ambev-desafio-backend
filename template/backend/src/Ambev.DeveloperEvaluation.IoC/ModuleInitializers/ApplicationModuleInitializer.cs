using Ambev.DeveloperEvaluation.Common.Security;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class ApplicationModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();

        builder.Services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(builder.Configuration["RabbitMq:Url"], "/", h =>
                {
                    h.Username(builder.Configuration["RabbitMq:Username"]!);
                    h.Password(builder.Configuration["RabbitMq:Pass"]!);
                });
            });
        });
    }
}