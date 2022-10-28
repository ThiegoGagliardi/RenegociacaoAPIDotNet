using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RenegociacaoAPI.src.Models.DTOs
{
    public class ParcelaReadDTO
    {          
        public ClienteReadDTO Cliente { get; set; }

       [JsonIgnore]
       
        public ContratoReadDTO Contrato { get; set;}

        public string NumContrato { get; set; }        

        public int NumParcela { get; set; }

        public DateTime Vencimento { get; set; }

        public Decimal Valor { get; set; } 

        public string Status { get; set; }
    }
}