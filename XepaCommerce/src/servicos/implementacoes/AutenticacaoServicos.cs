using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using XepaCommerce.src.dtos;
using XepaCommerce.src.modelos;
using XepaCommerce.src.repositorios;

namespace XepaCommerce.src.servicos.implementacoes
{

    /// <summary>
    /// <para>Resumo: Classe responsavel por implementar IAutenticacao</para>
    /// <para>Criado por: Dannyela Souza</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    public class AutenticacaoServicos : IAutenticacao
    {
        #region Atributos

        private readonly IUsuario _repositorio;
        public IConfiguration Configuracao { get; }

        #endregion

        #region Construtores

        public AutenticacaoServicos(IUsuario repositorio, IConfiguration configuration)
        {
            _repositorio = repositorio;
            Configuracao = configuration;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// <para>Resumo: Método responsavel por criptografar senha</para>
        /// </summary>
        /// <param name="senha">Senha a ser criptografada</param>
        /// <returns>string</returns>
        public string CodificarSenha(string senha)
        {
            var bytes = Encoding.UTF8.GetBytes(senha);
            return Convert.ToBase64String(bytes);
        }


        /// <summary>
        /// <para>Resumo: Método assíncrono responsavel por criar usuario sem duplicar no banco</para>
        /// </summary>
        /// <param name="dto">NovoUsuarioDTO</param>
        public async Task CriarUsuarioSemDuplicarAsync(NovoUsuarioDTO dto)
        {
            var usuario = await _repositorio.PegarUsuarioPeloEmailAsync(dto.Email);
            if (usuario != null) throw new Exception("Este email já está sendo utilizado");
            dto.Senha = CodificarSenha(dto.Senha);
            await _repositorio.NovoUsuarioAsync(dto);
        }


        /// <summary>
        /// <para>Resumo: Método responsavel por gerar token JWT</para>
        /// </summary>
        /// <param name="usuario">UsuarioModelo</param>
        /// <returns>string</returns>
        public string GerarToken(UsuarioModelo usuario)
        {
            var tokenManipulador = new JwtSecurityTokenHandler();
            var chave = Encoding.ASCII.GetBytes(Configuracao["Settings:Secret"]);
            var tokenDescricao = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
            new Claim[]
            {
            new Claim(ClaimTypes.Email, usuario.Email.ToString()),
            new Claim(ClaimTypes.Role, usuario.Tipo.ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(chave),
            SecurityAlgorithms.HmacSha256Signature
            )
            };
            var token = tokenManipulador.CreateToken(tokenDescricao);
            return tokenManipulador.WriteToken(token);
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono responsavel devolver autorização para usuario autenticado</para>
        /// </summary>
        /// <param name="dto">AutenticarDTO</param>
        /// <returns>AutorizacaoDTO</returns>
        /// <exception cref="Exception">Usuário não encontrado</exception>
        /// <exception cref="Exception">Senha incorreta</exception>
        public async Task<AutorizacaoDTO> PegarAutorizacaoAsync(AutenticarDTO autenticacao)
        {
            var usuario = await _repositorio.PegarUsuarioPeloEmailAsync(autenticacao.Email);
            if (usuario == null) throw new Exception("Usuário não encontrado");
            if (usuario.Senha != CodificarSenha(autenticacao.Senha)) throw new
            Exception("Senha incorreta");
            return new AutorizacaoDTO(usuario.Id, usuario.Email, usuario.Tipo, GerarToken(usuario));
            
        }
        #endregion
    }
}