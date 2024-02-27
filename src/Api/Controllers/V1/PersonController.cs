using Andreani.Arq.Pipeline.Clases;
using Andreani.Arq.WebHost.Controllers;
using desafio_backend.Application.UseCase.V1.PersonOperation.Commands.Update;
using desafio_backend.Application.UseCase.V1.PersonOperation.Commands.Create;
using Microsoft.AspNetCore.Mvc;
using desafio_backend.Domain.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using desafio_backend.Application.UseCase.V1.PersonOperation.Queries.GetList;
using WebApi.Models;


namespace desafio_backend.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class PersonController : ApiControllerBase
{
  /// <summary>
  /// Creación de nueva persona
  /// </summary>
  /// <param name="body"></param>
  /// <returns></returns>
  [HttpPost]
  [ProducesResponseType(typeof(CreatePersonResponse), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Create(CreatePersonCommand body) => Result(await Mediator.Send(body));

  /// <summary>
  /// Listado de persona de la base de datos
  /// </summary>
  /// <remarks>en los remarks podemos documentar información más detallada</remarks>
  /// <returns></returns>
  [HttpGet]
  [ProducesResponseType(typeof(List<Person>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Get() => Result(await Mediator.Send(new ListPerson()));

  [HttpPut("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status404NotFound)]
  public async Task<IActionResult> Update(string id, UpdatePersonVm body)
  {
    return Result(await Mediator.Send(new UpdatePersonCommand
    {
      PersonId = id,
      Apellido = body.Apellido,
      Nombre = body.Nombre
    }));
  }

}


