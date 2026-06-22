using Microsoft.EntityFrameworkCore;
using PagBoleto.Domain.Entities;

namespace PagBoleto.Infrastructure.Persistence;

public class PagBoletoDbContext : DbContext
{
    public PagBoletoDbContext(DbContextOptions<PagBoletoDbContext> options) : base(options)
    {
    }

    public DbSet<Boleto> Boletos => Set<Boleto>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PagBoletoDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
