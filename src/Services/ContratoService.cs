using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using RenegociacaoAPI.src.Models.DTOs;
using RenegociacaoAPI.src.Models.Entities;
using RenegociacaoAPI.src.Context;
using RenegociacaoAPI.src.mapeamentoDados;

namespace RenegociacaoAPI.src.Services
{
    public class ContratoService
    {
        private RenegociacaoContext _context;

        public ContratoService(RenegociacaoContext contexto)
        {
            this._context = contexto;            
        }

        public ContratoServiceResultado CriarUmContrato( ContratoCreateDTO novoContratoDTO)
        {
            var clienteLocalizado  = _context.Clientes.FirstOrDefault(cli => cli.Id == novoContratoDTO.ClienteId);

            if (clienteLocalizado is null){

                return new ContratoServiceResultado ("Cliente não localizado", StatusServiceRetorno.Erro);
            }

            var contratoLocalizado =  _context.Contratos.FirstOrDefault(c => c.NumContrato == novoContratoDTO.NumContrato && 
                                                                             c.ClienteId   == clienteLocalizado.Id);

            if (!(contratoLocalizado is null)){

                return new ContratoServiceResultado ("Contrato já cadastrado", StatusServiceRetorno.Erro);
            }

            try{

                ContratoMapeamento mapeamento = new ContratoMapeamento(novoContratoDTO, _context);
                Contrato novoContrato         = mapeamento.RetornaContrato();

                _context.Contratos.Add(novoContrato);
                _context.SaveChanges();

                mapeamento = new ContratoMapeamento(novoContrato,  _context);

                ContratoServiceResultado resultado = new ContratoServiceResultado( "Cliente cadastrado", StatusServiceRetorno.Criado);
                resultado.Contratos.Add(mapeamento.RetornaContratoDTO());

                return resultado;
            } catch (Exception e){
                throw new Exception ($"Erro ao cadastrar contrato, { e.Message }");
            }          
        }

        public ContratoServiceResultado LocalizarContratoPorIdNumContrato(int id, string numContrato)
        {
            try{

                var contratoConsulta = _context.Contratos.Where(c => c.ClienteId == id && c.NumContrato == numContrato)
                                                                .Include(c => c.Cliente);

                Contrato contratoLocalizado = contratoConsulta.FirstOrDefault();

                if (contratoLocalizado is null)
                {
                    return new ContratoServiceResultado($"Contrato  com id {id} não localizado", StatusServiceRetorno.Nao_Localizado);
                }

                ContratoMapeamento mapeamento = new ContratoMapeamento(contratoLocalizado, _context);

                ContratoReadDTO contratoLocalizadoDTO = mapeamento.RetornaContratoDTO();

                var resultado =  new ContratoServiceResultado ("Contrato localizado",StatusServiceRetorno.Localizado);
                
                resultado.Contratos.Add(contratoLocalizadoDTO);

                return resultado;

            } catch (Exception e){
                throw new Exception ($"Erro ao localizar contrato por Id, { e.Message }");
            }
        }

        public ContratoServiceResultado LocalizarContratos()
        {
            try{

                var contratosLocalizados = _context.Contratos.Include(c => c.Cliente).ToList();

                if (contratosLocalizados is null)
                {
                    return new ContratoServiceResultado($"Contrato não cadastrados", StatusServiceRetorno.Erro);
                }

                ContratoServiceResultado resultado =  new ContratoServiceResultado ("Contratos localizados",StatusServiceRetorno.Localizado);                

                foreach (var contrato in contratosLocalizados){

                    ContratoMapeamento mapeamento = new ContratoMapeamento(contrato, _context);
                    resultado.Contratos.Add(mapeamento.RetornaContratoDTO());
                }

                return resultado;

            } catch (Exception e){
                throw new Exception($"Erro ao localizar contrato, { e.Message }");
            }
        }

        public ContratoServiceResultado AtualizarContrato(int id, string numContrato, ContratoCreateDTO contratoAlteradoDTO)
        {
            try{
                Contrato contratoAlterado = _context.Contratos.FirstOrDefault(c => c.ClienteId == id && c.NumContrato == numContrato);

                if (contratoAlterado is null){

                    return new ContratoServiceResultado($"Clientes com id {id} não localizado para atualização", StatusServiceRetorno.Nao_Localizado);
                }

                contratoAlterado.NumContrato = contratoAlteradoDTO.NumContrato;
                contratoAlterado.Plano       = contratoAlteradoDTO.Plano;
                contratoAlterado.Juros       = contratoAlteradoDTO.Juros;
                contratoAlterado.Multa       = contratoAlteradoDTO.Multa;

                _context.Contratos.Update(contratoAlterado);
                _context.SaveChanges();

                var resultado = new ContratoServiceResultado($"Contrato com número de contrato {numContrato} do cliente {id} atualizado", StatusServiceRetorno.Atualizado);
                return resultado;

            } catch (Exception e){

                throw new Exception($"Erro ao atualizar cliente Id {id}/{numContrato}, { e.Message }");
            }
        }

        public ContratoServiceResultado DeletarContrato(int id, string numContrato)
        {
            try{

                var contratoConsulta = _context.Contratos.Where(c => c.ClienteId == id && c.NumContrato == numContrato)
                                                         .Include(c => c.Cliente)
                                                         .Include( p => p.Parcelas.Where( p => p.ClienteId == id && 
                                                                                          p.NumContrato == numContrato));

                Contrato contratoDeletado = contratoConsulta.FirstOrDefault();                
                
                if (contratoDeletado is null){

                    return new ContratoServiceResultado($"Contrato com número {numContrato} do cliente id {id} não localizado para deleção", StatusServiceRetorno.Nao_Localizado);
                }

                foreach ( var p in contratoDeletado.Parcelas)
                {
                    _context.Parcelas.Remove(p);                                       
                }

                _context.Contratos.Remove(contratoDeletado);
                _context.SaveChanges();

                var resultado = new ContratoServiceResultado($"Contrato com número {numContrato}  do cliente id {id} deletado", StatusServiceRetorno.Deletado);
                return resultado;

            } catch (Exception e){

                throw new Exception($"Erro ao deletar com número {numContrato} do cliente id {id} , { e.Message }");
            }
        }         
    }
}