using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using  RenegociacaoAPI.src.Services;
using RenegociacaoAPI.src.Context;
using RenegociacaoAPI.src.Models.DTOs;
using RenegociacaoAPI.src.Models.Entities;

namespace RenegociacaoAPI.src.Controllers
{
    [Route("[controller]")]
    
    public class ContratoController : Controller
    {
        private readonly ContratoService _contratoService;

        public ContratoController(RenegociacaoContext contexto)
        {
            _contratoService = new ContratoService(contexto);
        }

        [HttpPost]
        public IActionResult CriarContrato ([FromBody] ContratoCreateDTO novoContrato)
        {
            try{

                var resultado = _contratoService.CriarUmContrato(novoContrato);

                if (resultado.Status != StatusServiceRetorno.Criado) {
                    return BadRequest($"Erro ao inserir o cliente, {resultado.Msg}");
                }

                return CreatedAtAction(nameof(LocalizaContratoPorIdNumContrato), new { Id = resultado.Contratos.First().Cliente.Id, 
                                                                                      numContrato =  resultado.Contratos.First().NumContrato}, resultado.Contratos.First());

            } catch (Exception e){

                return BadRequest($"{e.Message}");
            }
        }

        [HttpGet("{id}/{numContrato}")]
        public IActionResult LocalizaContratoPorIdNumContrato (int id, string numContrato)
        {
            try{
                
                var resultado = _contratoService.LocalizarContratoPorIdNumContrato(id, numContrato);

                if (resultado.Status == StatusServiceRetorno.Nao_Localizado) {
                    return NotFound($"Contrato não localizado, {resultado.Msg}");
                }                

                if (resultado.Status != StatusServiceRetorno.Localizado) {
                    return BadRequest($"{resultado.Msg}");
                }

                return Ok(resultado.Contratos);                

            } catch (Exception e){

                return BadRequest($"Erro ao localizar o contrato por Id e Num Contrato, {e.Message}");
            }
        }
                
        [HttpGet]
        public IActionResult LocalizarContrato()
        {
            try{

                var resultado = _contratoService.LocalizarContratos();             

                if (resultado.Status != StatusServiceRetorno.Localizado) {
                    return BadRequest($"{resultado.Msg}");
                }

                return Ok(resultado.Contratos);

            } catch (Exception e){

                return BadRequest($"Erro ao buscar todos os contratos, {e.Message}");
            }
        }

        [HttpPut("{id}/{numContrato}")]

        public IActionResult AtualizarContrato([FromRoute] int id, [FromRoute] string numContrato,[FromBody] ContratoCreateDTO contratoAlterado)
        {
            try {
                var resultado = _contratoService.AtualizarContrato(id, numContrato, contratoAlterado);

                if (resultado.Status == StatusServiceRetorno.Nao_Localizado) {
                    return NotFound($"Contraot  o contrato {contratoAlterado.ClienteId} / {contratoAlterado.NumContrato} não localizado para atualização, {resultado.Msg}");
                }                 

                if (resultado.Status != StatusServiceRetorno.Atualizado){
                    return BadRequest($"Erro ao atualizar  o contrato {contratoAlterado.ClienteId} / {contratoAlterado.NumContrato}, {resultado.Msg}");
                }

                return NoContent();
            } catch (Exception e){
                return BadRequest($"Erro ao atualizar o contrato {contratoAlterado.ClienteId} / {contratoAlterado.NumContrato}, {e.Message}");
            }
        }

        [HttpDelete("{id}/{numContrato}")]
        public IActionResult DeletarContrato([FromRoute] int id, [FromRoute] string numContrato)
        {
            try {
                var resultado = _contratoService.DeletarContrato(id, numContrato);

                if (resultado.Status == StatusServiceRetorno.Nao_Localizado) {
                    return NotFound(resultado.Msg);
                }                 

                if (resultado.Status != StatusServiceRetorno.Deletado){
                    return BadRequest($"Erro ao deletar o contrato {id} / {numContrato}, {resultado.Msg}");
                }

                return NoContent();

            } catch (Exception e){
                return BadRequest(e.Message);   
            }
        }        
    }
}