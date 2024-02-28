using desafio_backend.Application.UseCase.V1.PedidosOperation.Queries.GetById;
using FluentAssertions;

namespace Application.Test.UseCase.V1.PedidosOperation.Queries
{
    public class GetPedidoByIdValidationTest
    {
        [Fact]
        public async Task Validation_WithPropertyCorrect_IsValidTrue()
        {
            // Arrange
            var request = new GetPedidoById
            {
                Id = Guid.NewGuid().ToString()
            };
            var validator = new GetPedidoByIdValidation();
            // Act
            var result = await validator.ValidateAsync(request);
            // Assert 
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("c4234jdsf-4324243fsdse-34234dsfsd-4324wfsr-42342rdsf-543543e23-dfsf54435-dfsd2342342-frsdfsd4td-534534543tdg5f434-5rew24rwerw-rewr45435-5rwrr4wr43543-fdsfdsfdsfdsfsd-324d32ew24-423sw24-d3543rew5-53t543er4354-t54435er3t-3453eet453-t34e-543-3etert4353453ege43")]

        public async Task Validation_WhitPropertyIncorrect_IsValidFalse(string id)
        {
            // Arrange
            var request = new GetPedidoById
            {
                Id = id,
            };
            var validator = new GetPedidoByIdValidation();
            // Act
            var result = await validator.ValidateAsync(request);
            // Assert 
            result.IsValid.Should().BeFalse();
        }
    }
}
