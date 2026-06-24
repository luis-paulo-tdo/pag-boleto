using PagBoleto.Domain.Entities;

namespace PagBoleto.Domain.Interfaces;

public interface IBoletoRepository
{
    Task AdicionarAsync(Boleto boleto, CancellationToken cancellationToken);
    Task<Boleto?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Boleto>> ListarAsync(CancellationToken cancellationToken);
}
