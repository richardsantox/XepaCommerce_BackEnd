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
    [Route("api/Produtos")]
    [Produces("application/json")]

    public class ProdutoControlador : ControllerBase
    {
        #region Atributos

        private readonly IProduto _repositorio;

        #endregion

        #region Construtores

        public ProdutoControlador(IProduto repositorio)
        {
            _repositorio = repositorio;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Pegar produto pelo Id
        /// </summary>
        /// <param name="idProduto">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o produto</response>
        /// <response code="404">Produto não existente</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProdutoModelo))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idProduto}")]
        [AllowAnonymous]
        public async Task<ActionResult> PegarProdutoPeloIdAsync([FromRoute] int idProduto) 
        {
            var Produto = await _repositorio.PegarProdutoPeloIdAsync(idProduto);

            if (Produto == null) return NotFound();
            return Ok(Produto);
        }

        /// <summary>
        /// Pegar produto pelo Nome
        /// </summary>
        /// <param name="nomeProduto">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o produto</response>
        /// <response code="204">Produto não existe</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProdutoModelo))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("nome")]
        [AllowAnonymous]
        public async Task<ActionResult> PegarProdutosPorNomeAsync([FromQuery] string nomeProduto) 
        {
            var Produto = await _repositorio.PegarProdutosPorNomeAsync(nomeProduto);

            if (Produto.Count < 1) return NoContent();
            return Ok(Produto);
        }

        /// <summary>
        /// Pegar todos os produtos
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retornar todas as postagens</response>
        /// <response code="404">Produto não encontrado</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> PegarTodosProdutosAsync()
        {
            var lista = await _repositorio.PegarTodosProdutosAsync();

            if (lista.Count < 1) return NotFound();
            return Ok(lista);
        }

        /// <summary>
        /// Deletar produto pelo Id
        /// </summary>
        /// <param name="idProduto">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Produto deletado</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("deletar/{idProduto}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> DeletarProdutoAsync([FromRoute] int idProduto)
        {
            await _repositorio.DeletarProdutoAsync(idProduto);
            return NoContent();
        }

        /// <summary>
        /// Criar novo produto
        /// </summary>
        /// <param name="produto">NovoProdutoDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Produto
        ///     {
        ///        "nomeProduto": "Banana",
        ///        "preco": 5.00f,
        ///        "descricao": "Banana da terra",
        ///        "foto": "URLPHOTO",
        ///        "estoque": 20
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna produto criado</response>
        /// <response code="400">Erro na requisição</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProdutoModelo))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> NovoProdutoAsync([FromBody] NovoProdutoDTO produto)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            await _repositorio.NovoProdutoAsync(produto);
            
            return Created("api/Produtos", produto);
        }

        /// <summary>
        /// Atualizar Produto
        /// </summary>
        /// <param name="produto">AtualizarProdutoDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /api/Produto
        ///     {
        ///        "nomeProduto": "Banana",
        ///        "preco": 5.00f,
        ///        "descricao": "Banana da terra",
        ///        "foto": "URLPHOTO",
        ///        "estoque": 20           
        ///      }
        ///
        /// </remarks>
        /// <response code="200">Retorna produto atualizado</response>
        /// <response code="400">Erro na requisição</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProdutoModelo))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> AtualizarProdutoAsync([FromBody] AtualizarProdutoDTO produto)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            await _repositorio.AtualizarProdutoAsync(produto);
            
            return Ok(produto);
        }
        #endregion
    }
}
