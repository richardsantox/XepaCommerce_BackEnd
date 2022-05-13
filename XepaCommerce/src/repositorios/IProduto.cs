using System.Collections.Generic;
using System.Threading.Tasks;
using XepaCommerce.src.dtos;
using XepaCommerce.src.modelos;

namespace XepaCommerce.src.repositorios
{
    /// <summary>
    /// <para>Resumo: Responsavel por representar ações de CRUD de produto</para>
    /// <para>Criado por: Thamires Freitas | Grupo 04</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    public interface IProduto
    {
        Task NovoProdutoAsync(NovoProdutoDTO produto);
        Task AtualizarProdutoAsync(AtualizarProdutoDTO produto);
        Task DeletarProdutoAsync(int id);
        Task<ProdutoModelo> PegarProdutoPeloIdAsync(int id);
        Task<List<ProdutoModelo>> PegarTodosProdutosAsync();
        Task<List<ProdutoModelo>> PegarProdutosPorNomeAsync(string nomeProduto);       
    }
}