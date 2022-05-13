using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XepaCommerce.src.data;
using XepaCommerce.src.dtos;
using XepaCommerce.src.modelos;

namespace XepaCommerce.src.repositorios.implementacoes
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por implementar IProduto</para>
    /// <para>Criado por: Matheus Correia</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 05/05/2022</para>
    /// </summary>
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
        /// <summary>
        /// <para>Resumo: Método assíncrono para atualizar um produto</para>
        /// </summary>
        /// <param name="produto">AtualizarProdutoDTO</param>
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
        
        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar um produto</para>
        /// </summary>
        /// <param name="id">Id do produto</param>
        public async Task DeletarProdutoAsync(int id)
        {
            _contexto.Produtos.Remove(await PegarProdutoPeloIdAsync(id));
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar um novo produto</para>
        /// </summary>
        /// <param name="produto">NovoProdutoDTO</param>        
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
        
        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um produto pelo Id</para>
        /// </summary>
        /// <param name="id">Id do produto</param>
        /// <return>ProdutoModelo</return>
        public async Task<ProdutoModelo> PegarProdutoPeloIdAsync(int id)
        {
            return await _contexto.Produtos.FirstOrDefaultAsync(p => p.Id == id);
        }
        
        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar produto pelo nome</para>
        /// </summary>
        /// <param name="nomeProduto">Nome do produto</param>
        /// <return>Lista ProdutoModelo</return>
        public async Task<List<ProdutoModelo>> PegarProdutosPorNomeAsync(string nomeProduto)
        {
            return await _contexto.Produtos.Where(u => u.NomeProduto.Contains(nomeProduto)).ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar todos produtos</para>
        /// </summary>
        /// <return>Lista ProdutoModelo</return>
        public async Task<List<ProdutoModelo>> PegarTodosProdutosAsync()
        {
            return await _contexto.Produtos.ToListAsync();
        }
        #endregion Metodos
    }
}