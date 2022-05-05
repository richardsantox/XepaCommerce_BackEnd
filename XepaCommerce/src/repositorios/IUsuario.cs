using System.Collections.Generic;

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
        UsuarioModelo PegarUsuarioPeloId(int id);
        List<UsuarioModelo> PegarUsuariosPeloNome(string nome);
        UsuarioModelo PegarUsuarioPeloEmail(string email);
        void NovoUsuario(NovoUsuarioDTO usuario);
        void AtualizarUsuario(AtualizarUsuarioDTO usuario);
        void DeletarUsuario(int id);
    }
}
