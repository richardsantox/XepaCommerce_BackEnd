using Microsoft.AspNetCore.Mvc;
using XepaCommerce.src.dtos;
using XepaCommerce.src.repositorios;

namespace XepaCommerce.src.Controladores
{
<<<<<<< HEAD
    public class ProdutoControlador
=======
    [ApiController]
    [Route("api/Produtos")]
    [Produces("application/json")]
    public class ProdutoControlador : ControllerBase
>>>>>>> 9d68fa7659c6a673e69f94629b0e05a17805fc07
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
        public IActionResult PegarProdutoPeloId([FromRoute] int idproduto) 
        {
            var Produto = _repositorio.PegarProdutoPeloId(idproduto);

            if (Produto == null) return NotFound();
            return Ok(Produto);
        }

        [HttpGet]
        public IActionResult PegarProdutoPorNome([FromQuery] string nomeproduto) 
        {
            var Produto = _repositorio.PegarProdutosPorNome(nomeproduto);

            if (Produto.Count < 1) return NoContent();
            return Ok(Produto);
        }

        [HttpGet]
        public IActionResult PegarTodosProdutos()
        {
            var lista = _repositorio.PegarTodosProdutos();

            if (lista.Count < 1) return NotFound();
            return Ok(lista);
        }

        [HttpDelete("deletar/{idproduto}")]
        public IActionResult DeletarProduto([FromRoute] int idproduto)
        {
            _repositorio.DeletarProduto(idproduto);
            return NoContent();
        }

        [HttpPost]
        public IActionResult NovoProduto([FromBody] NovoProdutoDTO produto)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repositorio.NovoProduto(produto);
            return Created("api/Produtos", produto);
        }

        [HttpPut]
        public IActionResult AtualizarProduto([FromBody] AtualizarProdutoDTO produto)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repositorio.AtualizarProduto(produto);
            return Ok(produto);
        }

        #endregion
    }
}
