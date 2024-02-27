using desafio_backend.Application.Common.Interfaces;
using desafio_backend.Domain.Entities;
using Andreani.Arq.Pipeline.Clases;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;

namespace desafio_backend.Application.UseCase.V1.PersonOperation.Commands.Create
{
  public class CreatePersonCommand : IRequest<Response<CreatePersonResponse>>
  {
    /// <summary>
    /// 
    /// </summary>
    /// <example>Lucas</example>
    public required string Nombre { get; set; }
    /// <summary>
    /// 
    /// </summary>
    /// <example>Olivera</example>
    public required string Apellido { get; set; }
  }

  public class CreatePersonCommandHandler(ICommandSqlServer repository, ILogger<CreatePersonCommandHandler> logger) : IRequestHandler<CreatePersonCommand, Response<CreatePersonResponse>>
  {
    public async Task<Response<CreatePersonResponse>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
      var entity = new Person
      {
        Nombre = request.Nombre,
        Apellido = request.Apellido
      };
      repository.Insert(entity);
      await repository.SaveChangeAsync();

      logger.LogDebug("the person was add correctly");

      return new Response<CreatePersonResponse>
      {
        Content = new CreatePersonResponse
        {
          Message = "Success",
          PersonId = entity.PersonId
        },
        StatusCode = System.Net.HttpStatusCode.Created
      };
    }
  }
}
