// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RenegociacaoAPI.src.Context;

#nullable disable

namespace RenegociacaoAPI.Migrations
{
    [DbContext(typeof(RenegociacaoContext))]
    partial class RenegociacaoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RenegociacaoAPI.SRC.Models.Entities.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Cpf")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("RenegociacaoAPI.SRC.Models.Entities.Contrato", b =>
                {
                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<string>("NumContrato")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DataContratacao")
                        .HasColumnType("datetime2");

                    b.Property<double>("Juros")
                        .HasColumnType("float");

                    b.Property<double>("Multa")
                        .HasColumnType("float");

                    b.Property<int>("Plano")
                        .HasColumnType("int");

                    b.HasKey("ClienteId", "NumContrato");

                    b.ToTable("Contratos", (string)null);
                });

            modelBuilder.Entity("RenegociacaoAPI.SRC.Models.Entities.Parcela", b =>
                {
                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<string>("NumContrato")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("NumParcela")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Vencimento")
                        .HasColumnType("datetime2");

                    b.HasKey("ClienteId", "NumContrato", "NumParcela");

                    b.HasIndex("NumContrato");

                    b.ToTable("Parcelas", (string)null);
                });

            modelBuilder.Entity("RenegociacaoAPI.SRC.Models.Entities.Contrato", b =>
                {
                    b.HasOne("RenegociacaoAPI.SRC.Models.Entities.Cliente", "Cliente")
                        .WithMany("Contratos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("RenegociacaoAPI.SRC.Models.Entities.Parcela", b =>
                {
                    b.HasOne("RenegociacaoAPI.SRC.Models.Entities.Cliente", "Cliente")
                        .WithMany("Parcelas")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("RenegociacaoAPI.SRC.Models.Entities.Contrato", "Contrato")
                        .WithMany("Parcelas")
                        .HasForeignKey("NumContrato")
                        .HasPrincipalKey("NumContrato")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Contrato");
                });

            modelBuilder.Entity("RenegociacaoAPI.SRC.Models.Entities.Cliente", b =>
                {
                    b.Navigation("Contratos");

                    b.Navigation("Parcelas");
                });

            modelBuilder.Entity("RenegociacaoAPI.SRC.Models.Entities.Contrato", b =>
                {
                    b.Navigation("Parcelas");
                });
#pragma warning restore 612, 618
        }
    }
}
