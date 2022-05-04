using Microsoft.EntityFrameworkCore;
using XepaCommerce.src.modelos;

namespace XepaCommerce.src.data
{
    public class XepaCommerceContexto : DbContext
    {
        public DbSet<UsuarioModelo> Usuarios { get; set; }
        public DbSet<ProdutoModelo> Produtos { get; set; }
        public DbSet<PedidoModelo> Pedidos { get; set; }

        public XepaCommerceContexto(DbContextOptions<XepaCommerceContexto> opt) : base(opt)
        {

        }
    }
}