using Microsoft.EntityFrameworkCore;
using Compartilhado.Models;

namespace Compartilhado.Context;

public class ViagensContext : DbContext
{
    public DbSet<Estados> Estados { get; set; }
    public DbSet<Reserva> Reserva { get; set; }
    public DbSet<Pagamento> Pagamento{ get; set; }
    public ViagensContext(DbContextOptions<ViagensContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);
}
