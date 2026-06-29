using PagBoleto.Domain.Enums;

namespace PagBoleto.Application.Boletos.Dtos;

public record CriarBoletoResult(Guid Id, StatusBoleto Status);
