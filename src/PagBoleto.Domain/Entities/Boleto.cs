using PagBoleto.Domain.Enums;

namespace PagBoleto.Domain.Entities;

public class Boleto
{
    public Guid Id { get; private set; }
    
    public string LinhaDigitavel { get; private set; } = string.Empty;

    public decimal Valor { get; private set; }

    public DateOnly Vencimento { get; private set; }

    public StatusBoleto Status { get; private set; }

    public int TentativasProcessamento { get; private set; }

    public string MotivoFalha { get; private set; } = string.Empty;

    public DateTime CriadoEm { get; private set; }

    public DateTime? AtualizadoEm { get; private set; }

    private const int MaxTentativas = 3;

    protected Boleto() { }

    public static Boleto Criar(string linhaDigitavel, decimal valor, DateOnly vencimento)
    {
        if (linhaDigitavel == string.Empty)
            throw new ArgumentException("A linha digitável está inválida.", nameof(linhaDigitavel));

        if (valor <= 0)
            throw new ArgumentOutOfRangeException(nameof(valor), "O valor a ser pago não existe.");

        if (vencimento < DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentOutOfRangeException(nameof(vencimento), "A data de vencimento está atrasada.");

        return new Boleto()
        {
            Id = Guid.NewGuid(),
            Status = StatusBoleto.Pendente,
            LinhaDigitavel = linhaDigitavel,
            Valor = valor,
            Vencimento = vencimento,
            CriadoEm = DateTime.UtcNow
        };
    }

    public void MarcarEmProcessamento()
    {
        if (Status != StatusBoleto.Pendente)
            throw new InvalidOperationException("Não é possível alterar status para 'Em Processamento'.");

        Status = StatusBoleto.EmProcessamento;
        AtualizadoEm = DateTime.UtcNow;
    }

    public void MarcarPago()
    {
        if (Status != StatusBoleto.EmProcessamento)
            throw new InvalidOperationException("Não é possível alterar status para 'Pago'.");

        Status = StatusBoleto.Pago;
        AtualizadoEm = DateTime.UtcNow;
    }

    public void RegistrarFalha(string motivo)
    {
        if (Status == StatusBoleto.Pendente)
            throw new InvalidOperationException("Não é possível registrar uma falha para um boleto pendente.");

        if (Status == StatusBoleto.Falha)
            throw new InvalidOperationException("Não é possível registrar uma falha para um boleto com falha.");

        if (Status == StatusBoleto.Pago)
            throw new InvalidOperationException("Não é possível registrar uma falha para boletos pagos.");

        TentativasProcessamento++;
        MotivoFalha = motivo;
        AtualizadoEm = DateTime.UtcNow;

        if (TentativasProcessamento >= MaxTentativas)
            Status = StatusBoleto.Falha;
        else
            Status = StatusBoleto.Pendente;
    }
}
