using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task DeletarPedidoAsync(int id)
        {
            _contexto.Pedidos.Remove(await PegarPedidoPeloIdAsync(id));
            await _contexto.SaveChangesAsync();
        }

        public async Task NovoPedidoAsync(NovoPedidoDTO pedido)
        {
            await _contexto.Pedidos.AddAsync(new PedidoModelo
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

        public async Task<PedidoModelo> PegarPedidoPeloIdAsync(int id)
        {
            return await _contexto.Pedidos
                 .Include(p => p.Comprador)
                 .Include(p => p.Produto)
                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<PedidoModelo>> PegarTodosPedidosAsync()
        {
            return await _contexto.Pedidos
               .Include(p => p.Comprador)
               .Include(p => p.Produto)
               .ToListAsync();
        }

        public async Task<List<PedidoModelo>> PesquisarPedidoAsync(
            string nomeProduto, 
            string nomeComprador,
            string emailComprador)
        {
            switch (nomeProduto, nomeComprador, emailComprador)
            {
                case (null, null, null):
                    return await PegarTodosPedidosAsync();

                case (null, null, _):
                    return await _contexto.Pedidos
                        .Include(pe => pe.Produto)
                        .Include(pe => pe.Comprador)
                        .Where(pe => pe.Comprador.Email == emailComprador)
                        .ToListAsync();

                case (null, _, null):
                    return await _contexto.Pedidos
                        .Include(pe => pe.Produto)
                        .Include(pe => pe.Comprador)
                        .Where(pe => pe.Comprador.Nome.Contains(nomeComprador))
                         .ToListAsync();

                case (_, null, null):
                    return await _contexto.Pedidos
                        .Include(pe => pe.Produto)
                        .Include(pe => pe.Comprador)
                        .Where(pe => pe.Produto.NomeProduto.Contains(nomeProduto))
                         .ToListAsync();

                case (_, _, null):
                    return await _contexto.Pedidos
                        .Include(pe => pe.Produto)
                        .Include(pe => pe.Comprador)
                        .Where(pe =>
                        pe.Produto.NomeProduto.Contains(nomeProduto) &
                        pe.Comprador.Nome.Contains(nomeComprador))
                         .ToListAsync();

                case (null, _, _):
                    return await _contexto.Pedidos
                        .Include(pe => pe.Produto)
                        .Include(pe => pe.Comprador)
                        .Where(pe =>
                        pe.Comprador.Nome.Contains(nomeComprador) &
                        pe.Comprador.Email == emailComprador)
                       .ToListAsync();

                case (_, null, _):
                    return await _contexto.Pedidos
                        .Include(pe => pe.Produto)
                        .Include(pe => pe.Comprador)
                        .Where(pe =>
                        pe.Produto.NomeProduto.Contains(nomeProduto) &
                        pe.Comprador.Email == emailComprador)
                        .ToListAsync();

                case (_, _, _):
                    return await _contexto.Pedidos
                        .Include(pe => pe.Produto)
                        .Include(pe => pe.Comprador)
                        .Where(pe =>
                        pe.Produto.NomeProduto.Contains(nomeProduto) |
                        pe.Comprador.Nome.Contains(nomeComprador) |
                        pe.Comprador.Email == emailComprador)
                        .ToListAsync();
            }


        }
        #endregion Metodos
    }
}
