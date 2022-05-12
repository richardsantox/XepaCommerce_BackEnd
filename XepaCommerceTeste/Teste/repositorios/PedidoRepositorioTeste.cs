using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XepaCommerce.src.data;
using XepaCommerce.src.dtos;
using XepaCommerce.src.repositorios;
using XepaCommerce.src.repositorios.implementacoes;
using XepaCommerce.src.utilidades;

namespace XepaCommerceTeste.Teste.repositorios
{
    [TestClass]
    public class PedidoRepositorioTeste
    {
        private XepaCommerceContexto _contexto;
        private IUsuario _repositorioU;
        private IProduto _repositorioP;
        private IPedido _repositorioPe;

        [TestInitialize]
        public void ConfiguracaoInicial()
        {
            var opt = new DbContextOptionsBuilder<XepaCommerceContexto>()
                    .UseInMemoryDatabase(databaseName: "db_xepacommerce")
                    .Options;
            _contexto = new XepaCommerceContexto(opt);
            _repositorioU = new UsuarioRepositorio(_contexto);
            _repositorioP = new ProdutoRepositorio(_contexto);
            _repositorioPe = new PedidoRepositorio(_contexto);
        }

        [TestMethod]
        public async Task CriarTresPedidosNoSistemaERetornarTres()
        {
            await _repositorioU.NovoUsuarioAsync(new NovoUsuarioDTO("Thamires", "thamires@email.com", "134652", "Rua augusta 200", TipoUsuario.NORMAL));
            await _repositorioU.NovoUsuarioAsync(new NovoUsuarioDTO("Ana Paula", "ana@email.com", "134652", "Rua augusta 200", TipoUsuario.NORMAL));

            await _repositorioP.NovoProdutoAsync(
                new NovoProdutoDTO(
                    "Melancia",
                    9.55f,
                    "Melancia Fuji",
                    "URLFOTO",
                    200
                )
            );

            await _repositorioP.NovoProdutoAsync(
                new NovoProdutoDTO(
                    "Laranja",
                    1.78f,
                    "Laranja pera",
                    "URLFOTO",
                    30
                )
            );


            await _repositorioPe.NovoPedidoAsync(
                new NovoPedidoDTO(
                    6,
                    200.00f,
                    "Enviado",
                    "Pix",
                    "Ana Paula",
                    "Melancia"
                )
            );

            await _repositorioPe.NovoPedidoAsync(
            new NovoPedidoDTO(
                    7,
                    100.00f,
                    "Enviado",
                    "Cartão",
                    "Thamires",
                    "Laranja"
                )
            );

            Assert.AreEqual(2, await _repositorioPe.PegarTodosPedidosAsync());

        }

        [TestMethod]
        public async Task PegarPedidoPorPesquisaRetornarCustomizada()
        {

            await _repositorioU.NovoUsuarioAsync(new NovoUsuarioDTO("Richard", "richard@email.com", "134652", "Rua Azul 200", TipoUsuario.NORMAL));
            await _repositorioP.NovoProdutoAsync(
                new NovoProdutoDTO(
                    "Banana",
                    1.78f,
                    "Banana nanica",
                    "URLFOTO",
                    500
                )
            );

            await _repositorioPe.NovoPedidoAsync(
                new NovoPedidoDTO(
                    5,
                    150.00f,
                    "Enviado",
                    "Cartão",
                    "Richard",
                    "Banana"
                )
            );

            await _repositorioPe.NovoPedidoAsync(
                new NovoPedidoDTO(
                    5,
                    150.00f,
                    "Enviado",
                    "Cartão",
                    "Richard",
                    "Banana"
                )
            );

            await _repositorioPe.NovoPedidoAsync(
                new NovoPedidoDTO(
                    5,
                    150.00f,
                    "Enviado",
                    "Cartão",
                    "Richard",
                    "Banana"
                )
            );

            var pedidos = await _repositorioPe
            .PesquisarPedidoAsync("Banana", null, null);
            Assert.AreEqual(3, pedidos.Count());
        }
    }
}
