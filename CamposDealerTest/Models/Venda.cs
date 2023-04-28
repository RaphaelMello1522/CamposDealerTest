using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CamposDealerTest.Models
{
    public class Venda
    {
        [JsonProperty("idVenda")]
        [Key]
        public int IdVenda { get; set; }

        [JsonProperty("idCliente")]
        [Required]
        public int ClienteId { get; set; }

        [JsonProperty("idProduto")]
        [Required]
        public int ProdutoId { get; set; }

        [JsonProperty("qtdVenda")]
        [Required]
        public int QuantidadeVenda { get; set; }

        [JsonProperty("vlrUnitarioVenda")]
        [Required]
        public int ValorUnitarioVenda { get; set; }

        [JsonProperty("dthVenda")]
        [Required]
        public DateTime DataHoraVenda { get; set; }
        [Required]
        public float ValorTotalVenda { get; set; }
        public Cliente Cliente { get; set; }
        public Produto Produto { get; set; }
    }
}
