using PagBoleto.Domain.Entities;
using PagBoleto.Domain.Interfaces;

namespace PagBoleto.Infrastructure.Persistence.Repositories;

public class OutboxMessageRepository : IOutboxMessageRepository
{
    private readonly PagBoletoDbContext _context;

    public OutboxMessageRepository(PagBoletoDbContext context) => _context = context;

    public Task AdicionarAsync(OutboxMessage outboxMessage, CancellationToken cancellationToken = default)
    {
        _context.OutboxMessages.Add(outboxMessage);
        return Task.CompletedTask;
    }
}
