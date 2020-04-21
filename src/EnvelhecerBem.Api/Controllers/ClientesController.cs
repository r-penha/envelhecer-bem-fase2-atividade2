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
    [Route("/api/clientes")]
    public class ClientesController : ControllerBase
    {
        private static readonly ILogger Log = Serilog.Log.ForContext<ClientesController>();
        private readonly ClienteApplicationService _service;

        public ClientesController(ClienteApplicationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Adiciona novo cliente
        /// </summary>
        /// <remarks>
        /// O cabeçalho da resposta <i><strong>Location</strong></i> informará a url do novo <i>resource</i> criado.
        /// </remarks>
        /// <param name="cliente"></param>
        /// <response code="201">ID do novo cliente criado</response>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(Cliente cliente)
        {
            try
            {
                await _service.RegistrarCliente(cliente);
                var location = cliente.GetLocation(Request);
                return Created(location, cliente.Id);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro ao registrar {@Cliente}", cliente);
                return StatusCode(500, "Erro ao registrar cliente.");
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(Guid id, Cliente cliente)
        {
            try
            {
                if (cliente.Id != id) return BadRequest("Resource não corresponde à entidade.");
                await _service.AlterarCliente(cliente);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro ao alterar {@Cliente}", cliente);
                return StatusCode(500, "Erro ao alterar cliente.");
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
                await _service.RemoverCliente(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro ao excluir {Cliente}", id);
                return StatusCode(500, "Erro ao excluir cliente.");
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                var cliente = await _service.ObterCliente(id);
                return cliente != null
                           ? (IActionResult) Ok(cliente)
                           : NotFound("Cliente não encontrado.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro ao obter cliente {Cliente}", id);
                return StatusCode(500, "Erro ao obter cliente.");
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Cliente>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string q)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(q)) return Ok(await _service.ListarClientes());
                Expression<Func<Cliente, bool>> expression = x => x.Nome.Contains(q) || x.Cpf.Contains(q);
                var clientes = await _service.Procurar(expression);
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro ao pesquisar clientes");
                return StatusCode(500, "Erro ao pesquisar clientes.");
            }
        }
    }
}