using CamposDealerTest.Models;

namespace CamposDealerTest.Repository.Interface
{
    public interface IVendaRepository : IDisposable
    {
        IEnumerable<Venda> ListarVendas();
        Venda BuscarVendaPorId(int id);
        void AdicionarVenda(Venda id);
        void DeletarVenda(int id);
        void AtualizarVenda(Venda id);
        void Salvar();
    }
}
