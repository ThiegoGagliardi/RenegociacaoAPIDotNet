using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenegociacaoAPI.src.Models.Entities
{
    public class Contrato
    {

        public int ClienteId { get; set; }

        public string NumContrato { get; set; }

        public int Plano { get; set; }

        public DateTime DataContratacao { get; set;}

        public double Multa { get; set; }

        public double Juros { get; set; }

        [NotMapped]
        public Cliente Cliente { get; set; }
       
       [NotMapped]
        public List<Parcela> Parcelas { get; set;}

        public Contrato()
        {
            Parcelas =  new List<Parcela>();            
        }
    }
}