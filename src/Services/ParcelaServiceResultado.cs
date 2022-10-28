using RenegociacaoAPI.src.Models.DTOs;

namespace RenegociacaoAPI.src.Services
{
    public class ParcelaServiceResultado : ServiceResultadoBase
    {
        public List<ParcelaReadDTO> Parcelas { get; set; }

        public ParcelaServiceResultado(string msg, StatusServiceRetorno status)
        {
            this.Msg    = msg;
            this.Status = status;
            Parcelas    = new List<ParcelaReadDTO>();
        }        
    }
}