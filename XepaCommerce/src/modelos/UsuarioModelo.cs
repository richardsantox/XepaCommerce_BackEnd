using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace XepaCommerce.src.modelos
{
    [Table("tb_usuarios")]
    public class UsuarioModelo
    {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            [Required]
            [StringLength(100)]
            public string Nome { get; set; }

            [Required]
            [StringLength(30)]
            public string Email { get; set; }

            [Required, StringLength(30)]
            public string Senha { get; set; }

            [Required]
            public string Endereco { get; set; }
    
            public string FormaDePagamento { get; set; }

    }
}