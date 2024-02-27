using desafio_backend.Application.UseCase.V1.PersonOperation.Queries.GetByName;
using FluentValidation;
using FluentValidation.TestHelper;

namespace Application.Test.UseCase.V1.PersonOperation.Queries.GetByName
{
    public class GetPersonByNameValidationTest
    {
        private readonly GetPersonByNameValidation _validationRule;

        public GetPersonByNameValidationTest()=> _validationRule = new();

        [Fact]
        public async Task RequestNotEmpty()
        {
            //Arrange
            var request= new GetPersonByName()
            {
                Name=string.Empty
            };

            //Act
            var response = await _validationRule.TestValidateAsync(request);

            // Assert
            response.ShouldHaveValidationErrorFor(x => x.Name)
             .WithErrorCode("NotEmptyValidator");

            response.ShouldHaveAnyValidationError();
        }

        [Fact]
        public async Task RequestMaximumLenghtValidator()
        {
            //Arrange
            var request = new GetPersonByName()
            {
                Name = "testsssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss"
            };

            //Act
            var response = await _validationRule.TestValidateAsync(request);

            //Assert
            response.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorCode("MaximumLengthValidator");

            response.ShouldHaveAnyValidationError();
        }
    }
}
