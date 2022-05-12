using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XepaCommerce.src.dtos;
using XepaCommerce.src.repositorios;

namespace XepaCommerce.src.Controladores
{
    [ApiController]
    [Route("api/Pedido")]
    [Produces("application/json")]
    public class PedidoControlador : ControllerBase
    {
        #region Atributos 

        private readonly IPedido _repositorio;

        #endregion

        #region Contrutores
        public PedidoControlador(IPedido repositorio)
        {
            _repositorio = repositorio;
        }

        #endregion

        #region Metodos 

        [HttpGet("id/{idPedido}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> PegarPedidoPeloIdAsync([FromRoute] int idPedido)
        {
            var pedido = await _repositorio.PegarPedidoPeloIdAsync(idPedido);

            if (pedido == null) return NotFound();

            return Ok(pedido);
        }

        [HttpGet]
        [Authorize(Roles = "NORMAL,ADMINISTRADOR")]
        public async Task<ActionResult> PegarTodosPedidos()
        {
            var lista = await _repositorio.PegarTodosPedidosAsync();

            if (lista.Count < 1) return NoContent();

            return Ok(lista);
        }

        [HttpGet("pesquisa")]
        [Authorize(Roles = "NORMAL,ADMINISTRADOR")]
        public async Task<ActionResult> PesquisarPedidoAsync
        (
            [FromQuery] string nomeProduto,
            [FromQuery] string nomeComprador,
            [FromQuery] string emailComprador
        )

        {
              var pedido = await _repositorio.PesquisarPedidoAsync(nomeProduto, nomeComprador, emailComprador);

              if (pedido.Count < 1) return NoContent();

               return Ok(pedido);

        }

        [HttpPost]
        [Authorize(Roles = "NORMAL,ADMINISTRADOR")]
        public async Task<ActionResult> NovaPedidoAsync([FromBody] NovoPedidoDTO pedido)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repositorio.NovoPedidoAsync(pedido);

            return Created($"api/Postagens", pedido);
        }

        [HttpDelete("deletar/{idPedido}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> DeletarPedidoAsync([FromRoute] int idPedido)
        {
            await _repositorio.DeletarPedidoAsync(idPedido);
            return NoContent();
        }
        #endregion
    }
}
