using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CamposDealerTest.Models
{
    public class Vendas
    {
        [Key]
        public int IdVenda { get; set; }
        public int IdCliente { get; set; }
        public int IdProduto { get; set; }
        public int qtdVenda { get; set; }
        public int vlrUnitarioVenda { get; set; }
        public DateTime dthVenda { get; set; }
        public float vlrTotalVenda { get; set; }

        public Clientes Cliente { get; set; }
        public Produto produto { get; set; }
    }
}