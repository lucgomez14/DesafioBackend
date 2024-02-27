using Andreani.Arq.Pipeline.Clases;
using Andreani.Arq.WebHost.Controllers;
using desafio_backend.Application.UseCase.V1.PedidosOperation.Commands.Create;
using desafio_backend.Application.UseCase.V1.PedidosOperation.Queries.GetById;
using Microsoft.AspNetCore.Mvc;
using desafio_backend.Domain.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WebApi.Models;
using MediatR;

namespace desafio_backend.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [Controller]
    public class PedidoController : ApiControllerBase
    {
        /// <summary>
        /// Creación de un nuevo pedido
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreatePedidoResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreatePedidoCommand body) => Result(await Mediator.Send(body));

        /// <summary>
        /// Listado de un pedido de la base de datos
        /// </summary>
        /// <remarks>en los remarks podemos documentar información más detallada</remarks>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Pedido), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id) => this.Result(await Mediator.Send(new GetPedidoById() { Id = id }));
        //public async Task<IActionResult> Get(string name) => this.Result(await Mediator.Send(new GetPersonByName() { Name = name }));
    }
}
