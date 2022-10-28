using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RenegociacaoAPI.src.Models.DTOs
{
    public class ContratoCreateDTO
    {
        [JsonIgnore]
        public ClienteReadDTO  Cliente { get; set; }

        public int ClienteId { get; set; }

        public string NumContrato { get; set; }        

        public double Multa{ get; set;}

        public double Juros {get; set;}        

        public int Plano { get; set; }

        public DateTime DataContratacao { get; set;}        
    }
}