using CamposDealerTest.Data;
using CamposDealerTest.Models;
using CamposDealerTest.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CamposDealerTest.Repository
{
    public class VendaRepository : IVendaRepository
    {
        private CamposDealerTestContext context;

        public VendaRepository(CamposDealerTestContext context)
        {
            this.context = context;
        }

        public void AdicionarVenda(Venda id)
        {
            context.Venda.Add(id);
        }

        public void AtualizarVenda(Venda id)
        {
            context.Entry(id).State = EntityState.Modified;
        }

        public Venda BuscarVendaPorId(int id)
        {
            return context.Venda.Find(id);
        }

        public IEnumerable<Venda> ListarVendas()
        {
            var vendasList = context.Venda.ToList();

            return vendasList;
        }

        public void DeletarVenda(int id)
        {
            Venda venda = BuscarVendaPorId(id);
            context.Venda.Remove(venda);
        }

        public void Salvar()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
