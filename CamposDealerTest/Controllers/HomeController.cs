using CamposDealerTest.Data;
using CamposDealerTest.Models;
using CamposDealerTest.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CamposDealerTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CamposDealerTestContext _context;
        private IClienteService _clienteService;
        private IProdutoService _produtoService;
        private IVendaService _vendaService;

        public HomeController(ILogger<HomeController> logger, IClienteService clienteService, IProdutoService produtoService, IVendaService vendaService, CamposDealerTestContext context)
        {
            _logger = logger;
            _clienteService = clienteService;
            _produtoService = produtoService;
            _vendaService = vendaService;
            _context = context;
        }

        public IActionResult Index()
        {
            GerarCargaDeCliente();
            GerarCargaDeProdutos();
            GerarCargaDeVendas();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //para gerar a carga o id tem de ser 0 pois o SQL server faz o auto-increment
        private void GerarCargaDeCliente()
        {
            var carga = _clienteService.GetClientesAsync().Result;
            foreach (var item in carga)
            {
                item.IdCliente = 0;
            }
            _context.AddRange(carga);
            _context.SaveChanges();
        }
        private void GerarCargaDeProdutos()
        {
            var carga = _produtoService.GetProdutosAsync().Result;
            foreach (var item in carga)
            {
                item.IdProduto = 0;
            }
            _context.AddRange(carga);
            _context.SaveChanges();
        }
        private void GerarCargaDeVendas()
        {
            var carga = _vendaService.GetVendasAsync().Result;
            foreach (var item in carga)
            {
                item.IdVenda = 0;
                item.ValorTotalVenda = item.ValorUnitarioVenda * item.QuantidadeVenda;
                _context.Cliente.ToList().Where(x => x.IdCliente.Equals(item.ClienteId));
                _context.Produto.ToList().Where(x => x.IdProduto.Equals(item.ProdutoId));
            }

            _context.AddRange(carga);
            _context.SaveChanges();
        }
    }
}