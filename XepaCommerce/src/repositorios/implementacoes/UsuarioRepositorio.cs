using System.Collections.Generic;
using System.Linq;
using XepaCommerce.src.data;
using XepaCommerce.src.dtos;
using XepaCommerce.src.modelos;

/// <summary>
/// <para>Resumo: Implementacao dos metodos na interface IUsuario </para>
/// <para>Criado por: Lucas Reluz</para>
/// <para>Versão: 1.0</para>
/// <para>Data: 05/05/2022</para>
/// </summary>
namespace XepaCommerce.src.repositorios.implementacoes
{
    public class UsuarioRepositorio : IUsuario
    {
        #region Atributos
        private readonly XepaCommerceContexto _contexto;
        #endregion

        #region Construtores

        public UsuarioRepositorio(XepaCommerceContexto contexto)
        {
            _contexto = contexto;
        }

        #endregion
        
        #region Metodos
        public void AtualizarUsuario(AtualizarUsuarioDTO usuario)
        {
            var _usuario = PegarUsuarioPeloId(usuario.Id);
            _usuario.Nome = usuario.Nome;
            _usuario.Senha = usuario.Senha;
            _usuario.Endereco = usuario.Endereco;
            _contexto.Update(_usuario);
            _contexto.SaveChanges();
        }

        public void DeletarUsuario(int id)
        {
            _contexto.Usuarios.Remove(PegarUsuarioPeloId(id));
            _contexto.SaveChanges();
        }

        public void NovoUsuario(NovoUsuarioDTO usuario)
        {
            _contexto.Usuarios.Add(new UsuarioModelo
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = usuario.Senha,
                Endereco = usuario.Endereco,
                Tipo = usuario.Tipo
            });
            _contexto.SaveChanges();
        }

        public UsuarioModelo PegarUsuarioPeloEmail(string email)
        {
            return _contexto.Usuarios.FirstOrDefault(u => u.Email == email);
        }

        public UsuarioModelo PegarUsuarioPeloId(int id)
        {
            return _contexto.Usuarios.FirstOrDefault(u => u.Id == id);
        }

        public List<UsuarioModelo> PegarUsuariosPeloNome(string nome)
        {
            return _contexto.Usuarios.Where(u => u.Nome.Contains(nome)).ToList();
        }
    }
}
#endregion