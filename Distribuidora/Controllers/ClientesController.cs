using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Distribuidora.Models;
using System.Text.RegularExpressions;

namespace Distribuidora.Controllers
{
    public class ClientesController : Controller
    {
        private DistribuidoraContext db = new DistribuidoraContext();

        // GET: Clientes
        public ActionResult Index()
        {
            return View(db.Clientes.ToList());
        }

        #region Login
        [HttpGet]
        public ActionResult Login()
        {
            return View(new Cliente());
        }

        [HttpPost]
        public ActionResult Login(Cliente u)
        {

           Cliente clienteUsuario;
            using (db)
            {
                string email = u.Email;
                string contrasenia = u.Contrasenia;
                clienteUsuario = db.Clientes.FirstOrDefault(e => e.Email == email && e.Contrasenia == contrasenia);
                if (clienteUsuario != null)
                {
                    return RedirectToAction("Index", "Clientes");
                }
                else
                {
                    ModelState.AddModelError("Error", "Email o contraseña incorrectos");
                }
            return View(u);
            }
        }
            

        #endregion

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Email,Nombre,Contrasenia")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                if (ValidacionContrasenia(cliente.Contrasenia)){
                    db.Clientes.Add(cliente);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

            return View(cliente);
        }

        public bool ValidacionContrasenia(string Contrasenia)
        {
            if (Contrasenia.Length >= 6)
            {
                if (new Regex("[a-z]").IsMatch(Contrasenia) && new Regex("[A-Z]").IsMatch(Contrasenia) && new Regex("[0-9]").IsMatch(Contrasenia))
                {
                    return true;
                }
            }
            return false;
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Email,Nombre,Contrasenia")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
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
