<<<<<<< HEAD
=======

>>>>>>> 91a3f1e35d5a5511429a6489f1b74432e789d16d
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