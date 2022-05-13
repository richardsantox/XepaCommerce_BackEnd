using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XepaCommerce.src.data;
using XepaCommerce.src.dtos;
using XepaCommerce.src.modelos;

namespace XepaCommerce.src.repositorios.implementacoes
{
    /// <summary>
    /// <para>Resumo: Classe responsável por implementar IUsuario</para>
    /// <para>Criado por: Gustavo Boaz</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 12/05/2022</para>
    /// </summary>
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

        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar um pedido</para>
        /// </summary>
        /// <param name="id">Id do pedido</param>
        public async Task DeletarPedidoAsync(int id)
        {
            _contexto.Pedidos.Remove(await PegarPedidoPeloIdAsync(id));
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar um pedido</para>
        /// </summary>
        /// <param name="pedido">NovoPedidoDTO</param>
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

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um pedido pelo Id</para>
        /// </summary>
        /// <param name="id">Id do pedido</param>
        /// <return>PedidoModelo</return>
        public async Task<PedidoModelo> PegarPedidoPeloIdAsync(int id)
        {
            return await _contexto.Pedidos
                 .Include(p => p.Comprador)
                 .Include(p => p.Produto)
                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar todos os produtos</para>
        /// </summary>
        /// <return>Lista PedidoModelo</return>
        public async Task<List<PedidoModelo>> PegarTodosPedidosAsync()
        {
            return await _contexto.Pedidos
               .Include(p => p.Comprador)
               .Include(p => p.Produto)
               .ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para Pesquisar pedido</para>
        /// </summary>
        /// <param name="nomeProduto">Nome do produto</param>
        /// <param name="nomeComprador">Nome do Comprador</param>
        /// <param name="emailComprador">Email do comprador</param>
        /// <return>Lista PedidoModelo</return>
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