using System.Collections.Generic;
using XepaCommerce.src.dtos;
using XepaCommerce.src.modelos;

namespace XepaCommerce.src.repositorios
{
    public interface IProduto
    {
        void NovoProduto(NovoProdutoDTO produto);
        void AtualizarProduto(AtualizarProdutoDTO produto);
        void DeletarProduto(int id);
        ProdutoModelo PegarProdutoPeloId(int id);
        List<ProdutoModelo> PegarTodosProdutos();
        List<ProdutoModelo> PegarProdutosPorNome(string nomeProduto);       
    }
}