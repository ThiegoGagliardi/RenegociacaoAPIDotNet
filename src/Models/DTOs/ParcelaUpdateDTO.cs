using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenegociacaoAPI.src.Models.DTOs
{
    public class ParcelaUpdateDTO
    {
        public DateTime Vencimento { get; set; }

        public Decimal Valor { get; set; }

        public string Status { get; set; }        
    }
}