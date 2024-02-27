using desafio_backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace desafio_backend.Infrastructure.Persistence.Configurations
{
    public class EstadoDelPedidoConfiguration : IEntityTypeConfiguration<EstadoDelPedido>
    {
        public void Configure(EntityTypeBuilder<EstadoDelPedido> builder)
        {
            builder.ToTable("estadoDelPedido");

            builder.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            builder.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
        }
    }
}
