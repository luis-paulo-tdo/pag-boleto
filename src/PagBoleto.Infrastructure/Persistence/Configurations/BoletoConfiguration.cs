using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PagBoleto.Domain.Entities;

namespace PagBoleto.Infrastructure.Persistence.Configurations;

public class BoletoConfiguration : IEntityTypeConfiguration<Boleto>
{
    public void Configure(EntityTypeBuilder<Boleto> builder)
    {
        builder.ToTable("boletos");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(b => b.LinhaDigitavel)
            .HasColumnName("linha_digitavel")
            .HasMaxLength(54)
            .IsRequired();

        builder.Property(b => b.Valor)
            .HasColumnName("valor")
            .HasColumnType("numeric(18,2)")
            .IsRequired();

        builder.Property(b => b.Vencimento)
            .HasColumnName("vencimento")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(b => b.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(b => b.TentativasProcessamento)
            .HasColumnName("tentativas_processamento")
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(b => b.MotivoFalha)
            .HasColumnName("motivo_falha")
            .HasMaxLength(500);

        builder.Property(b => b.CriadoEm)
            .HasColumnName("criado_em")
            .IsRequired();

        builder.Property(b => b.AtualizadoEm)
            .HasColumnName("atualizado_em");

        builder.HasIndex(b => b.LinhaDigitavel)
            .IsUnique()
            .HasDatabaseName("ix_boletos_linha_digitavel");

        builder.HasIndex(b => new { b.Status, b.Vencimento })
            .HasDatabaseName("ix_boletos_status_vencimento");
    }
}
