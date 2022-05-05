using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XepaCommerce.src.dtos
{
    /// <summary>
    /// <para>Resumo: Classe espelho para criar um novo pedido</para>
    /// <para>Crriado por: Richard Santos</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 05/05/2022 11:18</para>
    /// </summary>
    public class NovoPedidoDTO
    {
        [Required]
        public int Quantidade { get; set; }

        [Required]
        public float PrecoTotal { get; set; }

        [Required, StringLength(50)]
        public string StatusPedido { get; set; }

        [Required, StringLength(30)]
        public string FormaDePagamento { get; set; }

        [Required]
        public string NomeComprador { get; set; }

        [Required]
        public string ProdutoComprado { get; set; }

        public NovoPedidoDTO(int quantidade, float precoTotal, string statusPedido, string formaDePagamento, string nomeComprador, string produtoComprado)
        {
            Quantidade = quantidade;
            PrecoTotal = precoTotal;
            StatusPedido = statusPedido;
            FormaDePagamento = formaDePagamento;
            NomeComprador = nomeComprador;
            ProdutoComprado = produtoComprado;
        }
    }
}
