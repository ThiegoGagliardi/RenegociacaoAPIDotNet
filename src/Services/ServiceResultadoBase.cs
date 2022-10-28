using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenegociacaoAPI.src.Services
{
    public abstract class ServiceResultadoBase
    {
        public string Msg { get; set;}
        public StatusServiceRetorno Status { get; set; }
    }
}