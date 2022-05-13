using Microsoft.EntityFrameworkCore;
using XepaCommerce.src.modelos;

namespace XepaCommerce.src.data
{
    /// <summary>
    /// <para>Resumo: Classe contexto, responsável por carregar contexto e definir DbSets</para>
    /// <para>Criado por: Richard Santos</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
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