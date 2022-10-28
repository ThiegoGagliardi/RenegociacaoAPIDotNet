using RenegociacaoAPI.src.Models.DTOs;
using RenegociacaoAPI.src.Models.Entities;
using RenegociacaoAPI.src.Context;

namespace RenegociacaoAPI.src.mapeamentoDados
{
    public class ClienteMapeamento
    {
        private Cliente _cliente;

        private ClienteCreateDTO _clienteDTO;

        private RenegociacaoContext _contexto;       
        
        public ClienteMapeamento(Cliente cliente, RenegociacaoContext contexto)
        {
            this._cliente    = cliente;            
            this._contexto   = contexto;
        }

        public ClienteMapeamento(ClienteCreateDTO clienteCreateDTO)
        {
            this._cliente    = new Cliente ();
            this._clienteDTO = clienteCreateDTO;
        }

        public Cliente RetornaCliente()
        {
            return new Cliente {
                Nome = _clienteDTO.Nome,
                Cpf  = _clienteDTO.Cpf
            };
        }

        public ClienteReadDTO RetornaClienteDTO()
        {
            ClienteReadDTO clienteDTO =  new ClienteReadDTO {
                
                Id   = _cliente.Id,
                Nome = _cliente.Nome,
                Cpf  = _cliente.Cpf
            };
            
            return clienteDTO;
        }        
    }
}