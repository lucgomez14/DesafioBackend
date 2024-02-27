using AutoFixture;
using FluentAssertions;
using Moq;
using System.Net;
using desafio_backend.Application.UseCase.V1.PersonOperation.Queries.GetList;
using desafio_backend.Application.Common.Interfaces;
using desafio_backend.Domain.Entities;

namespace Application.Test.PersonOperation.Queries.GetList
{
  public class ListPersonTest
  {
    private readonly Mock<IQuerySqlServer> _query;
    private readonly ListPersonHandler _handler;
    private readonly CancellationToken _cancellationToken;
    public ListPersonTest()
    {
      _query = new();
      _cancellationToken = CancellationToken.None;
      _handler = new ListPersonHandler(_query.Object);
    }

    [Fact]
    public async Task Handle_GetListUser_Success()
    {
      // Arrange
      var request = new ListPerson();
      var resonse = new Fixture().CreateMany<Person>();
      _query.Setup(_ => _.GetAllAsync<Person>())
          .ReturnsAsync(resonse);
      // Act
      var result = await _handler.Handle(request, _cancellationToken);

      // Assert
      result.Content.Should().Equal(resonse);
      result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Handle_GetListUser_ThrowException()
    {
      // Arrange
      var request = new ListPerson();
      _query.Setup(_ => _.GetAllAsync<Person>())
          .ThrowsAsync(new Exception());

      // Act
      // Assert
      await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, _cancellationToken));

    }
  }
}
