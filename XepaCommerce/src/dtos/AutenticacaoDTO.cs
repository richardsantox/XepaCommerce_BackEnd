using System.ComponentModel.DataAnnotations;
using XepaCommerce.src.utilidades;

namespace XepaCommerce.src.dtos
{
    /// <summary>
    /// <para>Resumo: Classe espelho responsável pela Autenticação de dados</para>
    /// <para>Criado por: Matheus Correia</para>
    /// <para>Version: 1.0</para>
    /// <para>Data: 12/05/2022</para>
    /// </summary>
    public class AutenticarDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }

        public AutenticarDTO(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }
    }

    /// <summary>
    /// <para>Resumo: Classe espelho responsável pela Autorização de dados</para>
    /// <para>Criado por: Matheus Correia</para>
    /// <para>Version: 1.0</para>
    /// <para>Data: 12/05/2022</para>
    /// </summary>
    public class AutorizacaoDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public TipoUsuario Tipo { get; set; }
        public string Token { get; set; }

        public AutorizacaoDTO(int id, string email, TipoUsuario tipo, string token)
        {
            Id = id;
            Email = email;
            Tipo = tipo;
            Token = token;
        }
    }
}
