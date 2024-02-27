using Andreani.Arq.Cqrs.Command;
using Andreani.Arq.Cqrs.Interfaces;
using desafio_backend.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;


namespace desafio_backend.Infrastructure.Services
{
    public class CommandSqlServer([FromKeyedServices("default")] ITransactionalConfiguration config) : TransactionalRepository(config), ICommandSqlServer
    {
    }
}
