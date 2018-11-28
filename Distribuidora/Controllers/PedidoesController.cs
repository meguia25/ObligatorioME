using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Distribuidora.Models;
using Distribuidora.ViewModels;

namespace Distribuidora.Controllers
{
    public class PedidoesController : Controller
    {
        private DistribuidoraContext db = new DistribuidoraContext();

        // GET: Pedidoes
        public ActionResult Index()
        {
            return View(db.Pedidos.ToList());
        }

        // GET: Pedidoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // GET: Pedidoes/Create
        public ActionResult Create()
        {
            Cliente cliente = new Cliente();
            Producto producto = new Producto();
            Pedido pedido = new Pedido();
            ViewPedidos model = new ViewPedidos(cliente, producto, pedido);
            return View();
        }

        // POST: Pedidoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewPedidos pedi)
        {
            Pedido pedido = new Pedido();
            if (ModelState.IsValid)
            {
                using (DistribuidoraContext db = new DistribuidoraContext())
                {
                    db.Pedidos.Add(pedido);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }


            }

            return View(pedido);
        }

        // GET: Pedidoes/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente;
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                if (TempData["Cliente"] == null) return new HttpNotFoundResult();
                cliente = (Cliente)TempData["Cliente"];
                TempData["Cliente"] = cliente;
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPedido,Estado")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                using(DistribuidoraContext db = new DistribuidoraContext())
                {

                    Pedido pedi = (from p in db.Pedidos
                                   where p.IdPedido == pedido.IdPedido
                                   select p).SingleOrDefault();

                    pedi.Estado = 1;
                    db.Entry(pedido).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            return View(pedido);
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
