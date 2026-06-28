using Microsoft.EntityFrameworkCore;
using PagBoleto.Domain.Entities;
using PagBoleto.Domain.Interfaces;

namespace PagBoleto.Infrastructure.Persistence.Repositories;

public class BoletoRepository : IBoletoRepository
{
    private readonly PagBoletoDbContext _context;

    public BoletoRepository(PagBoletoDbContext context) => _context = context;

    public Task AdicionarAsync(Boleto boleto, CancellationToken cancellationToken = default)
    {
        _context.Boletos.AddAsync(boleto, cancellationToken);
        return Task.CompletedTask;
    }
        
    public async Task<Boleto?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _context.Boletos.FindAsync(id, cancellationToken);

    public async Task<IReadOnlyList<Boleto>> ListarAsync(CancellationToken cancellationToken = default) =>
        await _context.Boletos.AsNoTracking().OrderBy(b => b.Vencimento).ToListAsync(cancellationToken);
}
