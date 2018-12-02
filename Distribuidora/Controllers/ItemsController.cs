using System;
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
    public class ItemsController : Controller
    {
        private DistribuidoraContext db = new DistribuidoraContext();

        public List<Item> GetCarrito
        {
            get { return Session["Carrito"] as List<Item>; }
        }

        //si el carrito no esta creado, lo crea, si ya existe un carrito agrega al producto
        private void AddCarrito(Item i) {
            var carrito = Session["Carrito"] as List<Item>;
            if (carrito == null)
                carrito = new List<Item>();
            carrito.Add(i);
            Session["Carrito"] = carrito;
        }

        private bool ProductoCarrito(Producto producto)
        {
            bool retorno = false;
            if (GetCarrito != null)
            {
                foreach (Item i in GetCarrito)
                {
                    if (i.Producto.Codigo == producto.Codigo)
                    {
                        i.CantUnidades++;
                        retorno = true;
                    }
                }
            }
            return retorno;
        }

        #region Visualizacion del carrito
        // GET: Items
        public ActionResult Carrito()
        {
            List<Item> viewCarrito = new List<Item>();
            Cliente cliente = Session["Cliente"] as Cliente;
            if (cliente != null)
            {
                foreach(Item i in GetCarrito)
                {
                    viewCarrito.Add(i);
                }
                //ViewBag.GetCarrito = GetCarrito;
                return View(viewCarrito);
            }
            return View();

        }
        #endregion

        #region agregar al carrito
        [HttpGet]
        //[Route("AgregarCarrito/{cod}")]
        public JsonResult AgregarCarrito(int id)
        {
            Cliente cliente = Session["Cliente"] as Cliente;
            Producto elProducto = buscarProducto(id);
            if (cliente != null && !ProductoCarrito(elProducto))
            {
                Item unItem = new Item(1, elProducto);
                AddCarrito(unItem);
            }
            //else if(cliente != null && ProductoCarrito(elProducto))
            //{

            //}
            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public Producto buscarProducto(int idProducto)
        {
            using (DistribuidoraContext db = new DistribuidoraContext())
            {
                Producto pro = (from c in db.Productos
                                where c.Codigo == idProducto
                                select c).SingleOrDefault();

                return pro;
            }
        }

        #endregion

        #region Editar el carrito
        public ActionResult bajarCantidad(int id)
        {
            int i = 0;
            bool bandera = false;
            while (i < GetCarrito.Count && !bandera)
            {
                if (GetCarrito[i].Producto.Codigo == id && GetCarrito[i].CantUnidades != 0)
                {
                    GetCarrito[i].CantUnidades = GetCarrito[i].CantUnidades - 1;
                    bandera = true;
                }
                i++;
            }
            return RedirectToAction("Carrito");
        }
        public ActionResult aumentarCantidad(int id)
        {
            int i = 0;
            bool bandera = false;
            while (i < GetCarrito.Count && !bandera)
            {
                if (GetCarrito[i].Producto.Codigo == id)
                {
                    GetCarrito[i].CantUnidades = GetCarrito[i].CantUnidades + 1;
                    bandera = true;
                }
                i++;
            }
            return RedirectToAction("Carrito");
        }


        public ActionResult borrarProducto(int id)
        {
            int i = 0;
            bool bandera = false;
            while (i < GetCarrito.Count && !bandera){
                if (GetCarrito[i].Producto.Codigo == id)
                {
                    GetCarrito.RemoveAll(c => c.Producto.Codigo.Equals(id));
                    bandera = true;
                }
                i++;
            }
            return RedirectToAction("Carrito");
        }

        #endregion

        #region Confirmar carrito
        //[ValidateAntiForgeryToken]
        public ActionResult ConfirmarCarrito()
        {
            if (ModelState.IsValid)
            {
                Cliente cliente = Session["Cliente"] as Cliente;
                if (cliente != null)
                {
                    using (DistribuidoraContext db = new DistribuidoraContext())
                    {
                        Pedido pedido = new Pedido()
                        {
                            Cliente = cliente,
                            Fecha = DateTime.Now,
                            Items = GetCarrito,
                            TotalPedido = calcularTotalCarrito()

                        };

                        foreach(Item i in GetCarrito)
                        {
                            Item items = new Item()
                            {
                                CantUnidades = i.CantUnidades,
                                Producto = i.Producto,
                                precioProducto = i.Producto.PrecioVenta
                            };
                            db.Items.Add(items);
                        }
                        GetCarrito.Clear();
                        db.Pedidos.Add(pedido);
                        db.SaveChanges();
                        return RedirectToAction("Buscar", "Productoes");
                    }
                }
            }
            return View();
        }

        public double calcularTotalCarrito()
        {
            double total = 0;
            foreach(Item i in GetCarrito)
            {
                total =+ i.CantUnidades * i.Producto.PrecioVenta;
            }
            return total;
        }
        #endregion

        #region dispose
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
