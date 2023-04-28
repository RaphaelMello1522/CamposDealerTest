using CamposDealerTest.Data;
using CamposDealerTest.Models;
using CamposDealerTest.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CamposDealerTest.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private CamposDealerTestContext context;

        public ClienteRepository(CamposDealerTestContext context)
        {
            this.context = context;
        }

        public void AdicionarCliente(Cliente cliente)
        {
            context.Cliente.Add(cliente);
        }

        public void AtualizarCliente(Cliente cliente)
        {
            context.Entry(cliente).State = EntityState.Modified;
        }

        public Cliente BuscarClientePorId(int id)
        {
            return context.Cliente.Find(id);
        }

        public IEnumerable<Cliente> ListarClientes()
        {
            var clientesList = context.Cliente.ToList();

            return clientesList;
        }

        public void DeletarCliente(int id)
        {
            Cliente cliente = BuscarClientePorId(id);
            context.Cliente.Remove(cliente);
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
