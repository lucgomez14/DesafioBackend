using Andreani.Arq.Pipeline.Clases;
using Andreani.Scheme.Onboarding;
using desafio_backend.Application.Common.Interfaces;
using desafio_backend.Domain.Entities;
using Andreani.Arq.AMQStreams;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Andreani.Arq.Core.Interface;
using Andreani.Arq.AMQStreams.Interface;
using MediatR;

namespace desafio_backend.Application.UseCase.V1.PedidosOperation.Commands.Create
{
    public class CreatePedidoCommand : IRequest<Response<CreatePedidoResponse>>
    {
        public string CuentaCorriente { get; set; }
        public string CodigoDeContratoInterno { get; set; }
    }

    public class CreatePedidoCommandHandler(ICommandSqlServer repository, ILogger<CreatePedidoCommandHandler> logger, Andreani.Arq.AMQStreams.Interface.IPublisher publisher) : IRequestHandler<CreatePedidoCommand, Response<CreatePedidoResponse>>
    {
 

        public async Task<Response<CreatePedidoResponse>> Handle(CreatePedidoCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Request recibido con los datos: CuentaCorriente '{0}' y Codigo de contrato interno '{1}' ", request.CuentaCorriente, request.CodigoDeContratoInterno);
            var id = Guid.NewGuid();
            try
            {
                var pedido = new Domain.Entities.Pedido
                {
                    Id = id,
                    NumeroDePedido = null,
                    CicloDelPedido = id.ToString(),
                    CodigoDeContratoInterno = long.Parse(request.CodigoDeContratoInterno),
                    Cuando = DateOnly.FromDateTime(DateTime.Now),
                    CuentaCorriente = request.CuentaCorriente,
                    EstadoDelPedido = 1,

                };
                repository.Insert(pedido);
                await repository.SaveChangeAsync();

                var message = new Andreani.Scheme.Onboarding.Pedido
                {
                    id = pedido.Id.ToString(),
                    cicloDelPedido = pedido.CicloDelPedido,
                    codigoDeContratoInterno = (long)pedido.CodigoDeContratoInterno,
                    estadoDelPedido = pedido.EstadoDelPedido.ToString(),
                    cuentaCorriente = long.Parse(pedido.CuentaCorriente),
                    cuando = pedido.Cuando.ToString()
                };
                await publisher.To(message, "PedidoCreado");

                logger.LogInformation("El pedido fue agregado correctamente, Cuenta corriente: {0}, Contrato Interno: {1}", request.CuentaCorriente, request.CodigoDeContratoInterno);

            }
            catch (Exception ex) //Después dividirlo en kafka y bd
            {
                logger.LogError("Ocurrió un error {0}",ex.Message);

                return new Response<CreatePedidoResponse>
                {
                    Content = new CreatePedidoResponse
                    {
                        Message = "Ocurrió un error"
                    },
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }


            var headers = new Dictionary<string, string>
            {
                { "location", $"api/pedido({id})"},
            };
            return new Response<CreatePedidoResponse>
            {
                Content = new CreatePedidoResponse
                {
                    Id = id,
                },
                Headers = headers,
                StatusCode = System.Net.HttpStatusCode.Created
            };

        }
    }
}
