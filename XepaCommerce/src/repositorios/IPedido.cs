using System.Collections.Generic;
using System.Threading.Tasks;
using XepaCommerce.src.dtos;
using XepaCommerce.src.modelos;

namespace XepaCommerce.src.repositorios
{

    public interface IPedido
    {
        /// <summary>
        /// <para>Resumo: Responsavel por representar ações de CRUD de pedido</para>
        /// <para>Criado por: Thamires Freitas | Grupo 04</para>
        /// <para>Versão: 1.0</para>
        /// <para>Data: 13/05/2022</para>
        /// </summary>
        Task NovoPedidoAsync(NovoPedidoDTO pedido);
        Task DeletarPedidoAsync(int id);
        Task<PedidoModelo> PegarPedidoPeloIdAsync(int id);
        Task <List<PedidoModelo>> PegarTodosPedidosAsync();
        Task<List<PedidoModelo>> PesquisarPedidoAsync(string nomeProduto, string nomeComprador, string emailComprador);
    }
}