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
    public class EmpleadoesController : Controller
    {
        private DistribuidoraContext db = new DistribuidoraContext();

        #region Login
        [HttpGet]
        public ActionResult Login()
        {
            return View(new Empleado());
        }

        [HttpPost]
        public ActionResult Login(Empleado u)
        {

            //if (ModelState.IsValid)
            //{
                Empleado empleadoUsuario;
                using (db)
                {
                    string email = u.Email;
                    string contrasenia = u.Contrasenia;
                    empleadoUsuario = db.Empleados.FirstOrDefault(e => e.Email == email && e.Contrasenia == contrasenia);
                    if (empleadoUsuario != null)
                    {
                        return RedirectToAction("Buscar", "Productos");
                    }
                    else
                    {
                        ModelState.AddModelError("Error","Email o contraseña incorrectos");
                    }
                }
            //}
            return View(u);
        }

        #endregion

        #region Asignar precio de venta al producto







        #endregion
        // GET: Empleadoes
        public ActionResult Index()
        {
            return View(db.Empleados.ToList());
        }

        // GET: Empleadoes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: Empleadoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empleadoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Email,Nombre,Contrasenia")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                if (ValidacionContrasenia(empleado.Contrasenia))
                {
                    db.Empleados.Add(empleado);
                    db.SaveChanges();
                    return RedirectToAction("Buscar", "Productos");
                }

            }

            return View(empleado);
        }

        public bool ValidacionContrasenia(string Contrasenia)
        {
            if(Contrasenia.Length >= 6)
            {
                if (new Regex("[a-z]").IsMatch(Contrasenia) && new Regex("[A-Z]").IsMatch(Contrasenia) && new Regex("[0-9]").IsMatch(Contrasenia))
                {
                    return true;
                }
            }
            return false;
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
