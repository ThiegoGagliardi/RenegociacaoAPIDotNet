using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using RenegociacaoAPI.src.Models.DTOs;
using RenegociacaoAPI.src.Models.Entities;
using RenegociacaoAPI.src.Context;
using RenegociacaoAPI.src.mapeamentoDados;

namespace RenegociacaoAPI.src.Services
{
    public class ParcelaService
    {
        private RenegociacaoContext _context;

        public ParcelaService(RenegociacaoContext contexto)
        {
            this._context = contexto;            
        }

        public ParcelaServiceResultado CriarParcela(ParcelaCreateDTO novaParcelaDTO)
        {
            var clienteLocalizado  = _context.Clientes.FirstOrDefault( c => c.Id == novaParcelaDTO.ClienteId);

            if (clienteLocalizado is null){

                return new ParcelaServiceResultado ("Cliente não localizado", StatusServiceRetorno.Erro);
            }

            var contratoLocalizado = _context.Contratos.FirstOrDefault(c => c.NumContrato == novaParcelaDTO.NumContrato && 
                                                                            c.ClienteId   == novaParcelaDTO.ClienteId);

            if (contratoLocalizado is null){

                return new ParcelaServiceResultado ("Contrato não cadastrado", StatusServiceRetorno.Erro);
            }           

            var parcelaLocalizada = _context.Parcelas.FirstOrDefault(p => p.NumContrato == novaParcelaDTO.NumContrato && 
                                                                          p.NumParcela == novaParcelaDTO.NumParcela &&
                                                                          p.ClienteId  == novaParcelaDTO.ClienteId);

            if (!(parcelaLocalizada is null)){

                return new ParcelaServiceResultado ("Parcela já cadastrada", StatusServiceRetorno.Erro);
            }            

            try{

                ParcelaMapeamento mapeamento = new ParcelaMapeamento(novaParcelaDTO, _context);
                Parcela novaParcela          = mapeamento.RetornaParcela();

                _context.Parcelas.Add(novaParcela);
                _context.SaveChanges();

                mapeamento = new ParcelaMapeamento(novaParcela, _context);

                ParcelaServiceResultado resultado = new ParcelaServiceResultado( "Cliente cadastrado", StatusServiceRetorno.Criado);
                resultado.Parcelas.Add(mapeamento.RetornaParcelaDTO());

                return resultado;

            } catch (Exception e){
                throw new Exception ($"Erro ao cadastrar Parcela, { e.Message }");
            }          
        }

        public ParcelaServiceResultado LocalizarParcelaPorIdNumContratoNumParc(int id, string numContrato, int numParcela)
        {
            try{

                var parcelaConsulta = _context.Parcelas.Where(p => p.ClienteId   == id && 
                                                              p.NumContrato == numContrato &&
                                                              p.NumParcela  == numParcela)
                                                              .Include(p => p.Contrato)
                                                              .Include(p => p.Cliente);

                Parcela parcelaLocalizado =  parcelaConsulta.FirstOrDefault();

                if (parcelaLocalizado is null)
                {
                    return new ParcelaServiceResultado($"Parcela {numContrato}/{numParcela} não localizado", StatusServiceRetorno.Nao_Localizado);
                }

                ParcelaMapeamento mapeamento = new ParcelaMapeamento(parcelaLocalizado, _context);

                ParcelaReadDTO parcelaLocalizadoDTO = mapeamento.RetornaParcelaDTO();

                var resultado =  new ParcelaServiceResultado ("Parcela localizado",StatusServiceRetorno.Localizado);
                
                resultado.Parcelas.Add(parcelaLocalizadoDTO);

                return resultado;

            } catch (Exception e){
                throw new Exception ($"Erro ao localizar Parcela por num contrato, { e.Message }");
            }
        }

        public ParcelaServiceResultado LocalizarParcelas()
        {
            try{

                var parcelasLocalizadas = _context.Parcelas.Include(p => p.Contrato).Include(p => p.Cliente).ToList();

                if (parcelasLocalizadas is null)
                {
                    return new ParcelaServiceResultado($"Contrato não cadastrados", StatusServiceRetorno.Erro);
                }

                ParcelaServiceResultado resultado =  new ParcelaServiceResultado ("Parcelas localizadas",StatusServiceRetorno.Localizado);                

                foreach (var parcela in parcelasLocalizadas){

                    ParcelaMapeamento mapeamento = new ParcelaMapeamento(parcela, _context);
                    resultado.Parcelas.Add(mapeamento.RetornaParcelaDTO());
                }

                return resultado;

            } catch (Exception e){
                throw new Exception($"Erro ao localizar parcela, { e.Message }");
            }
        }

        public ParcelaServiceResultado AtualizarParcelas(int id, string numContrato, int numParcela, ParcelaUpdateDTO parcelaAlteradoDTO)
        {
            try{
                Parcela parcelaAlterada = _context.Parcelas.FirstOrDefault(p => p.ClienteId   == id && 
                                                                                p.NumContrato == numContrato &&
                                                                                p.NumParcela  == numParcela);

                if (parcelaAlterada is null){

                    return new ParcelaServiceResultado($"Parcela c{numContrato}/{numParcela} não localizado para atualização", StatusServiceRetorno.Nao_Localizado);
                }                

                parcelaAlterada.Valor      = parcelaAlteradoDTO.Valor;
                parcelaAlterada.Vencimento = parcelaAlteradoDTO.Vencimento;
                parcelaAlterada.Status     = ParcelaMapeamento.verificarParcelaStatus(parcelaAlteradoDTO.Status);
                
                _context.Parcelas.Update(parcelaAlterada);
                _context.SaveChanges();

                var resultado = new ParcelaServiceResultado($"Parcela com número de contrato {numContrato}/{numParcela} do cliente {id} atualizado", StatusServiceRetorno.Atualizado);
                return resultado;

            } catch (Exception e){

                throw new Exception($"Erro ao atualizar parcela {numContrato}/{numParcela}, { e.Message }");
            }
        }

        public ParcelaServiceResultado DeletarParcela(int id, string numContrato, int numParc)
        {
            try{
                Parcela parcelaDeletado = _context.Parcelas.FirstOrDefault(p => p.ClienteId == id && 
                                                                                p.NumContrato == numContrato &&
                                                                                p.NumParcela  == numParc);

                if (parcelaDeletado is null){

                    return new ParcelaServiceResultado($"Parcela com número {numContrato}/{numParc} do cliente id {id} não localizada para deleção", StatusServiceRetorno.Nao_Localizado);
                }

                _context.Parcelas.Remove(parcelaDeletado);
                _context.SaveChanges();

                var resultado = new ParcelaServiceResultado($"Parcela com  número {numContrato}/{numParc} do cliente id {id} atualizado", StatusServiceRetorno.Deletado);
                return resultado;

            } catch (Exception e){

                throw new Exception($"Erro ao deletar parcela Id {numContrato}/{numParc} , { e.Message }");
            }
        }        
    }
}