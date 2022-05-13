using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XepaCommerce.src.dtos;
using XepaCommerce.src.modelos;
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
        
        /// <summary>
        /// Pegar pedido pelo Id
        /// </summary>
        /// <param name="idPedido">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o pedido</response>
        /// <response code="404">Pedido não existente</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PedidoModelo))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idPedido}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> PegarPedidoPeloIdAsync([FromRoute] int idPedido)
        {
            var pedido = await _repositorio.PegarPedidoPeloIdAsync(idPedido);

            if (pedido == null) return NotFound();

            return Ok(pedido);
        }

        /// <summary>
        /// Pega todos os pedidos
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna os pedidos</response>
        /// <response code="404">Pedidos não existentes</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PedidoModelo))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [Authorize(Roles = "NORMAL,ADMINISTRADOR")]
        public async Task<ActionResult> PegarTodosPedidos()
        {
            var lista = await _repositorio.PegarTodosPedidosAsync();

            if (lista.Count < 1) return NoContent();

            return Ok(lista);
        }

        /// <summary>
        /// Pegar pedidos por pesquisa
        /// </summary>
        /// <param name="nomeProduto">string</param>
        /// <param name="nomeComprador">string</param>
        /// <param name="emailComprador">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o pedido</response>
        /// <response code="404">Pedido não existente</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PedidoModelo))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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


        /// <summary>
        /// Novo pedido
        /// </summary>
        /// <param name="pedido">NovoPedidoDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /api/Pedido
        ///     {
        ///       "quantidade" : "1",
        ///       "precototal" : 5.60,
        ///       "statuspedido" : "Entregue",
        ///       "formadepagamento" : "Cartão de Credito"
        ///       "nomecomprador" : "Dom Pedro II"
        ///       "produtocomprado" : "Abobora"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Pedido criado e feito</response>
        /// <response code="400">Erro na requisição</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize(Roles = "NORMAL,ADMINISTRADOR")]
        public async Task<ActionResult> NovoPedidoAsync([FromBody] NovoPedidoDTO pedido)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repositorio.NovoPedidoAsync(pedido);

            return Created($"api/Postagens", pedido);
        }

        /// <summary>
        /// Deleta um pedido
        /// </summary>
        /// <param name="idPedido">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Pedido Excluido</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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