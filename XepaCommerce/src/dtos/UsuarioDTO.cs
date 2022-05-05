using System.ComponentModel.DataAnnotations;

namespace XepaCommerce.src.dtos
{
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
    
        
        public NovoUsuarioDTO(string nome ,string email , string senha , string endereco )
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Endereco = endereco;
        }
    }

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

        public AtualizarUsuarioDTO( string nome , string senha , string endereco )
        {
            Nome = nome;
            Senha = senha;
            Endereco = endereco;

        }
    }
}
