using CamposDealerTest.Data;
using CamposDealerTest.Models;
using CamposDealerTest.Repository.Interface;
using CamposDealerTest.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace CamposDealerTest.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private CamposDealerTestContext context;
        private IProdutoService _produtoService;

        public ProdutoRepository(CamposDealerTestContext context, IProdutoService produtoService)
        {
            this.context = context;
            _produtoService = produtoService;
        }

        public void AdicionarProduto(Produto id)
        {
            context.Produto.Add(id);
        }

        public void AtualizarProduto(Produto id)
        {
            context.Entry(id).State = EntityState.Modified;
        }

        public Produto BuscarProdutoPorId(int id)
        {
            return context.Produto.Find(id);
        }

        public IEnumerable<Produto> ListarProdutos()
        {
            var peopleList = context.Produto.ToList();

            return peopleList;
        }

        public void DeletarProduto(int id)
        {
            Produto produto = BuscarProdutoPorId(id);
            context.Produto.Remove(produto);
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
