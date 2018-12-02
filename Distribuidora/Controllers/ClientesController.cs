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
                    Session["Cliente"] = clienteUsuario;
                    return RedirectToAction("Index", "Clientes");
                }
                else
                {
                    return RedirectToAction("Error", "Shared");
                }
            }
        }


        #endregion

        #region Alta usuario
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
                if (ValidacionContrasenia(cliente.Contrasenia) && !buscarCliente(cliente.Email))
                {
                    Cliente clienteNuevo = new Cliente()
                    {
                        Nombre = cliente.Nombre,
                        Email = cliente.Email,
                        FechaRegistro = DateTime.Now,
                        Contrasenia = cliente.Contrasenia
                    };
                    db.Clientes.Add(clienteNuevo);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Clientes");
                }
                else
                {
                    return RedirectToAction("Error", "Shared");
                }

            }

            return RedirectToAction("Index", "Clientes");
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

        public bool buscarCliente(string Email)
        {
            using(DistribuidoraContext db = new DistribuidoraContext())
            {
                Cliente cli = (from c in db.Clientes
                               where c.Email == Email
                               select c).SingleOrDefault();
                Empleado emp = (from e in db.Empleados
                                where e.Email == Email
                                select e).SingleOrDefault();

                if(cli == null && emp == null)
                {
                    return false;
                }
               else
                {
                    return true;
                }
            }        
        }
        #endregion
            
        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
