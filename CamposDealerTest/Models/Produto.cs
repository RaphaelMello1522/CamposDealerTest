using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CamposDealerTest.Models
{
    public class Produto
    {
        [JsonProperty("idProduto")]
        [Key]
        [Required]
        public int IdProduto { get; set; }

        [JsonProperty("dscProduto")]
        [Required]
        public string Descricao { get; set; }

        [Required]
        [JsonProperty("vlrUnitario")]
        public float ValorUnitario { get; set; }
    }
}
