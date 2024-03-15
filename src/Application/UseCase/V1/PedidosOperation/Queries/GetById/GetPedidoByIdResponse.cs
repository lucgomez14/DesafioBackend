using desafio_backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafio_backend.Application.UseCase.V1.PedidosOperation.Queries.GetById
{
    public class GetPedidoByIdResponse
    {
        public string Id { get; set; }
        public int? NumeroPedido { get; set; }
        public string CicloDelPedido { get; set; }
        public long? CodigoDeContratoIntero { get; set; }
        public string EstadoDelPedido { get; set; }

        public string cuentaCorriente { get; set; }
        public string cuando {  get; set; }
        public string message { get; set; }

    }
}
