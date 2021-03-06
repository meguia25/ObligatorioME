﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Distribuidora.Models;

namespace Distribuidora.Controllers
{   
    public class ProductoesController : Controller
    {
        private DistribuidoraContext db = new DistribuidoraContext();

        // GET: Productoes
        public ActionResult Index()
        {
            List<Producto> productos = new List<Producto>();
            using (DistribuidoraContext db = new DistribuidoraContext())
            {
                productos = db.Productos.ToList();
            }
            return View(productos);
        }

        #region Detalles
        // GET: Productoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }
        #endregion

        #region Editar
        // GET: Productoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,PrecioVenta")] Producto producto)
        {
            using(DistribuidoraContext db = new DistribuidoraContext())
            {
                Producto prod = (from p in db.Productos
                                 where p.Codigo == producto.Codigo                                                           
                                 select p).SingleOrDefault();

                if(prod.PrecioSugerido < producto.PrecioVenta && producto.PrecioVenta <= (prod.PrecioSugerido)*1.10  )
                {
                    prod.PrecioVenta = producto.PrecioVenta;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Productos");
                }
            }
            return View(producto);
        }
        #endregion

        #region Buscar (sin terminar)
        //[HttpPost]
        public ActionResult Buscar(string filtroBusqueda, string parametroBusqueda, string rangoPrecios)
        {
            List<Producto> productos = new List<Producto>();
            //ViewProductos vp = new ViewProductos(fabricado, importado);
            using(DistribuidoraContext db = new DistribuidoraContext())
            {
                if (filtroBusqueda == "Codigo")
                {
                    int codigo = Int32.Parse(parametroBusqueda);
                    productos = db.Productos.Where(p => p.Codigo == codigo).ToList();
                }
                else if (filtroBusqueda == "TextoNombre")
                {
                    productos = db.Productos.Where(p => p.Nombre.Contains(parametroBusqueda)).ToList();
                }
                else if (filtroBusqueda == "TextoDescripcion")
                {
                    productos = db.Productos.Where(p => p.Descripcion.Contains(parametroBusqueda)).ToList();
                }
                else if (filtroBusqueda == "RangoPrecios")
                {
                    int precio = Int32.Parse(rangoPrecios);
                    if(precio == 200)
                    {
                        productos = db.Productos.Where(p => p.PrecioVenta >= 0 && p.PrecioVenta <= 200).ToList();
                    }
                    else if(precio == 500)
                    {
                        productos = db.Productos.Where(p => p.PrecioVenta >= 201 && p.PrecioVenta <= 500).ToList();
                    }
                    else if (precio == 1000)
                    {
                        productos = db.Productos.Where(p => p.PrecioVenta >= 501 && p.PrecioVenta <= 1000).ToList();
                    }
                    else if (precio == 1001)
                    {
                        productos = db.Productos.Where(p => p.PrecioVenta <= 1001).ToList();
                    }
                }
                else if (filtroBusqueda == "TipoProducto")
                {
                    if (parametroBusqueda == "Fabricado")
                    {
                        //productos = db.Productos.Where(p => p.Codigo).ToList();
                        //como accedo a Fabricado o importado? con el view models, pero me falta la consulta
                    }
                    if (parametroBusqueda == "Importado")
                    {
                        //  productos = db.Productos.Where(p => p.Importado).ToList();
                        //como accedo a Fabricado o importado?
                    }
                }
                else
                {
                    //Falta ordernar por tipo de producto, pero no se como acceder aun a Fabricado o Importado
                    productos = db.Productos.OrderBy(p => p.Nombre).ToList();
                }
                //productos = db.Productos.ToList();
            }

            return View(productos);
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
