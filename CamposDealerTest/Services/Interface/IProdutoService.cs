using CamposDealerTest.Models;

namespace CamposDealerTest.Services.Interface
{
    public interface IProdutoService
    {
        Task<List<Produto>> GetProdutosAsync();
    }
}
