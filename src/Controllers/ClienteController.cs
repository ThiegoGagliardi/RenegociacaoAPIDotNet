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
    public class ClienteController : Controller
    {
        private readonly ClienteService _clienteService;

        public ClienteController(RenegociacaoContext contexto)
        {
            _clienteService = new ClienteService(contexto);
        }

        [HttpPost]
        public IActionResult CriarCliente ([FromBody] ClienteCreateDTO novoCliente)
        {
            try{

                var resultado = _clienteService.CriarUmCliente(novoCliente);

                if (resultado.Status != StatusServiceRetorno.Criado) {
                    return BadRequest($"Erro ao inserir o cliente, {resultado.Msg}");
                }

                return CreatedAtAction(nameof(LocalizaClientePorId), new { Id = resultado.Clientes.First().Id}, resultado.Clientes.First());

            } catch (Exception e){

                return BadRequest($"Erro ao inserir o cliente, {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult LocalizaClientePorId ([FromRoute] int id)
        {
            try{

                var resultado = _clienteService.LocalizarClientePorId(id);

                if (resultado.Status == StatusServiceRetorno.Nao_Localizado) {
                    return NotFound($"Cliente não localizado, {resultado.Msg}");
                }                

                if (resultado.Status != StatusServiceRetorno.Localizado) {
                    return BadRequest($"Erro ao localizar o cliente, {resultado.Msg}");
                }

                return Ok(resultado.Clientes.First());                

            } catch (Exception e){

                return BadRequest($"Erro ao inserir o cliente, {e.Message}");
            }
        }

        [HttpGet]
        public IActionResult LocalizarCliente()
        {
            try{

                var resultado = _clienteService.LocalizarCliente();             

                if (resultado.Status != StatusServiceRetorno.Localizado) {
                    return BadRequest($"Erro ao localizar o cliente, {resultado.Msg}");
                }

                return Ok(resultado.Clientes);

            } catch (Exception e){

                return BadRequest($"Erro ao inserir o cliente, {e.Message}");
            }
        }

        [HttpPut("{id}")]

        public IActionResult AtualizarCliente([FromRoute]int id, [FromBody] ClienteCreateDTO clienteAlterado)
        {
            try {
                var resultado = _clienteService.AtualizarCliente(id, clienteAlterado);

                if (resultado.Status == StatusServiceRetorno.Nao_Localizado) {
                    return NotFound($"Cliente {id} não localizado para atualização, {resultado.Msg}");
                }                 

                if (resultado.Status != StatusServiceRetorno.Atualizado){
                    return BadRequest($"Erro ao atualizar o cliente, {resultado.Msg}");
                }

                return NoContent();
            } catch (Exception e){
                return BadRequest($"Erro ao atualizar o cliente {id}, {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarCliente([FromRoute] int id)
        {
            try {
                var resultado = _clienteService.DeletarCliente(id);

                if (resultado.Status == StatusServiceRetorno.Nao_Localizado) {
                    return NotFound(resultado.Msg);
                }                 

                if (resultado.Status != StatusServiceRetorno.Atualizado){
                    return BadRequest($"Erro ao deletar o cliente, {resultado.Msg}");
                }

                return NoContent();

            } catch (Exception e){
                return BadRequest($"Erro ao deletar o cliente {id}, {e.Message}");   
            }
        }
    }
}
