using System.ComponentModel.DataAnnotations;
using XepaCommerce.src.utilidades;

namespace XepaCommerce.src.dtos
{
    /// <summary>
    /// <para>Resumo: Classe espelho responsável pela Criação de Novos Usuários</para>
    /// <para>Criado por: Matheus Correia</para>
    /// <para>Version: 1.0</para>
    /// <para>Data: 12/05/2022</para>
    /// </summary>
    public class NovoUsuarioDTO
    {
        [Required , StringLength(100)]
        public string Nome { get; set; }

        [Required ,StringLength(30)]
        public string Email { get; set; }

        [Required, StringLength(30)]
        public string Senha { get; set; }

        [Required]
        public string Endereco { get; set; }

        [Required]
        public TipoUsuario Tipo { get; set; }
        
        public NovoUsuarioDTO(string nome ,string email , string senha , string endereco, TipoUsuario tipo)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Endereco = endereco;
            Tipo = tipo;
        }
    }

    /// <summary>
    /// <para>Resumo: Classe espelho responsável pela Atualização de Usuários</para>
    /// <para>Criado por: Matheus Correia</para>
    /// <para>Version: 1.0</para>
    /// <para>Data: 12/05/2022</para>
    /// </summary>
    public class AtualizarUsuarioDTO
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Nome { get; set; }

        [Required, StringLength(30)]
        public string Senha { get; set; }

        [Required]
        public string Endereco { get; set; }
        
        public AtualizarUsuarioDTO(int id, string nome , string senha , string endereco )
        {
            Id = id;
            Nome = nome;
            Senha = senha;
            Endereco = endereco;

        }
    }
}
