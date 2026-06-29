using PagBoleto.Domain.Interfaces;

namespace PagBoleto.Application.Boletos.Commands;

public record CriarBoletoCommand(string LinhaDigitavel, decimal Valor, DateOnly Vencimento);

public class CriarBoletoCommandHandler
{
    private readonly IBoletoRepository _boletoRepository;
}
