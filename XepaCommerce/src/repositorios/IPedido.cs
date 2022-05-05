using System.Collections.Generic;
using XepaCommerce.src.dtos;
using XepaCommerce.src.modelos;

namespace XepaCommerce.src.repositorios
{
    public interface IPedido
    {
        void NovoPedido(NovoPedidoDTO pedido);
        void DeletarPedido(int id);
        PedidoModelo PegarPedidoPeloId(int id);
        List<PedidoModelo> PegarTodosPedidos();
        List<PedidoModelo> PesquisarPedido(string produto, string comprador);
    }
}
