using System.Collections.Generic;
<<<<<<< HEAD
using XepaCommerce.src.dtos;
=======
using XepaCommerce.src.dtos;
>>>>>>> c44a131f3831a10afeac52f2da570d036e71f986
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