using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenegociacaoAPI.src.Models.Entities
{
    public class Parcela
    {

        public int ClienteId { get; set; }

        public string NumContrato { get; set; }        

        public int NumParcela { get; set; }

        public DateTime Vencimento { get; set; }

        public Decimal Valor { get; set; }

        [NotMapped]
        public Contrato Contrato { get; set; }

        [NotMapped]
        public Cliente Cliente { get; set; }
        
        public ParcelaStatus Status { get; set; }
                
    }
}