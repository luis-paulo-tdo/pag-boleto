using PagBoleto.Domain.Entities;

namespace PagBoleto.Domain.Interfaces;

public interface IOutboxMessageRepository
{
    Task AdicionarAsync(OutboxMessage outboxMessage, CancellationToken cancellationToken = default);
}
