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
    public class usuarioRepositorioTeste
    {
        private XepaCommerceContexto _contexto;
        private IUsuario _repositorio;

        [TestInitialize]
        public void ConfiguracaoInicial()
        {
            var opt = new DbContextOptionsBuilder<XepaCommerceContexto>()
            .UseInMemoryDatabase(databaseName: "db_xepacommerce")
            .Options;
            _contexto = new XepaCommerceContexto(opt);
            _repositorio = new UsuarioRepositorio(_contexto);
        }

        [TestMethod]
        public void CriarQuatroUsuariosNoBancoRetornaQuatroUsuarios()
        {
            _repositorio.NovoUsuario(
                new NovoUsuarioDTO(
                    "Richard",
                    "richard@email.com",
                    "134652",
                    "Rua da flores, 55, SP"));

            _repositorio.NovoUsuario(
               new NovoUsuarioDTO(
                   "Ana Paula",
                   "ana@email.com",
                   "134652",
                   "Rua dos laranjais, 302, SP"));

            _repositorio.NovoUsuario(
               new NovoUsuarioDTO(
                   "Matheus Correira",
                   "matheus@email.com",
                   "134652",
                   "Rua das americas, 963, SP"));

            _repositorio.NovoUsuario(
               new NovoUsuarioDTO(
                   "Thamires Aparecida",
                   "thamires@email.com",
                   "134652",
                   "Rua Brasil, 214, SP"));

            Assert.AreEqual(4, _contexto.Usuarios.Count());
        }

    }
}
