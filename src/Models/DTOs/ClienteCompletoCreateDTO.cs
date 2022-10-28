using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenegociacaoAPI.src.Models.DTOs
{
    public class ClienteCompletoCreateDTO
    {
        
        public string Nome { get; set; }

        public string Cpf { get; set; }

        public IEnumerable<ContratoCreateDTO> Contratos { get; set; }
    }
}