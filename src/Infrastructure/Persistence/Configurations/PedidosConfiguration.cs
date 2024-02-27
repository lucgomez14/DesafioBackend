using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using desafio_backend.Domain.Entities;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace desafio_backend.Infrastructure.Persistence.Configurations
{
    public class PedidosConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
                builder.ToTable("pedidos");

                builder.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");
                builder.Property(e => e.CicloDelPedido)
                    .HasMaxLength(50)
                    .HasColumnName("cicloDelPedido");
                builder.Property(e => e.CodigoDeContratoInterno).HasColumnName("codigoDeContratoInterno");
                builder.Property(e => e.Cuando).HasColumnName("cuando");
                builder.Property(e => e.CuentaCorriente)
                    .HasMaxLength(50)
                    .HasColumnName("cuentaCorriente");
                builder.Property(e => e.EstadoDelPedido).HasColumnName("estadoDelPedido");
                builder.Property(e => e.NumeroDePedido).HasColumnName("numeroDePedido");

                builder.HasOne(d => d.EstadoDelPedidoNavigation).WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.EstadoDelPedido)
                    .HasConstraintName("FK_pedidos_estadoDelPedido");
        }
    }
}
