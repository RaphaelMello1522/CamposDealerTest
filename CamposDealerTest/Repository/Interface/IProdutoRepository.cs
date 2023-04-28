using CamposDealerTest.Models;

namespace CamposDealerTest.Repository.Interface
{
    public interface IProdutoRepository : IDisposable
    {
        IEnumerable<Produto> ListarProdutos();
        Produto BuscarProdutoPorId(int id);
        void AdicionarProduto(Produto id);
        void DeletarProduto(int id);
        void AtualizarProduto(Produto id);
        void Salvar();
    }
}
