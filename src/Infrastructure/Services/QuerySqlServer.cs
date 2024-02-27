using Andreani.Arq.Cqrs.Interfaces;
using Andreani.Arq.Cqrs.Queries;
using desafio_backend.Application.Common.Interfaces;
using desafio_backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using desafio_backend.Infrastructure.Persistence;
using System.Threading.Tasks;
using System;

namespace desafio_backend.Infrastructure.Services
{
    public class QuerySqlServer : ReadOnlyQuery, IQuerySqlServer
    {
        private readonly ApplicationDbContext _context;
        public QuerySqlServer([FromKeyedServices("default")] IReadOnlyQueryConfiguration config,
            [FromKeyedServices("default")] ApplicationDbContext context) : base(config)
        {
          _context = context;
        }

        public async Task<EstadoDelPedido> GetDescripcionEstadoDelPedidoById(int? id)
        {
            return await _context.EstadoDelPedidos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Pedido> GetPedidoById(Guid id)
        {
            return await _context.Pedidos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Person> GetPersonByNameAsync(string name)
        {
          return await _context.Person.FirstAsync(x => x.Nombre == name);
        }
    }
}
