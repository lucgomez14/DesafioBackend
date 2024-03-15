using Andreani.Arq.Pipeline.Clases;
using Andreani.Scheme.Onboarding;
using desafio_backend.Application.Common.Interfaces;
using desafio_backend.Application.UseCase.V1.PedidosOperation.Commands.Create;
using desafio_backend.Domain.Common;
using desafio_backend.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace desafio_backend.Application.UseCase.V1.PedidosOperation.Queries.GetById
{
    public class GetPedidoById : IRequest<Response<GetPedidoByIdResponse>>
    {
        public required string Id { get; set; }
    }


    public class GetPedidoByIdHandler(IQuerySqlServer query, ILogger<GetPedidoByIdHandler> logger) : IRequestHandler<GetPedidoById, Response<GetPedidoByIdResponse>>
    {
            public async Task<Response<GetPedidoByIdResponse>> Handle(GetPedidoById request, CancellationToken cancellationToken)
            {
            var response = new Response<GetPedidoByIdResponse>();
            try
            {
                logger.LogInformation("Request recibido con el ID: '{0}'", request.Id);
                var pedido = await query.GetPedidoById(new Guid(request.Id));
                if (pedido == null)
                {
                    response.AddNotification("#3123", nameof(request.Id), string.Format(ErrorMessage.NOT_FOUND_RECORD, nameof(Domain.Entities.Pedido), request.Id));
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    logger.LogError("No se encontró ningún pedido con el ID: '{0}'", request.Id);
                    return response;
                }
                var estadoDelPedido = await query.GetDescripcionEstadoDelPedidoById(pedido.EstadoDelPedido);
                if (estadoDelPedido == null)
                {
                    estadoDelPedido = new EstadoDelPedido
                    {
                        Descripcion = "Id no encontrado"
                    };
                }
                response.Content = new GetPedidoByIdResponse
                {
                    Id = pedido.Id.ToString(),
                    NumeroPedido = pedido.NumeroDePedido,
                    CicloDelPedido = pedido.CicloDelPedido,
                    CodigoDeContratoIntero = pedido.CodigoDeContratoInterno,
                    EstadoDelPedido = estadoDelPedido.Descripcion,
                    cuentaCorriente = pedido.CuentaCorriente,
                    cuando = pedido.Cuando.ToString(),
                };
                logger.LogInformation("El pedido fue encontrado correctamente, id:{0}", response.Content.Id);
                response.StatusCode = System.Net.HttpStatusCode.OK; 
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError("Ocurrio un error: {0}", ex.Message);
                response.Content = new GetPedidoByIdResponse
                {
                    message = "Ocurrió un error"
                };
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
                
            }
    }
}
