<<<<<<< HEAD
=======
using Microsoft.EntityFrameworkCore;
using XepaCommerce.src.modelos;

>>>>>>> d5bdb2fbcbae14d0c6496b9cea1d4ff5b1515540
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