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
        public double TotalPedido { get; set; }
        public List<Item> Items { get; set; }



    }
}