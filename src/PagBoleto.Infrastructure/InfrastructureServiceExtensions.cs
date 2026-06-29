using Microsoft.Extensions.DependencyInjection;
using PagBoleto.Application.Abstractions;
using PagBoleto.Domain.Interfaces;
using PagBoleto.Infrastructure.Persistence;
using PagBoleto.Infrastructure.Persistence.Repositories;

namespace PagBoleto.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBoletoRepository, BoletoRepository>();
        services.AddScoped<IOutboxMessageRepository, OutboxMessageRepository>();

        return services;
    }
}
