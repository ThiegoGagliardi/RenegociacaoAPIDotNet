using Microsoft.EntityFrameworkCore;
using RenegociacaoAPI.src.Context;
using RenegociacaoAPI.src.Models.Entities;
using RenegociacaoAPI.src.Models.DTOs;


namespace RenegociacaoAPI.src.Services
{
    public class CalculoService
    {
        private RenegociacaoContext _context; 
        public CalculoService(RenegociacaoContext contexto)
        {
            this._context  = contexto;
        }

        public CalculoServiceResultado EfetuaCalculo (int id, string numContrato)
        {
            try{

                var clienteLocalizado  = _context.Clientes.FirstOrDefault(c => c.Id == id);

                if (clienteLocalizado is null){

                    return new CalculoServiceResultado ("Cliente não localizado", StatusServiceRetorno.Erro);
                }

                var contratoLocalizado = _context.Contratos.FirstOrDefault(c => c.NumContrato == numContrato && 
                                                                            c.ClienteId   == id);

                if (contratoLocalizado is null){

                    return new CalculoServiceResultado ("Contrato não cadastrado", StatusServiceRetorno.Erro);
                }                   

                var parcelaLocalizado = _context.Parcelas.Where(p => p.ClienteId   == id && 
                                                                     p.NumContrato == numContrato &&
                                                                     p.Status     != ParcelaStatus.Paga).Include(p => p.Contrato).ToList();

                if (parcelaLocalizado is null)
                {
                    return new CalculoServiceResultado($"Parcelas não localizadas para o Contrato {numContrato}", StatusServiceRetorno.Nao_Localizado);
                }

                var resultado =  new CalculoServiceResultado ("Parcela localizado",StatusServiceRetorno.Localizado);
                
                foreach (var p in parcelaLocalizado){

                    resultado.Parcelas.Add(new ParcelaCalculoDTO(p.Cliente,p.Contrato,p));
                }

                return resultado;

            } catch (Exception e){
                throw new Exception ($"Erro ao localizar Parcela por num contrato, { e.Message }");
            }            
        }        
        
    }
}