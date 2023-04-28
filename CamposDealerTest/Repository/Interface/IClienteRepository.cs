using CamposDealerTest.Models;

namespace CamposDealerTest.Repository.Interface
{
    public interface IClienteRepository : IDisposable
    {
        IEnumerable<Cliente> ListarClientes();
        Cliente BuscarClientePorId(int id);
        void AdicionarCliente(Cliente id);
        void DeletarCliente(int id);
        void AtualizarCliente(Cliente id);
        void Salvar();
    }
}
