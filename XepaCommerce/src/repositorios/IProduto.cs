using System.Collections.Generic;
using XepaCommerce.src.modelos;

namespace XepaCommerce.src.repositorios
{
    public interface IProduto
    {
        void NovoProduto(NovoProdutoDTO produto);
        void AtualizarProdutoDTO(AtualizarProdutoDTO produto);
        void DeletarProduto(int id);
        ProdutoModelo PegarProdutoPeloId(int id);
        List<ProdutoModelo> PegarTodosProdutos();
        List<ProdutoModelo> PegarProdutosPorPesquisa(string nomeProduto, string descricao);       
    }
}