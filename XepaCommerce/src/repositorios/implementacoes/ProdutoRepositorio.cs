using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XepaCommerce.src.data;
using XepaCommerce.src.dtos;
using XepaCommerce.src.modelos;

namespace XepaCommerce.src.repositorios.implementacoes
{
    public class ProdutoRepositorio : IProduto
    {
        #region Atributos
        private readonly XepaCommerceContexto _contexto;
        #endregion

        #region Construtores

        public ProdutoRepositorio(XepaCommerceContexto contexto)
        {
            _contexto = contexto;
        }

        #endregion

        #region Metodos
        public async Task AtualizarProdutoAsync(AtualizarProdutoDTO produto)
        {
            var _produto = await PegarProdutoPeloIdAsync(produto.Id);
            _produto.NomeProduto = produto.NomeProduto;
            _produto.Preco = produto.Preco;
            _produto.Descricao = produto.Descricao;
            _produto.Foto = produto.Foto;
            _produto.Estoque = produto.Estoque;
            _contexto.Update(_produto);
            await _contexto.SaveChangesAsync();
        }

        public async Task DeletarProdutoAsync(int id)
        {
            _contexto.Produtos.Remove(await PegarProdutoPeloIdAsync(id));
            await _contexto.SaveChangesAsync();
        }

        public async Task NovoProdutoAsync(NovoProdutoDTO produto)
        {
            _contexto.Produtos.Add(new ProdutoModelo
            {
            NomeProduto = produto.NomeProduto,
            Preco = produto.Preco,
            Descricao = produto.Descricao,
            Foto = produto.Foto,
            Estoque = produto.Estoque
        });
            await _contexto.SaveChangesAsync();
        }

        public async Task<ProdutoModelo> PegarProdutoPeloIdAsync(int id)
        {
            return await _contexto.Produtos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<ProdutoModelo>> PegarProdutosPorNomeAsync(string nomeProduto)
        {
            return await _contexto.Produtos.Where(u => u.NomeProduto.Contains(nomeProduto)).ToListAsync();
        }

        public List<ProdutoModelo> PegarTodosProdutos()
        {
            return _contexto.Produtos.ToList();
        }
        #endregion Metodos
    }
}