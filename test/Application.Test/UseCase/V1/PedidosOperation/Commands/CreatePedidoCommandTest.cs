using Andreani.Arq.AMQStreams.Interface;
using AutoFixture;
using desafio_backend.Application.Common.Interfaces;
using desafio_backend.Application.UseCase.V1.PedidosOperation.Commands.Create;
using desafio_backend.Application.UseCase.V1.PersonOperation.Commands.Create;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test.UseCase.V1.PedidosOperation.Commands
{
    public class CreatePedidoCommandTest
    {
            private readonly Mock<ICommandSqlServer> _repository;
            private readonly Mock<ILogger<CreatePedidoCommandHandler>> _logger;
            private readonly Mock<IPublisher> _publisher;
            private CreatePedidoCommandHandler _handler;
            private CancellationToken _cancellationToken;

            public CreatePedidoCommandTest()
            {
                _repository = new();
                _logger = new Mock<ILogger<CreatePedidoCommandHandler>>();
                _cancellationToken = CancellationToken.None;
                _publisher = new();

                _handler = new CreatePedidoCommandHandler(_repository.Object, _logger.Object, _publisher.Object);

            }

            [Fact]
            public async Task Handle_CreatePedido_Success()
            {
                // Arrange
                //var request = new Fixture().Create<CreatePedidoCommand>();
                var request = new CreatePedidoCommand
                {
                    CodigoDeContratoInterno = new Random().Next(1000000, 99999999).ToString(),
                    CuentaCorriente = new Random().Next(1000000, 99999999).ToString(),
                };
                // Act
                var result = await _handler.Handle(request, _cancellationToken);
                // Assert
                result.StatusCode.Should().Be(HttpStatusCode.Created);
            }
            [Fact]
            public async Task Handler_CreatePedido_UpdateDatabaseException()
            {
                // Arrange
                var request = new CreatePedidoCommand
                {
                    CodigoDeContratoInterno = new Random().Next(1000000, 99999999).ToString(),
                    CuentaCorriente = new Random().Next(1000000, 99999999).ToString(),
                };

                // Act
                _repository.Setup(_ => _.SaveChangeAsync()).ThrowsAsync(new DbUpdateException());
                // Assert
                await Assert.ThrowsAsync<DbUpdateException>(() => _handler.Handle(request, _cancellationToken));
            } 

            //Agregar casos que no funcionen D:
    }
}
