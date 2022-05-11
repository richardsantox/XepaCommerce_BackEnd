using XepaCommerce.src.dtos;
using XepaCommerce.src.modelos;

namespace XepaCommerce.src.servicos
{
    public interface IAutenticacao
    {
        string CodificarSenha(string senha);
        
        void CriarUsuarioSemDuplicar(NovoUsuarioDTO dto);

        string GerarToken(UsuarioModelo usuario);

        AutorizacaoDTO PegarAutorizacao(AutenticarDTO autenticacao);
    }
}
