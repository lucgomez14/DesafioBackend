using AutoFixture;
using desafio_backend.Application.Common.Interfaces;
using desafio_backend.Application.UseCase.V1.PedidosOperation.Queries.GetById;
using desafio_backend.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test.UseCase.V1.PedidosOperation.Queries
{
    public class GetPedidoByIdTest
    {
        private readonly Mock<IQuerySqlServer> _query;
        private readonly Mock<ILogger<GetPedidoByIdHandler>> _logger;
        private readonly GetPedidoByIdHandler _handler;
        private CancellationToken _cancellationToken;

        public GetPedidoByIdTest()
            {
                _query = new();
                _logger = new Mock<ILogger<GetPedidoByIdHandler>>();
                _cancellationToken = new CancellationToken();
                _handler = new GetPedidoByIdHandler(_query.Object,_logger.Object);
        }

        [Fact]
        public async Task Handler_GetPedidoById_Success()
        {
                // Arrange
                var request = new GetPedidoById
                {
                    Id = Guid.NewGuid().ToString()
                };
                var pedido = new Pedido
                {
                    Id = Guid.Parse(request.Id),
                    NumeroDePedido = new Random().Next(1000000, 99999999),
                    CicloDelPedido = new Random().Next(1000000, 99999999).ToString(),
                    CodigoDeContratoInterno = new Random().Next(1000000, 99999999),
                    EstadoDelPedido = new Random().Next(1, 3),
                    CuentaCorriente = new Random().Next(1000000, 99999999).ToString(),
                    Cuando = new DateOnly(),
                    EstadoDelPedidoNavigation = null,
                };
                _query.Setup(_ => _.GetPedidoById(It.IsAny<Guid>())).ReturnsAsync(pedido);
                // Act
                var result = await _handler.Handle(request, _cancellationToken);
                // Assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Handler_GetPedidoById_PedidoNotExist()
        {
                // Arrange
                var request = new GetPedidoById
                {
                    Id = Guid.NewGuid().ToString()
                };
                _query.Setup(_ => _.GetPedidoById(It.IsAny<Guid>())).ReturnsAsync((Pedido)null);
                // Act
                var result = await _handler.Handle(request, _cancellationToken);
                // Assert
                result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Handler_GetPersonByName_ThrowUpdateDatabase()
        {
                // Arrange
                var request = new GetPedidoById
                {
                    Id = Guid.NewGuid().ToString()
                };
                _query.Setup(_ => _.GetPedidoById(It.IsAny<Guid>())).ThrowsAsync(new DbUpdateException());
                // Atc
                // Assert
                await Assert.ThrowsAsync<DbUpdateException>(() => _handler.Handle(request, _cancellationToken));
        }
    }
}
