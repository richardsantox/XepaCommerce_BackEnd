using System.Collections.Generic;
using System.Linq;
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
        public void AtualizarProduto(AtualizarProdutoDTO produto)
        {
            var _produto = PegarProdutoPeloId(produto.Id);
            _produto.NomeProduto = produto.NomeProduto;
            _produto.Preco = produto.Preco;
            _produto.Descricao = produto.Descricao;
            _produto.Foto = produto.Foto;
            _produto.Estoque = produto.Estoque;
            _contexto.Update(_produto);
            _contexto.SaveChanges();
        }

        public void DeletarProduto(int id)
        {
            _contexto.Produtos.Remove(PegarProdutoPeloId(id));
            _contexto.SaveChanges();
        }

        public void NovoProduto(NovoProdutoDTO produto)
        {
            _contexto.Produtos.Add(new ProdutoModelo
            {
            NomeProduto = produto.NomeProduto,
            Preco = produto.Preco,
            Descricao = produto.Descricao,
            Foto = produto.Foto,
            Estoque = produto.Estoque
        });
            _contexto.SaveChanges();
        }

        public ProdutoModelo PegarProdutoPeloId(int id)
        {
            return _contexto.Produtos.FirstOrDefault(p => p.Id == id);
        }

        public List<ProdutoModelo> PegarProdutosPorNome(string nomeProduto)
        {
            return _contexto.Produtos.Where(u => u.NomeProduto.Contains(nomeProduto)).ToList();
        }

        public List<ProdutoModelo> PegarTodosProdutos()
        {
            return _contexto.Produtos.ToList();
        }
        #endregion Metodos
    }
}