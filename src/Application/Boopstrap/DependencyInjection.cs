using Andreani.Arq.Core.Pipeline.Extension;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace desafio_backend.Application.Boopstrap;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg =>
        {
          cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
        services.AddAndreaniPipeline(Verbose: true);

        return services;
    }
}
