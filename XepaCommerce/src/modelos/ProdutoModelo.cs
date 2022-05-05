using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace XepaCommerce.src.modelos
{
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
