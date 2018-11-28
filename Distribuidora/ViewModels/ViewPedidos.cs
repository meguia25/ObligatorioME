using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Distribuidora.Models;

namespace Distribuidora.ViewModels
{
    public class ViewPedidos
    {
        public Pedido Pedido { get; set; }
        public Cliente Cliente { get; set; }
        public Producto Producto { get; set; }

        public ViewPedidos(Cliente cliente, Producto producto, Pedido pedido)
        {
            this.Pedido = pedido;
            this.Cliente = cliente;
            this.Producto = producto;
        }
        public ViewPedidos()
        {

        }
    }
}