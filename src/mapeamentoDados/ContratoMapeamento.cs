using RenegociacaoAPI.src.Models.DTOs;
using RenegociacaoAPI.src.Models.Entities;
using RenegociacaoAPI.src.Context;

namespace RenegociacaoAPI.src.mapeamentoDados
{
    public class ContratoMapeamento
    {
        private Contrato _contrato;

        private ContratoCreateDTO _contratoDTO;

        private RenegociacaoContext _contexto;

        public ContratoMapeamento(Contrato contrato, RenegociacaoContext contexto)
        {
            this._contrato        = contrato;
            this._contratoDTO     = new ContratoCreateDTO();
            this._contexto        = contexto;
        }

        public ContratoMapeamento(ContratoCreateDTO contratoCreateDTO, RenegociacaoContext contexto)
        {
            this._contrato    = new Contrato();
            this._contratoDTO = contratoCreateDTO;
            this._contexto    = contexto;
        }

        public Contrato RetornaContrato()
        {
            Contrato contrato  = new Contrato();
                        
            contrato.Cliente         = _contexto.Clientes.FirstOrDefault(c => c.Id == _contratoDTO.ClienteId);
            contrato.ClienteId       = contrato.Cliente.Id; 
            contrato.NumContrato     = _contratoDTO.NumContrato;              
            contrato.Plano           = _contratoDTO.Plano;
            contrato.Multa           = _contratoDTO.Multa;
            contrato.Juros           = _contratoDTO.Juros;            
            contrato.DataContratacao = _contratoDTO.DataContratacao;

            return contrato;
        }

        public ContratoReadDTO RetornaContratoDTO()
        {
            ContratoReadDTO contratoDTO = new ContratoReadDTO();             

            ClienteMapeamento  clienteMapeamento = new ClienteMapeamento(_contrato.Cliente, _contexto);
            
            contratoDTO.Cliente         = clienteMapeamento.RetornaClienteDTO();
            contratoDTO.NumContrato     = _contrato.NumContrato;              
            contratoDTO.Plano           = _contrato.Plano;
            contratoDTO.Multa           = _contrato.Multa;
            contratoDTO.Juros           = _contrato.Juros;                          
            contratoDTO.DataContratacao = _contrato.DataContratacao;

            return contratoDTO;
        }       
    }
}