using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CamposDealerTest.Models
{
    public class Cliente
    {
        [Key]
        [Required]
        [JsonProperty("idCliente")]
        public int IdCliente { get; set; }

        [Required]
        [JsonProperty("nmCliente")]
        public string Nome { get; set; }

        [Required]
        [JsonProperty("Cidade")]
        public string Cidade { get; set; }
    }
}
