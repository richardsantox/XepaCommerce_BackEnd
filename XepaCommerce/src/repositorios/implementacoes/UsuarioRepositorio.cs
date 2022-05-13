using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        /// <summary>
        /// <para>Resumo: Método assíncrono para atualizar um usuario</para>
        /// </summary>
        /// <param name="usuario">AtualizarUsuarioDTO</param>
        public async Task AtualizarUsuarioAsync(AtualizarUsuarioDTO usuario)
        {
            var _usuario = await PegarUsuarioPeloIdAsync(usuario.Id);
            _usuario.Nome = usuario.Nome;
            _usuario.Senha = usuario.Senha;
            _usuario.Endereco = usuario.Endereco;
            _contexto.Usuarios.Update(_usuario);
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar um usuario</para>
        /// </summary>
        /// <param name="id">Id do usuario</param>
        public async Task DeletarUsuarioAsync(int id)
        {
            _contexto.Usuarios.Remove(await PegarUsuarioPeloIdAsync(id));
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar um novo usuario</para>
        /// </summary>
        /// <param name="usuario">NovoUsuarioDTO</param>
        public async Task NovoUsuarioAsync(NovoUsuarioDTO usuario)
        {
            await _contexto.Usuarios.AddAsync(new UsuarioModelo
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = usuario.Senha,
                Endereco = usuario.Endereco,
                Tipo = usuario.Tipo
            });
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um usuario pelo email</para>
        /// </summary>
        /// <param name="email">Email do usuario</param>
        /// <return>UsuarioModelo</return>
        public async Task<UsuarioModelo> PegarUsuarioPeloEmailAsync(string email)
        {
            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um usuario pelo Id</para>
        /// </summary>
        /// <param name="id">Id do usuario</param>
        /// <return>UsuarioModelo</return>
        public async Task <UsuarioModelo> PegarUsuarioPeloIdAsync(int id)
        {
            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar usuarios pelo nome</para>
        /// </summary>
        /// <param name="nome">Nome do usuario</param>
        /// <return>Lista UsuarioModelo</return>
        public async Task<List<UsuarioModelo>> PegarUsuariosPeloNomeAsync(string nome)
        {
            return await _contexto.Usuarios.Where(u => u.Nome.Contains(nome)).ToListAsync();
        }
    }
}
#endregion