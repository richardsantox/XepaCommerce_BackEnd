using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XepaCommerce.src.modelos
{ 
    [Table("tb_pedidos")]
    public class PedidoModelo
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public float PrecoTotal { get; set; }

        [Required, StringLength(50)]
        public string StatusPedido { get; set; }

        [Required]
        public string FormaDePagamento { get; set; }

        [ForeignKey("fk_usuario")]
        public UsuarioModelo Comprador { get; set; }

        [ForeignKey("fk_produto")]
        public ProdutoModelo Produto { get; set; }

    }
}
