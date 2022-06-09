using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CamposDealerTest.Models
{
    public class Clientes
    {
        [Key]
        [Required]
        public int idCliente { get; set; }
        [Required]
        [StringLength(50)]
        public string nmCliente { get; set; }
        [Required]
        [StringLength(25)]
        public string Cidade { get; set; }

    }
}