using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PagBoleto.Domain.Entities;

namespace PagBoleto.Infrastructure.Persistence.Configurations;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outbox_messages");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(m => m.Tipo)
            .HasColumnName("tipo")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(m => m.Conteudo)
            .HasColumnName("conteudo")
            .HasColumnType("jsonb")
            .IsRequired();

        builder.Property(m => m.ProcessadoEm)
            .HasColumnName("processado_em");

        builder.Property(m => m.CriadoEm)
            .HasColumnName("criado_em")
            .IsRequired();

        builder.HasIndex(m => new { m.ProcessadoEm, m.CriadoEm })
            .HasDatabaseName("ix_outbox_message_processado_criado_em");
    }
}
