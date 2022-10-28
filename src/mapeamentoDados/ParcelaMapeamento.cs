using RenegociacaoAPI.src.Models.DTOs;
using RenegociacaoAPI.src.Models.Entities;
using RenegociacaoAPI.src.Context;

namespace RenegociacaoAPI.src.mapeamentoDados
{
    public class ParcelaMapeamento
    {
        private Parcela _parcela;

        private ParcelaCreateDTO _parcelaDTO;

        private RenegociacaoContext _contexto;

        public ParcelaMapeamento(Parcela parcela, RenegociacaoContext contexto)
        {
            this._parcela     = parcela;
            this._parcelaDTO  = new ParcelaCreateDTO();
            this._contexto    = contexto;
        }

        public ParcelaMapeamento(Parcela parcela, ParcelaCreateDTO parcelaCreateDTO, RenegociacaoContext contexto)
        {
            this._parcela     = parcela;
            this._parcelaDTO  = parcelaCreateDTO;
            this._contexto    = contexto;
        }        

        public ParcelaMapeamento(ParcelaCreateDTO parcelaCreateDTO, RenegociacaoContext contexto)
        {
            this._parcela     = new Parcela();
            this._parcelaDTO = parcelaCreateDTO;
            this._contexto    = contexto;
        }

        public Parcela RetornaParcela()
        {
            _parcela.Cliente         = _contexto.Clientes.FirstOrDefault(c => c.Id == _parcelaDTO.ClienteId); 
            _parcela.Contrato        = _contexto.Contratos.FirstOrDefault(c => c.ClienteId == _parcelaDTO.ClienteId &&
                                                                               c.NumContrato == _parcelaDTO.NumContrato); 
            _parcela.ClienteId       = _parcelaDTO.ClienteId;                       
            _parcela.NumContrato     = _parcelaDTO.NumContrato;
            _parcela.NumParcela      = _parcelaDTO.NumParcela;
            _parcela.Vencimento      = _parcelaDTO.Vencimento;
            _parcela.Valor           = _parcelaDTO.Valor;                        
            _parcela.Status          = verificarParcelaStatus(_parcelaDTO.Status);
 
            return _parcela;
        }     

        public static ParcelaStatus verificarParcelaStatus(string status)
        {
            ParcelaStatus codStatus;

            switch (status.ToUpper()){
                case "VENCIDA"   : codStatus = ParcelaStatus.Vencida; break;
                case "VINCENDA"  : codStatus = ParcelaStatus.Vincenda; break;
                case "PAGA"      : codStatus = ParcelaStatus.Paga; break;
                default          : codStatus = ParcelaStatus.Inativa; break;
            }

            return codStatus;
        }

        public ParcelaReadDTO RetornaParcelaDTO()
        {
            ParcelaReadDTO parcelaDTO = new ParcelaReadDTO();             

            ClienteMapeamento  clienteMapeamento =  new ClienteMapeamento(_parcela.Cliente, _contexto);
            parcelaDTO.Cliente         = clienteMapeamento.RetornaClienteDTO();

            ContratoMapeamento  contratoMapeamento =  new ContratoMapeamento(_parcela.Contrato,_contexto);
            parcelaDTO.Contrato                    = contratoMapeamento.RetornaContratoDTO();
            parcelaDTO.NumContrato     = _parcela.NumContrato;
            parcelaDTO.NumParcela      = _parcela.NumParcela;
            parcelaDTO.Valor           = _parcela.Valor;
            parcelaDTO.Vencimento      = _parcela.Vencimento;
            parcelaDTO.Status          = verificarParcelaStatusDescricao(_parcela.Status);

            return parcelaDTO;
        }

        public  static string verificarParcelaStatusDescricao(ParcelaStatus status)
        {
            string descricaoStatus;

            switch (status){
                case  ParcelaStatus.Vencida  : descricaoStatus = "VENCIDA"   ; break;
                case  ParcelaStatus.Vincenda : descricaoStatus = "VINCENDA"  ; break;
                case  ParcelaStatus.Paga     : descricaoStatus = "PAGA"      ; break;
                case  ParcelaStatus.Inativa  : descricaoStatus = "INATIVA"   ; break;
                default                      :  descricaoStatus = ""         ; break;
            }

            return descricaoStatus;
        }
    }
}