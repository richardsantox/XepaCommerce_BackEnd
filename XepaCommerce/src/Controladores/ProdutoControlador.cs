using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XepaCommerce.src.dtos;
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

        [HttpGet("id/{idproduto}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> PegarProdutoPeloIdAsync([FromRoute] int idproduto) 
        {
            var Produto = await _repositorio.PegarProdutoPeloIdAsync(idproduto);

            if (Produto == null) return NotFound();
            return Ok(Produto);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> PegarProdutosPorNomeAsync([FromQuery] string nomeproduto) 
        {
            var Produto = await _repositorio.PegarProdutosPorNomeAsync(nomeproduto);

            if (Produto.Count < 1) return NoContent();
            return Ok(Produto);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult PegarTodosProdutos()
        {
            var lista = _repositorio.PegarTodosProdutos();

            if (lista.Count < 1) return NotFound();
            return Ok(lista);
        }

        [HttpDelete("deletar/{idproduto}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> DeletarProdutoAsync([FromRoute] int idproduto)
        {
            await _repositorio.DeletarProdutoAsync(idproduto);
            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> NovoProdutoAsync([FromBody] NovoProdutoDTO produto)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            await _repositorio.NovoProdutoAsync(produto);
            
            return Created("api/Produtos", produto);
        }

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
