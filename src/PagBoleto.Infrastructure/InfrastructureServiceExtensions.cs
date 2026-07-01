using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PagBoleto.Application.Abstractions;
using PagBoleto.Domain.Interfaces;
using PagBoleto.Infrastructure.Persistence;
using PagBoleto.Infrastructure.Persistence.Repositories;

namespace PagBoleto.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PagBoletoDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Postgres")));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBoletoRepository, BoletoRepository>();
        services.AddScoped<IOutboxMessageRepository, OutboxMessageRepository>();

        return services;
    }
}
