<<<<<<< HEAD

=======
<<<<<<< HEAD
=======

>>>>>>> 91a3f1e35d5a5511429a6489f1b74432e789d16d
>>>>>>> 2d53b0d2874de60f5338a6d48934f54c855f7f20
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