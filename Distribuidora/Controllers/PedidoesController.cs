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
    public class PedidoesController : Controller
    {
        private DistribuidoraContext db = new DistribuidoraContext();

        #region Lista de Pedidos  (modificar la vista)
        public ActionResult ListaPedidos()
        {
            Cliente cliente = Session["Cliente"] as Cliente;
            if (cliente != null)
            {
                using (DistribuidoraContext db = new DistribuidoraContext())
                {
                    List<Pedido> pedi = db.Pedidos.Where(p => p.Cliente.IdCliente == cliente.IdCliente)                                            
                                            .Include("Items")
                                            .ToList();

                    return View(pedi);
                }
            }           
            return View();              
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
