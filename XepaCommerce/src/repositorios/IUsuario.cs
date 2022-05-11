using System.Collections.Generic;
using System.Threading.Tasks;
using XepaCommerce.src.dtos;
using XepaCommerce.src.modelos;

namespace XepaCommerce.src.repositorios
{
    /// <summary>
    /// <para>Resumo: Responsavel por representar ações de CRUD de usuario</para>
    /// <para>Criado por: Vinicius Santos Matos | Grupo 04 </para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 05/05/2022</para>
    /// </summary>
    public interface IUsuario
    {
        Task <UsuarioModelo> PegarUsuarioPeloIdAsync(int id);
        Task<List<UsuarioModelo>> PegarUsuariosPeloNomeAsync(string nome);
        Task<UsuarioModelo> PegarUsuarioPeloEmailAsync(string email);
        Task NovoUsuarioAsync(NovoUsuarioDTO usuario);
        Task AtualizarUsuarioAsync(AtualizarUsuarioDTO usuario);
        Task DeletarUsuarioAsync(int id);
    }
}
