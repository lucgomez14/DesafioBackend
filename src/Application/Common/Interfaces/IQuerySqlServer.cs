using Andreani.Arq.Core.Interface;
using desafio_backend.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace desafio_backend.Application.Common.Interfaces;

    public interface IQuerySqlServer: IReadOnlyQuery
    {
        public Task<Person> GetPersonByNameAsync(string name);
        public Task<Pedido> GetPedidoById(Guid id);
        public Task<EstadoDelPedido> GetDescripcionEstadoDelPedidoById(int? id);
    }
