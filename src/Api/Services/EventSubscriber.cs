using Amazon.Runtime.Internal.Util;
using Andreani.Arq.AMQStreams.Class;
using Andreani.Arq.AMQStreams.Interface;
using Andreani.Scheme.Onboarding;
using desafio_backend.Application.Common.Interfaces;
using System;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace desafio_backend.Services
{
    public class EventSubscriber: ISubscriber
    {
        private readonly IQuerySqlServer _query;
        //private readonly ILogger _logger;
        private readonly ICommandSqlServer _repository;

        public EventSubscriber(IQuerySqlServer querySqlServer, ICommandSqlServer repository)
        {
            _query = querySqlServer;
            _repository = repository;
        }

        public async Task Consume(Pedido @event, ConsumerMetadata metaData)
        {
            //try
            //{
                var pedido = await _query.GetPedidoById(new Guid(@event.id));
                //if (pedido != null) 
                //{
                    pedido.NumeroDePedido = @event.numeroDePedido;
                    pedido.EstadoDelPedido = int.Parse(@event.estadoDelPedido);
                    await _repository.SaveChangeAsync();
                //}

            //}
           
        }
    }
}
