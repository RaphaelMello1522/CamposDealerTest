using CamposDealerTest.Data;
using CamposDealerTest.Models;
using CamposDealerTest.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CamposDealerTest.Controllers
{
    public class VendasController : Controller
    {
        private readonly CamposDealerTestContext _context;
        private IVendaService _vendaService;

        public VendasController(CamposDealerTestContext context, IVendaService vendaService)
        {
            _context = context;
            _vendaService = vendaService;
        }

        public async Task<IActionResult> Index(string busca)
        {
            var venda = from m in _context.Venda.Include("Cliente").Include("Produto")
                        select m;

            if (!string.IsNullOrEmpty(busca))
            {
                venda = venda.Where(p => p.Cliente.Nome!.Contains(busca) || p.Produto.Descricao!.Contains(busca));


            }
            return View(await venda.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Venda == null)
            {
                return NotFound();
            }

            var venda = await _context.Venda.Include("Cliente").Include("Produto")
                .FirstOrDefaultAsync(m => m.IdVenda == id);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        public IActionResult Create()
        {
            PopulateSelect();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Venda venda)
        {
            var vendas = _context.Venda.Include("Cliente").Include("Produto");
            var cliente = _context.Cliente.ToList().Where(x => x.IdCliente.Equals(venda.ClienteId));
            var produto = _context.Produto.ToList().Where(x => x.IdProduto.Equals(venda.ProdutoId));

            venda.ValorTotalVenda = venda.QuantidadeVenda * venda.ValorUnitarioVenda;
            venda.DataHoraVenda = DateTime.Now;
            _context.Add(venda);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            PopulateSelect();

            if (id == null || _context.Venda == null)
            {
                return NotFound();
            }

            var venda = await _context.Venda.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }
            return View(venda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Venda venda)
        {
            if (id != venda.IdVenda)
            {
                return NotFound();
            }

            var vendas = _context.Venda.Include("Cliente").Include("Produto");
            var cliente = _context.Cliente.ToList().Where(x => x.IdCliente.Equals(venda.ClienteId));
            var produto = _context.Produto.ToList().Where(x => x.IdProduto.Equals(venda.ProdutoId));

            venda.ValorTotalVenda = venda.QuantidadeVenda * venda.ValorUnitarioVenda;
            venda.DataHoraVenda = DateTime.Now;

            _context.Update(venda);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Vendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Venda == null)
            {
                return NotFound();
            }

            var venda = await _context.Venda
                .FirstOrDefaultAsync(m => m.IdVenda == id);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Venda == null)
            {
                return Problem("Entity set 'CamposDealerTestContext.Venda'  is null.");
            }
            var venda = await _context.Venda.FindAsync(id);
            if (venda != null)
            {
                _context.Venda.Remove(venda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendaExists(int id)
        {
            return (_context.Venda?.Any(e => e.IdVenda == id)).GetValueOrDefault();
        }

        private void PopulateSelect(object selecionado = null)
        {
            var clienteQuery = _context.Cliente.AsNoTracking().ToList();
            var produtoQuery = _context.Produto.AsNoTracking().ToList();

            ViewBag.ClienteId = new SelectList(clienteQuery, "IdCliente", "Nome", selecionado);
            ViewBag.ProdutoId = new SelectList(produtoQuery, "IdProduto", "Descricao", selecionado);
            return;
        }
    }
}
