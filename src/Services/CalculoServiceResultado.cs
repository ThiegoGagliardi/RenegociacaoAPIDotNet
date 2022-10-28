using RenegociacaoAPI.src.Models.DTOs;

namespace RenegociacaoAPI.src.Services
{
    public class CalculoServiceResultado : ServiceResultadoBase
    {
        public List<ParcelaCalculoDTO> Parcelas { get; set; }

        public CalculoServiceResultado(string msg, StatusServiceRetorno status)
        {
            this.Msg    = msg;
            this.Status = status;
            Parcelas    = new List<ParcelaCalculoDTO>();
        }        

    }
}