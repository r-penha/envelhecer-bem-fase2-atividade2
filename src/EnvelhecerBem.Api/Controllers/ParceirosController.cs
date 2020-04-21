using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EnvelhecerBem.Core.Domain;
using EnvelhecerBem.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EnvelhecerBem.Api.Controllers
{
    [ApiController]
    [Route("/api/parceiros")]
    public class ParceirosController : ControllerBase
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<ParceirosController>();
        private readonly ParceiroApplicationService _service;

        public ParceirosController(ParceiroApplicationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Adiciona novo parceiro
        /// </summary>
        /// <remarks>
        /// O cabeçalho da resposta <i><strong>Location</strong></i> informará a url do novo <i>resource</i> criado.
        /// </remarks>
        /// <param name="parceiro"></param>
        /// <response code="201">ID do novo parceiro criado</response>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(Parceiro parceiro)
        {
            try
            {
                await _service.RegistrarParceiro(parceiro);
                var location = parceiro.GetLocation(Request);
                return Created(location, parceiro.Id);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro ao registrar {@Parceiro}", parceiro);
                return StatusCode(500, "Erro ao registrar parceiro.");
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(Guid id, Parceiro parceiro)
        {
            try
            {
                if (parceiro.Id != id) return BadRequest("Resource não corresponde à entidade.");
                await _service.AlterarParceiro(parceiro);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro ao alterar {@Parceiro}", parceiro);
                return StatusCode(500, "Erro ao alterar parceiro.");
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.RemoverParceiro(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro ao excluir {Parceiro}", id);
                return StatusCode(500, "Erro ao excluir parceiro.");
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType(typeof(Parceiro), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                var parceiro = await _service.ObterParceiro(id);
                return parceiro != null
                           ? (IActionResult) Ok(parceiro)
                           : NotFound("Parceiro não encontrado.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro ao obter parceiro {Parceiro}", id);
                return StatusCode(500, "Erro ao obter parceiro.");
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Parceiro>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string q)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(q)) return Ok(await _service.ListarParceiros());
                Expression<Func<Parceiro, bool>> expression = x => x.RazaoSocial.Contains(q) || x.Cnpj.Contains(q);
                var parceiros = await _service.Procurar(expression);
                return Ok(parceiros);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro ao pesquisar parceiros");
                return StatusCode(500, "Erro ao pesquisar parceiros.");
            }
        }
    }
}