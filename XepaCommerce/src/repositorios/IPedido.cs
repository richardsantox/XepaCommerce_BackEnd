using System.Collections.Generic;
<<<<<<< HEAD
using XepaCommerce.src.dtos;
=======
using XepaCommerce.src.dtos;
>>>>>>> c44a131f3831a10afeac52f2da570d036e71f986
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
