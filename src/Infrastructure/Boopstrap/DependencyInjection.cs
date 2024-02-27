using Andreani.Arq.Cqrs.Extension;
using desafio_backend.Application.Common.Interfaces;
using desafio_backend.Infrastructure.Persistence;
using desafio_backend.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace desafio_backend.Infrastructure.Boopstrap;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCQRS(configuration)
        .Configure<ApplicationDbContext>();

        services.AddScoped<ICommandSqlServer, CommandSqlServer>();
        services.AddScoped<IQuerySqlServer, QuerySqlServer>();

    return services;
    }
}
