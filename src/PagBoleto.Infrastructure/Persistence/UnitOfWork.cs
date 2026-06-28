using PagBoleto.Application.Abstractions;

namespace PagBoleto.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly PagBoletoDbContext _context;

    public UnitOfWork(PagBoletoDbContext context) => _context = context;

    public Task<int> CommitAsync(CancellationToken cancellationToken = default) => _context.SaveChangesAsync(cancellationToken);
}
