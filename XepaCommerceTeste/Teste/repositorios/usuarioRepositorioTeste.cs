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
    #region Testes
    [TestClass]
    public class UsuarioRepositorioTeste
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
                    "Rua da flores, 55, SP",
                    TipoUsuario.NORMAL));

            _repositorio.NovoUsuario(
               new NovoUsuarioDTO(
                   "Ana Paula",
                   "ana@email.com",
                   "134652",
                   "Rua dos laranjais, 302, SP",
                   TipoUsuario.NORMAL));

            _repositorio.NovoUsuario(
               new NovoUsuarioDTO(
                   "Matheus Correira",
                   "matheus@email.com",
                   "134652",
                   "Rua das americas, 963, SP",
                   TipoUsuario.NORMAL));

            _repositorio.NovoUsuario(
               new NovoUsuarioDTO(
                   "Thamires Aparecida",
                   "thamires@email.com",
                   "134652",
                   "Rua Brasil, 214, SP",
                   TipoUsuario.NORMAL));

            Assert.AreEqual(4, _contexto.Usuarios.Count());
        }


        [TestMethod]
        public void PegarUsuarioPeloEmailRetornaNaoNulo()
        {
            
            _repositorio.NovoUsuario(
                new NovoUsuarioDTO(
                    "Souza",
                    "souza@email.com",
                    "134652",
                    "Rua França, 214, SP",
                    TipoUsuario.NORMAL));

            var user = _repositorio.PegarUsuarioPeloEmail("souza@email.com");

            Assert.IsNotNull(user);
        }


        [TestMethod]
        public void PegarUsuarioPeloIdRetornaNaoNuloENomeDoUsuario()
        {
 
            _repositorio.NovoUsuario(
                new NovoUsuarioDTO(
                    "Lucas Reluz",
                    "lucas@email.com",
                    "134652",
                    "Rua Alemanha, 214, SP",
                    TipoUsuario.NORMAL));

            var user = _repositorio.PegarUsuarioPeloId(6);

            Assert.IsNotNull(user);
            Assert.AreEqual("Lucas Reluz", user.Nome);
        }


        [TestMethod]
        public void AtualizarUsuarioRetornaUsuarioAtualizado()
        {
            _repositorio.NovoUsuario(
            new NovoUsuarioDTO(
            "Ana Paula",
            "paula@email.com",
            "134652",
            "Rua Paraguay, 214, SP",
            TipoUsuario.NORMAL));

            var antigo =
            _repositorio.PegarUsuarioPeloEmail("estefania@email.com");
            _repositorio.AtualizarUsuario(
            new AtualizarUsuarioDTO(
            7,
            "Ana Julia",
            "123456",
            "Rua Paraguay, 951, SP"
            ));

            Assert.AreEqual(
            "Ana Julia",
            _contexto.Usuarios.FirstOrDefault(u => u.Id == antigo.Id).Nome);

            Assert.AreEqual(
            "123456",
            _contexto.Usuarios.FirstOrDefault(u => u.Id ==
            antigo.Id).Senha);
        }
    }
    #endregion
}

