using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using XepaCommerce.src.utilidades;

namespace XepaCommerce.src.modelos
{
    /// <summary>
    /// <para>Resumo: Classe espelho responsável por Usuários no servidor</para>
    /// <para>Criado por: Matheus Correia</para>
    /// <para>Version: 1.0</para>
    /// <para>Data: 12/05/2022</para>
    /// </summary>
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

            [Required]
            public TipoUsuario Tipo { get; set; }


    }
}