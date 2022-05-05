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
        public void CriarTresPedidosNoSistemaERetornarTres() 
        {
            _repositorioPe.NovoPedido(
            new NovoPedidoDTO(
            5,
            150.00f,
            "Enviado", 
            "Cartão",
            "Thamires",
            "Banana"
            )
            );

            _repositorioPe.NovoPedido(
            new NovoPedidoDTO(
            6,
            200.00f,
            "Enviado",
            "Pix",
            "Ana Paula",
            "Melancia"
            )
            );

            _repositorioPe.NovoPedido(
            new NovoPedidoDTO(
            7,
            100.00f,
            "Enviado",
            "Cartão",
            "Richard",
            "Laranja"
            )
            );

            Assert.AreEqual(3,_repositorioPe.PegarTodosPedidos().Count());
    
        }
    }
}
