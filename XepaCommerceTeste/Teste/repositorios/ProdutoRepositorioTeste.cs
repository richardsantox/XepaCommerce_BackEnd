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
    public class ProdutoRepositorioTeste
    {
        private XepaCommerceContexto _contexto;
        private IProduto _repositorio;

        [TestInitialize]
        public void ConfiguracaoInicial()
        {
            var opt = new DbContextOptionsBuilder<XepaCommerceContexto>()
            .UseInMemoryDatabase(databaseName: "db_xepacommerce")
            .Options;
            _contexto = new XepaCommerceContexto(opt);
            _repositorio = new ProdutoRepositorio(_contexto);
        }

        [TestMethod]
        public void CriarQuatroProdutosNoBancoRetornaQuatroProdutos()
        {
            _repositorio.NovoProduto(
                new NovoProdutoDTO(
                    "Banana",
                    1.98f,
                    "Banana nanica madura orgânica",
                    "Link da foto",
                    50 ));

            _repositorio.NovoProduto(
                new NovoProdutoDTO(
                    "Maça",
                    5.98f,
                    "",
                    "Link da foto",
                    200));
       
            _repositorio.NovoProduto(
              new NovoProdutoDTO(
                    "Pera",
                    3.30f,
                    "",
                    "Link da foto",
                    100));

            _repositorio.NovoProduto(
              new NovoProdutoDTO(
                    "Abacate",
                    7.98f,
                    "Abacate Maduro, pronto para consumo",
                    "Link da foto",
                    30));

            Assert.AreEqual(4, _contexto.Produtos.Count());
        }

        [TestMethod]
        [DataRow(1)]
        public void PegarProdutoPeloIdRetornaNome(int id)
        {
            //GIVEN - Dado que pesquiso pelo id 1
            var produto = _repositorio.PegarProdutoPeloId((id));
            //THEN - Entao deve retornar 1 tema)
            Assert.AreEqual("Banana", produto.NomeProduto);
        }
    }
}

