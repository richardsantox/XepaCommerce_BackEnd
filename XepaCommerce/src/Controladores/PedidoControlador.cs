using Microsoft.AspNetCore.Mvc;
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

        public IActionResult PegarPedidoPeloId([FromRoute] int idPedido)
        {
            
            var pedido = _repositorio.PegarPedidoPeloId(idPedido);

            if (pedido == null) return NotFound();

            return Ok(pedido);
        }

        [HttpGet]

        public IActionResult PegarTodosPedidos()
        {
            var lista = _repositorio.PegarTodosPedidos();

            if (lista.Count < 1) return NoContent();

            return Ok(lista);
        }

        [HttpGet("pesquisa")]
        public IActionResult PesquisarPedido
        (
            [FromQuery] string produto,
            [FromQuery] string comprador,
            [FromQuery] string email
        )

        {
              var pedido = _repositorio.PesquisarPedido(produto, comprador, email);

              if (pedido.Count < 1) return NoContent();

               return Ok(pedido);

        }

        [HttpPost]
        public IActionResult NovaPedido([FromBody] NovoPedidoDTO pedido)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repositorio.NovoPedido(pedido);

            return Created($"api/Postagens", pedido);
        }

        [HttpDelete("deletar/{idPedido}")]
        public IActionResult DeletarPedido([FromRoute] int idPedido)
        {
            _repositorio.DeletarPedido(idPedido);
            return NoContent();
        }
        #endregion
    }
}
