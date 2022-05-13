using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace XepaCommerce.src.modelos
{
    /// <summary>
    /// <para>Resumo: Classe espelho responsável por Produtos no servidor</para>
    /// <para>Criado por: Matheus Correia</para>
    /// <para>Version: 1.0</para>
    /// <para>Data: 12/05/2022</para>
    /// </summary>
    [Table("tb_produtos")]

    public class ProdutoModelo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string NomeProduto { get; set; }

        [Required]
        public float Preco { get; set; }    

        [StringLength (100)]
        public string Descricao { get; set; }

        [Required, StringLength(100)]
        public string Foto { get; set; }  

        [Required]
        public int Estoque { get; set; }    

        //[JsonIgnore]
        //public List<ProdutoModelo> RelatedPosts { get; set; }
        
    }
}
