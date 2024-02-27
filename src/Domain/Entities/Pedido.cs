using System;
using System.Collections.Generic;

namespace desafio_backend.Domain.Entities;

public partial class Pedido
{
    public Guid Id { get; set; }

    public int? NumeroDePedido { get; set; }

    public string CicloDelPedido { get; set; }

    public long? CodigoDeContratoInterno { get; set; }

    public int? EstadoDelPedido { get; set; }

    public string CuentaCorriente { get; set; }

    public DateOnly? Cuando { get; set; }

    public virtual EstadoDelPedido EstadoDelPedidoNavigation { get; set; }
}
