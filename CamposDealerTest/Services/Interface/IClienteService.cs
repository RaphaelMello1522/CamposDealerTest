using CamposDealerTest.Models;

namespace CamposDealerTest.Services.Interface
{
    public interface IClienteService
    {
        Task<List<Cliente>> GetClientesAsync();
    }
}
