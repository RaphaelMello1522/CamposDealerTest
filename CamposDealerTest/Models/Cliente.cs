using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CamposDealerTest.Models
{
    public class Cliente
    {
        [JsonProperty("idProduto")]
        [Key]
        [Required]
        public int IdCliente { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cidade { get; set; }
    }
}
