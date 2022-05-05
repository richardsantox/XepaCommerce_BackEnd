using System.ComponentModel.DataAnnotations;

namespace XepaCommerce.src.dtos
{
    /// <summary>
    /// <para>Resumo: Classe espelho para criar um produto</para>
    /// <para>Criado por: Thamires Freitas</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 05/05/2022</para>
    /// </summary>
    public class NovoProdutoDTO

    {
        [Required, StringLength(100)]
        public string NomeProduto { get; set; }

        [Required]
        public float Preco { get; set; }

        [StringLength(100)]
        public string Descricao { get; set; }

        [Required, StringLength(100)]
        public string Foto { get; set; }

        [Required]
        public int Estoque { get; set; }

        public NovoProdutoDTO(string nomeproduto, float preco, string descricao, string foto, int estoque)
        {
            NomeProduto = nomeproduto;
            Preco = preco;
            Descricao = descricao;
            Foto = foto;
            Estoque = estoque;
        }
    }
    /// <summary>
    /// <para>Resumo: Classe espelho para alterar um produto</para>
    /// <para>Criado por: Thamires Freitas</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 05/05/2022</para>
    /// </summary>
    public class AtualizarProdutoDTO
    {
        internal int Id;

        [Required, StringLength(100)]
        public string NomeProduto { get; set; }

        [Required]
        public float Preco { get; set; }

        [StringLength(100)]
        public string Descricao { get; set; }

        [Required, StringLength(100)]
        public string Foto { get; set; }

        [Required]
        public int Estoque { get; set; }

        public AtualizarProdutoDTO(string nomeproduto, float preco, string descricao, string foto, int estoque)
        {
            NomeProduto = nomeproduto;
            Preco = preco;
            Descricao = descricao;
            Foto = foto;
            Estoque = estoque;
        }
    }
}