using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using RenegociacaoAPI.src.Models.DTOs;
using RenegociacaoAPI.src.Models.Entities;
using RenegociacaoAPI.src.Context;
using RenegociacaoAPI.src.Services;
using RenegociacaoAPI.src.mapeamentoDados;

namespace RenegociacaoAPI.src.Controllers
{
    [Route("[controller]")]
    public class ParcelaController : Controller
    {
        private readonly ParcelaService _parcelaService;

        public ParcelaController(RenegociacaoContext contexto)
        {
            _parcelaService = new ParcelaService(contexto);
        }

        [HttpPost]
        public IActionResult CriarParcela ([FromBody] ParcelaCreateDTO novaParcela)
        {
            try{

                var resultado = _parcelaService.CriarParcela(novaParcela);

                if (resultado.Status != StatusServiceRetorno.Criado) {
                    return BadRequest(resultado.Msg);
                }

                return CreatedAtAction(nameof(LocalizaParcelaPorNumParc), new { id = resultado.Parcelas.First().Cliente.Id, 
                                                                                numContrato =  resultado.Parcelas.First().NumContrato, 
                                                                                numParcela = resultado.Parcelas.First().NumParcela}, resultado.Parcelas.First());

            } catch (Exception e){

                return BadRequest($"Erro ao inserir o Parcela, {e.Message}");
            }
        }

        [HttpGet("{id}/{numContrato}/{numParcela}")]
        public IActionResult LocalizaParcelaPorNumParc (int id, string numContrato, int numParcela)
        {
            try{

                var resultado = _parcelaService.LocalizarParcelaPorIdNumContratoNumParc(id, numContrato, numParcela);

                if (resultado.Status == StatusServiceRetorno.Nao_Localizado) {
                    return NotFound($"Parcela não localizado, {resultado.Msg}");
                }                

                if (resultado.Status != StatusServiceRetorno.Localizado) {
                    return BadRequest(resultado.Msg);
                }

                return Ok(resultado.Parcelas);                

            } catch (Exception e){

                return BadRequest($"Erro ao localizar parcela, {e.Message}");
            }
        }

        [HttpGet]
        public IActionResult LocalizarParcelas()
        {
            try{

                var resultado = _parcelaService.LocalizarParcelas();             

                if (resultado.Status != StatusServiceRetorno.Localizado) {
                    return BadRequest(resultado.Msg);
                }

                return Ok(resultado.Parcelas);

            } catch (Exception e){

                return BadRequest($"Erro ao localizar parcela, {e.Message}");
            }
        }

        [HttpPut("{id}/{numContrato}/{numParcela}")]

        public IActionResult AtualizarParcela([FromRoute] int id, [FromRoute] string numContrato, [FromRoute] int numParcela, [FromBody] ParcelaUpdateDTO parcelaAlterada)
        {
            try {
                var resultado = _parcelaService.AtualizarParcelas(id, numContrato, numParcela, parcelaAlterada);

                if (resultado.Status == StatusServiceRetorno.Nao_Localizado) {
                    return NotFound($"Parcela {id} não localizada para atualização, {resultado.Msg}");
                }                 

                if (resultado.Status != StatusServiceRetorno.Atualizado){
                    return BadRequest(resultado.Msg);
                }

                return NoContent();
            } catch (Exception e){
                return BadRequest($"Erro ao atualizar o cliente {id}, {e.Message}");
            }
        }

        [HttpDelete("{id}/{numContrato}/{numParcela}")]
        public IActionResult DeletarParcela([FromRoute] int id, [FromRoute] string numContrato, [FromRoute] int numParcela)
        {
            try {
                var resultado = _parcelaService.DeletarParcela(id, numContrato,numParcela);

                if (resultado.Status == StatusServiceRetorno.Nao_Localizado) {
                    return NotFound(resultado.Msg);
                }                 

                if (resultado.Status != StatusServiceRetorno.Deletado){
                    return BadRequest($"Erro ao deletar parcela, {resultado.Msg}");
                }
                
                return NoContent();

            } catch (Exception e){
                return BadRequest($"Erro ao deletar parcela {id}, {e.Message}");   
            }
        }  
    }
}