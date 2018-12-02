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
using System.IO;

namespace Distribuidora.Controllers
{
    public class EmpleadoesController : Controller
    {
        private DistribuidoraContext db = new DistribuidoraContext();
        public ActionResult Index()
        {
            return View(db.Empleados.ToList());
        }

        #region Login
        [HttpGet]
        public ActionResult Login()
        {
            return View(new Empleado());
        }

        [HttpPost]
        public ActionResult Login(Empleado u)
        {
                Empleado empleadoUsuario;
                using (DistribuidoraContext db = new DistribuidoraContext())
                {
                    string email = u.Email;
                    string contrasenia = u.Contrasenia;
                    empleadoUsuario = db.Empleados.FirstOrDefault(e => e.Email == email && e.Contrasenia == contrasenia);
                    if (empleadoUsuario != null)
                    {
                        Session["Empleado"] = empleadoUsuario;
                        return RedirectToAction("Index", "Empleadoes");
                    }
                    else
                    {
                    return RedirectToAction("Error", "Shared");
                }
                }
        }

        #endregion

        #region alta usuario empleado (no es requerimiento)
        // GET: Empleadoes/Create
        public ActionResult Create()
        {
            return View();
        }


        //// POST: Empleadoes/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Email,Nombre,Contrasenia")] Empleado empleado)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (ValidacionContrasenia(empleado.Contrasenia))
        //        {
        //            db.Empleados.Add(empleado);
        //            db.SaveChanges();
        //            return RedirectToAction("Buscar", "Productos");
        //        }

        //    }

        //    return View(empleado);
        //}

        //public bool ValidacionContrasenia(string Contrasenia)
        //{
        //    if(Contrasenia.Length >= 6)
        //    {
        //        if (new Regex("[a-z]").IsMatch(Contrasenia) && new Regex("[A-Z]").IsMatch(Contrasenia) && new Regex("[0-9]").IsMatch(Contrasenia))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        #endregion

        //#region Carga de productos
        //    public List<Producto> CargaProductos()
        //    {
        //    string archivo = (HttpRuntime.AppDomainAppPath + ConfigurationManager.AppSettings["/Archivos/ProductosTodos.txt"]);
        //    ;
        //    using (DistribuidoraContext db = new DistribuidoraContext())
        //    {
        //        List<Producto> productos = new List<Producto>();
        //        List<Empleado> empleados = new List<Empleado>();
        //        string[] filas = System.IO.File.ReadAllLines(archivo);
        //        foreach (string fila in filas)
        //        {

        //            string[] productoFila = fila.Split('|');
        //            Producto producto = new Producto()
        //            {
        //                Nombre = productoFila[0],
        //                Descripcion = productoFila[1],
        //                Costo = double.Parse(productoFila[2]),
        //                PrecioSugerido = double.Parse(productoFila[3]),
        //                PrecioVenta  = double.Parse(productoFila[3]),
        //            };
        //            Fabricado fabricado = new Fabricado()
        //            {
        //                Tiempo = double.Parse(productoFila[5])
        //            };
        //            Importado importado = new Importado()
        //            {
        //                PaisOrigen = productoFila[6],
        //                CantMinima = int.Parse(productoFila[7])
        //            };
        //            db.Productos.Add(producto);
        //            db.SaveChanges();
        //        }
        //        }
        //     return
        //    }
        //#endregion

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
