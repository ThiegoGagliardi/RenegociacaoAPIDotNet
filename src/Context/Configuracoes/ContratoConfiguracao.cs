using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RenegociacaoAPI.src.Models.Entities;

namespace RenegociacaoAPI.src.Context.Configuracoes
{
    public class ContratoConfiguracao : IEntityTypeConfiguration<Contrato>
    {
        public void Configure (EntityTypeBuilder<Contrato> builder)
        {
            builder.ToTable("Contratos");

            builder.HasKey( c => new {c.ClienteId, c.NumContrato});

            builder.HasOne( c => c.Cliente)
                   .WithMany(contrato => contrato.Contratos)
                   .HasForeignKey(contrato => contrato.ClienteId)                   
                   .OnDelete(DeleteBehavior.ClientNoAction);
        }        
    }
}