using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CamposDealerTest.Models;

namespace CamposDealerTest.Data
{
    public class CamposDealerTestContext : DbContext
    {
        public CamposDealerTestContext (DbContextOptions<CamposDealerTestContext> options)
            : base(options)
        {
        }

        public DbSet<CamposDealerTest.Models.Produto> Produto { get; set; } = default!;

        public DbSet<CamposDealerTest.Models.Venda> Venda { get; set; } = default!;

        public DbSet<CamposDealerTest.Models.Cliente> Cliente { get; set; } = default!;
    }
}
