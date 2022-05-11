using System.Collections.Generic;
using System.Threading.Tasks;
using XepaCommerce.src.dtos;
using XepaCommerce.src.modelos;

namespace XepaCommerce.src.repositorios
{
    public interface IProduto
    {
        Task NovoProdutoAsync(NovoProdutoDTO produto);
        Task AtualizarProdutoAsync(AtualizarProdutoDTO produto);
        Task DeletarProdutoAsync(int id);
        Task<ProdutoModelo> PegarProdutoPeloIdAsync(int id);
        List<ProdutoModelo> PegarTodosProdutos();
        Task<List<ProdutoModelo>> PegarProdutosPorNomeAsync(string nomeProduto);       
    }
}