using FluentAssertions;
using PagBoleto.Domain.Entities;
using PagBoleto.Domain.Enums;

namespace PagBoleto.Domain.Tests;

public class BoletoTests
{
    private const string LinhaDigitavelValida = "34191.79001 01043.510047 91020.150008 1 96610000014000";
    private static DateOnly VencimentoValido() => DateOnly.FromDateTime(DateTime.UtcNow).AddDays(5);

    #region Criar

    [Fact]
    public void Criar_LinhaDigitavelVazia_DeveLancarException()
    {
        var linhaDigitavel = string.Empty;

        var act = () => Boleto.Criar(linhaDigitavel, 150m, VencimentoValido());

        act.Should().Throw<ArgumentException>().WithParameterName(nameof(linhaDigitavel));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    [InlineData(-0.01)]
    public void Criar_ValorZeroNegativo_DeveLancarException(decimal valor)
    {
        var act = () => Boleto.Criar(LinhaDigitavelValida, valor, VencimentoValido());

        act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("valor");
    }

    [Fact]
    public void Criar_ComVencimentoPassado_DeveLancarException()
    {
        var vencimento = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(-1);

        var act = () => Boleto.Criar(LinhaDigitavelValida, 150m, vencimento);

        act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("vencimento");
    }

    [Fact]
    public void Criar_ComVencimentoHoje_DeveCriarBoleto()
    {
        var vencimento = DateOnly.FromDateTime(DateTime.UtcNow);

        var boleto = Boleto.Criar(LinhaDigitavelValida, 150m, vencimento);

        boleto.Vencimento.Should().Be(vencimento);
        boleto.Status.Should().Be(StatusBoleto.Pendente);
    }

    [Fact]
    public void Criar_ComDadosValidos_DeveCriarBoleto()
    {
        var vencimento = VencimentoValido();

        var boleto = Boleto.Criar(LinhaDigitavelValida, 150m, vencimento);

        boleto.Should().NotBeNull();
        boleto.Id.Should().NotBeEmpty();
        boleto.LinhaDigitavel.Should().Be(LinhaDigitavelValida);
        boleto.Valor.Should().Be(150m);
        boleto.Vencimento.Should().Be(vencimento);
        boleto.Status.Should().Be(StatusBoleto.Pendente);
        boleto.TentativasProcessamento.Should().Be(0);
        boleto.MotivoFalha.Should().BeEmpty();
        boleto.CriadoEm.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        boleto.AtualizadoEm.Should().BeNull();
    }

    #endregion

    #region MarcarEmProcessamento

    [Fact]
    public void MarcarEmProcessamento_QuandoPendente_DeveSerSucesso()
    {
        var boleto = Boleto.Criar(LinhaDigitavelValida, 150m, VencimentoValido());

        boleto.MarcarEmProcessamento();

        boleto.Status.Should().Be(StatusBoleto.EmProcessamento);
        boleto.AtualizadoEm.Should().NotBeNull();
    }

    [Fact]
    public void MarcarEmProcessamento_JaEmProcessamento_DeveLancarException()
    {
        var boleto = Boleto.Criar(LinhaDigitavelValida, 150m, VencimentoValido());

        boleto.MarcarEmProcessamento();

        var act = () => boleto.MarcarEmProcessamento();

        act.Should().Throw<InvalidOperationException>();
    }

    #endregion
}
