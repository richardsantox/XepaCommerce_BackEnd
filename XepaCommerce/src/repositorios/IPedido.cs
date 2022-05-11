using System.Collections.Generic;
using System.Threading.Tasks;
using XepaCommerce.src.dtos;
using XepaCommerce.src.modelos;

namespace XepaCommerce.src.repositorios
{
    public interface IPedido
    {
        Task NovoPedidoAsync(NovoPedidoDTO pedido);
        Task DeletarPedidoAsync(int id);
        Task<PedidoModelo> PegarPedidoPeloIdAsync(int id);
        Task <List<PedidoModelo>> PegarTodosPedidosAsync();
        Task<List<PedidoModelo>> PesquisarPedidoAsync(string produto, string comprador, string email);
    }
}
