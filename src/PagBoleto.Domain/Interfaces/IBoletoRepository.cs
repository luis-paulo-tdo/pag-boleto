using PagBoleto.Domain.Entities;

namespace PagBoleto.Domain.Interfaces;

public interface IBoletoRepository
{
    Task AdicionarAsync(Boleto boleto, CancellationToken cancellationToken = default);
    Task<Boleto?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Boleto>> ListarAsync(CancellationToken cancellationToken = default);
}
