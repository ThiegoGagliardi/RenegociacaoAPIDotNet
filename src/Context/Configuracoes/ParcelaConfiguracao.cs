using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenegociacaoAPI.src.Models.Entities;


namespace RenegociacaoAPI.src.Context.Configuracoes
{
    public class ParcelaConfiguracao : IEntityTypeConfiguration<Parcela>
    {
        public void Configure(EntityTypeBuilder<Parcela> builder)
        {
            builder.ToTable("Parcelas");

            builder.HasKey( k => new { k.ClienteId, k.NumContrato, k.NumParcela});

            builder.HasOne(c => c.Cliente )
                   .WithMany(p => p.Parcelas)
                   .HasForeignKey( p => p.ClienteId)
                   .HasPrincipalKey(p => p.Id)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Contrato )
                   .WithMany(p => p.Parcelas)
                   .HasForeignKey(p => p.NumContrato)
                   .HasPrincipalKey(p => p.NumContrato)                   
                   .OnDelete(DeleteBehavior.NoAction);

        }
    }
}