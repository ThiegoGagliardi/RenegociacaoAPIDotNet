using RenegociacaoAPI.src.Models.DTOs;

namespace RenegociacaoAPI.src.Services
{
    public class ContratoServiceResultado : ServiceResultadoBase
    {
        public List<ContratoReadDTO> Contratos { get; set; }

        public ContratoServiceResultado(string msg, StatusServiceRetorno status)
        {
            this.Msg    = msg;
            this.Status = status;
            Contratos   = new List<ContratoReadDTO>();
        }        
    }
}