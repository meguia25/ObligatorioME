using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Distribuidora.Models
{
    [Table("Pedido")]
    public class Pedido
    {
        [Key]
        public int IdPedido { get; set; }
        public Cliente Cliente { get; set; }

        public DateTime Fecha { get; set; }
        public int TotalPedido { get; set; }
        public Producto Producto { get; set; }
        public int CantUnidades { get; set; }
        public int Estado { get; set; } //0 en carrito, 1 confirmado

    }
}