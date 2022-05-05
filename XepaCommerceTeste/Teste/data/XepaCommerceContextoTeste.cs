using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XepaCommerce.src.data;
using XepaCommerce.src.modelos;

namespace XepaCommerceTeste.Teste.data
{
    [TestClass]
    public class XepaCommerceContextoTeste
    {
        private XepaCommerceContexto _contexto;

        [TestInitialize]
        public void incio()
        {
            var opt = new DbContextOptionsBuilder<XepaCommerceContexto>()
                .UseInMemoryDatabase(databaseName: "bd_xepacommerce")
                .Options;

            _contexto = new XepaCommerceContexto(opt);
        }


        [TestMethod]
        public void InserirNovoUsuarioNoBancoRetornaUsuario()
        {
            UsuarioModelo usuario = new UsuarioModelo();

            usuario.Nome = "Richard";
            usuario.Email = "richard@email.com";
            usuario.Senha = "123456";
            usuario.Endereco = "Rua Abreu, 55";
            
            _contexto.Usuarios.Add(usuario);

            _contexto.SaveChanges();


            Assert.IsNotNull(_contexto.Usuarios.FirstOrDefault(u => u.Email == "richard@email.com"));
        }
    }
}
