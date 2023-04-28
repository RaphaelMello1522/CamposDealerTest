using CamposDealerTest.Data;
using CamposDealerTest.Models;
using CamposDealerTest.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CamposDealerTest.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly CamposDealerTestContext _context;
        private IProdutoService _produtoService;

        public ProdutosController(CamposDealerTestContext context, IProdutoService produtoService)
        {
            _context = context;
            _produtoService = produtoService;
        }

        public async Task<IActionResult> Index(string busca)
        {
            GerarCargaDeProdutos();

            var produto = from m in _context.Produto
                          select m;

            if (!string.IsNullOrEmpty(busca))
            {
                produto = produto.Where(p => p.Descricao!.Contains(busca));
            }
            return View(await produto.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Produto == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .FirstOrDefaultAsync(m => m.IdProduto == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProduto,Descricao,ValorUnitario")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Produto == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProduto,Descricao,ValorUnitario")] Produto produto)
        {
            if (id != produto.IdProduto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.IdProduto))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produto == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .FirstOrDefaultAsync(m => m.IdProduto == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produto == null)
            {
                return Problem("Entity set 'CamposDealerTestContext.Produto'  is null.");
            }
            var produto = await _context.Produto.FindAsync(id);
            if (produto != null)
            {
                _context.Produto.Remove(produto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return (_context.Produto?.Any(e => e.IdProduto == id)).GetValueOrDefault();
        }

        //para gerar a carga o id tem de ser 0 pois o SQL server faz o auto-increment
        public void GerarCargaDeProdutos()
        {
            var carga = _produtoService.GetProdutosAsync().Result;
            foreach (var item in carga)
            {
                item.IdProduto = 0;
            }
            _context.AddRange(carga);
            _context.SaveChanges();
        }
    }
}
