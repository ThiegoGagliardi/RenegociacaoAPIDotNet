using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace RenegociacaoAPI.src.Models.DTOs
{
    public class ContratoReadDTO
    {       
        public ClienteReadDTO Cliente { get; set; }
        
        public string NumContrato { get; set; }

        public int Plano { get; set; }

        public double Multa{ get; set;}

        public double Juros {get; set;}          

        public DateTime DataContratacao { get; set;}
    }
}