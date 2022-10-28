using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RenegociacaoAPI.src.Services;
using RenegociacaoAPI.src.Context;

namespace RenegociacaoAPI.src.Controllers
{
    [Route("[controller]")]

    public class CalculoController : Controller
    {
        private readonly CalculoService _calculoService;

        public CalculoController(RenegociacaoContext contexto)
        {
            _calculoService = new CalculoService(contexto);
        }

        [HttpGet("{id}/{numContrato}")]

        public IActionResult Get (int id, string numContrato)
        {
            try{
                
                var resultado = _calculoService.EfetuaCalculo(id, numContrato);

                if (resultado.Status == StatusServiceRetorno.Nao_Localizado) {
                    return NotFound($"Contrato não localizado, {resultado.Msg}");
                }                

                if (resultado.Status != StatusServiceRetorno.Localizado) {
                    return BadRequest($"{resultado.Msg}");
                }

                return Ok(resultado.Parcelas);                

            } catch (Exception e){

                return BadRequest($"Erro ao localizar o contrato por Id e Num Contrato, {e.Message}");
            }
        }        
        
    }
}