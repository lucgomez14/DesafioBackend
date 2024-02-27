using Andreani.Arq.Pipeline.Clases;
using Andreani.Arq.WebHost.Controllers;
using desafio_backend.Application.UseCase.V1.PersonOperation.Queries.GetByName;
using desafio_backend.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace desafio_backend.Controllers.V2;

[ApiController]
[Route("api/v2/[controller]")]
public class PersonController : ApiControllerBase
{
  [HttpGet("{name}")]
  [ProducesResponseType(typeof(Person), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status404NotFound)]
  public async Task<IActionResult> Get(string name) => this.Result(await Mediator.Send(new GetPersonByName() { Name = name }));
}

