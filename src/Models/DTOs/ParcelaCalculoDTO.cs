using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RenegociacaoAPI.src.Models.Entities;

namespace RenegociacaoAPI.src.Models.DTOs
{
    public class ParcelaCalculoDTO
    {

        public string NomeCliente { get; set; }           

        public string  Contrato { get; set;}

        public int NumParc { get; set; }

        public DateTime Vencimento { get; set; }

        public int Atraso { get; set; }

        public decimal Valor { get; set; }

        public decimal TotalMulta { get; set; }

        public decimal TotalJuros { get; set; }

        public decimal TotalDesagio { get; set; }

        public decimal TotalValor { get; set; }

        public ParcelaCalculoDTO(Cliente cliente, Contrato contrato, Parcela parcela)
        {

            this.NomeCliente = cliente.Nome;
            this.Contrato    = contrato.NumContrato;
            this.NumParc     = parcela.NumParcela;
            this.Vencimento  = parcela.Vencimento;
            this.Atraso      = (DateTime.Today.Subtract(parcela.Vencimento).Days)/30;
            this.Valor       = parcela.Valor;            

            if (this.Atraso > 0) {           
                this.TotalMulta  = (this.Valor * (Convert.ToDecimal(contrato.Multa)/100));     
                this.TotalJuros  = (this.Valor * (Convert.ToDecimal(contrato.Juros)/100)) * this.Atraso;
            }else{
                this.TotalDesagio = (this.Valor * (Convert.ToDecimal(contrato.Juros)/100)) * this.Atraso;
            }

            this.TotalValor  = this.Valor  + this.TotalMulta + this.TotalJuros + this.TotalDesagio;
            
        }     
    }
}