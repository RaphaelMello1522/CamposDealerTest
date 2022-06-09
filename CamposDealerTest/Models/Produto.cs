using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CamposDealerTest.Models
{
    public class Produto
    {
        [Key]
        [Required]
        public int idProduto { get; set; }

        [Required]
        [StringLength(250)]
        public string dscProduto { get; set; }

        [Required]
        public float vlrUnitario { get; set; }
    }
}