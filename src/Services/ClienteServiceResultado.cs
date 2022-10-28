using RenegociacaoAPI.src.Models.DTOs;

namespace RenegociacaoAPI.src.Services
{
    public class ClienteServiceResultado :ServiceResultadoBase
    {
        public List<ClienteReadDTO> Clientes { get; set; }

        public ClienteServiceResultado(string msg, StatusServiceRetorno status)
        {
            this.Msg    = msg;
            this.Status = status;
            Clientes    = new List<ClienteReadDTO>();
        }
    }
}