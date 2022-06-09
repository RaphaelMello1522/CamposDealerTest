using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CamposDealerTest.Models;
using Newtonsoft.Json;

namespace CamposDealerTest.Controllers.Sales
{
    public class VendasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Vendas
        public async Task<ActionResult> Index()
        {
            var vendas = db.Vendas.Include(v => v.Cliente).Include(v => v.produto);
            return View(vendas.ToList());

            string Baseurl = "https://camposdealer.dev/Sites/TesteAPI";

                List<Vendas> EmpInfo = new List<Vendas>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.GetAsync("/venda");
                    if (Res.IsSuccessStatusCode)
                    {
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        EmpInfo = JsonConvert.DeserializeObject<List<Vendas>>(EmpResponse);
                    }
                    return View(EmpInfo.AsEnumerable());
                }
            }

        

        // GET: Vendas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendas vendas = db.Vendas.Find(id);
            if (vendas == null)
            {
                return HttpNotFound();
            }
            return View(vendas);
        }

        // GET: Vendas/Create
        public ActionResult Create()
        {
            ViewBag.IdCliente = new SelectList(db.Clientes, "idCliente", "nmCliente");
            ViewBag.IdProduto = new SelectList(db.Produtoes, "idProduto", "dscProduto");
            return View();
        }

        // POST: Vendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdVenda,IdCliente,IdProduto,qtdVenda,vlrUnitarioVenda,dthVenda")] Vendas vendas)
        {
            if (ModelState.IsValid)
            {
                db.Vendas.Add(vendas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCliente = new SelectList(db.Clientes, "idCliente", "nmCliente", vendas.IdCliente);
            ViewBag.IdProduto = new SelectList(db.Produtoes, "idProduto", "dscProduto", vendas.IdProduto);
            return View(vendas);
        }

        // GET: Vendas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendas vendas = db.Vendas.Find(id);
            if (vendas == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCliente = new SelectList(db.Clientes, "idCliente", "nmCliente", vendas.IdCliente);
            ViewBag.IdProduto = new SelectList(db.Produtoes, "idProduto", "dscProduto", vendas.IdProduto);
            return View(vendas);
        }

        // POST: Vendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdVenda,IdCliente,IdProduto,qtdVenda,vlrUnitarioVenda,dthVenda,vlrTotalVenda")] Vendas vendas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCliente = new SelectList(db.Clientes, "idCliente", "nmCliente", vendas.IdCliente);
            ViewBag.IdProduto = new SelectList(db.Produtoes, "idProduto", "dscProduto", vendas.IdProduto);
            return View(vendas);
        }

        // GET: Vendas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendas vendas = db.Vendas.Find(id);
            if (vendas == null)
            {
                return HttpNotFound();
            }
            return View(vendas);
        }

        // POST: Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vendas vendas = db.Vendas.Find(id);
            db.Vendas.Remove(vendas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
