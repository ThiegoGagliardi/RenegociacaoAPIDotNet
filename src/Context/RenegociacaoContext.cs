using Microsoft.EntityFrameworkCore;
using RenegociacaoAPI.src.Models.Entities;
using RenegociacaoAPI.src.Context.Configuracoes;

namespace RenegociacaoAPI.src.Context
{
    public class RenegociacaoContext : DbContext
    {

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Parcela> Parcelas { get; set; }

        public DbSet<Contrato> Contratos { get; set; }

        public RenegociacaoContext( DbContextOptions opcoes) : base(opcoes)
        {
            
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContratoConfiguracao());

            modelBuilder.ApplyConfiguration(new ParcelaConfiguracao());
        }     
        
    }
}
