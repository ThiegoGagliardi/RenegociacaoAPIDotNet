using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RenegociacaoAPI.src.Models.DTOs
{
    public class ParcelaCreateDTO
    {

        [JsonIgnore]
        public ClienteCreateDTO Cliente { get; set; }

        [JsonIgnore]        
        public ContratoCreateDTO Contrato { get; set;}

        public int ClienteId { get; set; } 

        public string NumContrato { get; set; }

        public int NumParcela { get; set; }        

        public DateTime Vencimento { get; set; }

        public Decimal Valor { get; set; } 

        public string Status { get; set; }
    }
}