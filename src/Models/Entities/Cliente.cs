using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenegociacaoAPI.src.Models.Entities
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public IEnumerable<Contrato> Contratos { get; set; }

        public IEnumerable<Parcela> Parcelas { get; set; }
    }
}