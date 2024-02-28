using desafio_backend.Application.UseCase.V1.PedidosOperation.Commands.Create;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test.UseCase.V1.PedidosOperation.Commands
{
    public class CreatePedidoValidationTest
    {
        [Fact]
        public async Task Validation_WithPropertyCorrect_IsValidTrue()
        {
            // Arrange
            var request = new CreatePedidoCommand
            {
                CodigoDeContratoInterno = new Random().Next(1000000, 99999999).ToString(),
                CuentaCorriente = new Random().Next(1000000, 99999999).ToString(),
            };
            var validator = new CreatePedidoValidation();
            // Act
            var result = await validator.ValidateAsync(request);
            // Assert 
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("", "341232134")]
        [InlineData("213213123", "")]
        [InlineData(null, "4234234")]
        [InlineData("213123123", null)]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("1231", "1234567890123456789012345678901234567890123456789012345678901234567890123456789023211234567890123456789012345678901234567890123456789012345678901234567890123456789032121234567890123456789012345678901234567890123456789012345678901234567890123456789032123255")]
        [InlineData("1234567890123456789012345678901234567890123456789012345678901234567890123456789023211234567890123456789012345678901234567890123456789012345678901234567890123456789032121234567890123456789012345678901234567890123456789012345678901234567890123456789032123255", "1231")]
        [InlineData("-3213213213", "-341232134")]
        public async Task Validation_WhitPropertyIncorrect_IsValidFalse(string cuentaCorriente, string codigoDeContratoInterno)
        {
            // Arrange
            var request = new CreatePedidoCommand
            {
                CodigoDeContratoInterno = codigoDeContratoInterno,
                CuentaCorriente = cuentaCorriente,
            };
            var validator = new CreatePedidoValidation();
            // Act
            var result = await validator.ValidateAsync(request);
            // Assert
            result.IsValid.Should().BeFalse();
        }
    }
}
