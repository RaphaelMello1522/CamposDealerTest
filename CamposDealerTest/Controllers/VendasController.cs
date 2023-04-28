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
            GerarCargaDeVendas();

            var venda = from m in _context.Venda
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
            var cliente = _context.Cliente.ToList().Where(x => x.IdCliente.Equals(venda.IdCliente));
            var produto = _context.Produto.ToList().Where(x => x.IdProduto.Equals(venda.IdProduto));

            venda.ValorTotalVenda = venda.QuantidadeVenda * venda.ValorUnitarioVenda;
            venda.DataHoraVenda = DateTime.Now;
            _context.Add(venda);
            await _context.SaveChangesAsync();

            return View(venda);
        }

        public async Task<IActionResult> Edit(int? id)
        {
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

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendaExists(venda.IdVenda))
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
            return View(venda);
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

        //para gerar a carga o id tem de ser 0 pois o SQL server faz o auto-increment
        private void GerarCargaDeVendas()
        {
            var carga = _vendaService.GetVendasAsync().Result;
            foreach (var item in carga)
            {
                item.IdVenda = 0;
                item.IdCliente = 0;
                item.IdProduto = 0;
            }
            _context.AddRange(carga);
            _context.SaveChanges();
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
