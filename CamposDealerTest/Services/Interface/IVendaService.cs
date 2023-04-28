using CamposDealerTest.Models;

namespace CamposDealerTest.Services.Interface
{
    public interface IVendaService
    {
        Task<List<Venda>> GetVendasAsync();
    }
}
