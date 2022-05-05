using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using XepaCommerce.src.data;
using XepaCommerce.src.dtos;
using XepaCommerce.src.modelos;

namespace XepaCommerce.src.repositorios.implementacoes
{
    public class PedidoRepositorio : IPedido
    {
        #region Atributos
        private readonly XepaCommerceContexto _contexto;
        #endregion

        #region Construtores

        public PedidoRepositorio(XepaCommerceContexto contexto)
        {
            _contexto = contexto;
        }

        #endregion

        #region Metodos

        public void DeletarPedido(int id)
        {
            _contexto.Pedidos.Remove(PegarPedidoPeloId(id));
            _contexto.SaveChanges();
        }

        public void NovoPedido(NovoPedidoDTO pedido)
        {
            _contexto.Pedidos.Add(new PedidoModelo
            {
                Quantidade = pedido.Quantidade,
                PrecoTotal = pedido.PrecoTotal,
                StatusPedido = pedido.StatusPedido,
                FormaDePagamento = pedido.FormaDePagamento,
                Comprador = _contexto.Usuarios.FirstOrDefault(u => u.Nome == pedido.NomeComprador),
                Produto = _contexto.Produtos.FirstOrDefault(p => p.NomeProduto == pedido.ProdutoComprado)
            });
            _contexto.SaveChanges();

        }

        public PedidoModelo PegarPedidoPeloId(int id)
        {
            return _contexto.Pedidos.FirstOrDefault(pe => pe.Id == id);
        }

        public List<PedidoModelo> PegarTodosPedidos()
        {
            return _contexto.Pedidos.ToList();
        }

        public List<PedidoModelo> PesquisarPedido(string produto, string comprador, string email)
        {
            switch (produto, comprador, email)
            {
                case (null, null, null):
                    return PegarTodosPedidos();

                case (null, null, _):
                    return _contexto.Pedidos
                        .Include(pe => pe.Produto)
                        .Include(pe => pe.Comprador)
                        .Where(pe => pe.Comprador.Email.Contains(comprador))
                        .ToList();

                case (null, _, null):
                    return _contexto.Pedidos
                        .Include(pe => pe.Produto)
                        .Include(pe => pe.Comprador)
                        .Where(pe => pe.Comprador.Nome.Contains(comprador))
                        .ToList();

                case (_, null, null):
                    return _contexto.Pedidos
                        .Include(pe => pe.Produto)
                        .Include(pe => pe.Comprador)
                        .Where(pe => pe.Produto.NomeProduto.Contains(produto))
                        .ToList();

                case (_, _, null):
                    return _contexto.Pedidos
                        .Include(pe => pe.Produto)
                        .Include(pe => pe.Comprador)
                        .Where(pe =>
                        pe.Produto.NomeProduto.Contains(produto) &
                        pe.Comprador.Nome.Contains(comprador))
                        .ToList();

                case (null, _, _):
                    return _contexto.Pedidos
                        .Include(pe => pe.Produto)
                        .Include(pe => pe.Comprador)
                        .Where(pe =>
                        pe.Comprador.Nome.Contains(comprador) &
                        pe.Comprador.Email.Contains(email))
                        .ToList();

                case (_, null, _):
                    return _contexto.Pedidos
                        .Include(pe => pe.Produto)
                        .Include(pe => pe.Comprador)
                        .Where(pe =>
                        pe.Produto.NomeProduto.Contains(produto) &
                        pe.Comprador.Email.Contains(email))
                        .ToList();

                case (_, _, _):
                    return _contexto.Pedidos
                        .Include(pe => pe.Produto)
                        .Include(pe => pe.Comprador)
                        .Where(pe =>
                        pe.Produto.NomeProduto.Contains(produto) |
                        pe.Comprador.Nome.Contains(comprador) |
                        pe.Comprador.Email.Contains(email))
                        .ToList();
            }


        }
        #endregion Metodos
    }
}
