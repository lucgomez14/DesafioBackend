using desafio_backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using desafio_backend.Domain.Entities;
using System.Reflection;

namespace desafio_backend.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Person> Person { get; set; }
    public virtual DbSet<EstadoDelPedido> EstadoDelPedidos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
