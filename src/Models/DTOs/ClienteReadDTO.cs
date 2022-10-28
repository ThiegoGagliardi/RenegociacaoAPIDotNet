using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenegociacaoAPI.src.Models.DTOs
{
    public class ClienteReadDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }   
    }
}