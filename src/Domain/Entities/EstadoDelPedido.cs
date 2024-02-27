using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafio_backend.Domain.Entities
{
    public class EstadoDelPedido
    {
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
