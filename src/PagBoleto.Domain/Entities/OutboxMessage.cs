namespace PagBoleto.Domain.Entities;

public class OutboxMessage
{
    public Guid Id { get; private set; }

    public string Tipo { get; private set; } = string.Empty;

    public string Conteudo { get; private set; } = string.Empty;

    public DateTime CriadoEm { get; private set; }

    public DateTime? ProcessadoEm { get; private set; }

    protected OutboxMessage() { }

    public static OutboxMessage Criar(string tipo, string conteudo)
    {
        if (string.IsNullOrWhiteSpace(tipo))
            throw new ArgumentException("O tipo da mensagem está inválido.", nameof(tipo));

        if (string.IsNullOrWhiteSpace(conteudo))
            throw new ArgumentException("O conteúdo da mensagem está inválido.", nameof(conteudo));

        return new OutboxMessage()
        {
            Id = Guid.NewGuid(),
            Tipo = tipo,
            Conteudo = conteudo,
            CriadoEm = DateTime.UtcNow
        };
    }

    public void MarcarComoProcessada()
    {
        if (ProcessadoEm != null)
            throw new InvalidOperationException("A mensagem já foi processada.");

        ProcessadoEm = DateTime.UtcNow;
    }
}
