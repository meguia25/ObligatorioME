using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Distribuidora.Models
{
    [Table("Item")]
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdItem { get; set; }
        public int CantUnidades { get; set; }
        public double precioProducto { get; set; }
        public Producto Producto { get; set; }

        public Item(int cantUnidades, Producto producto)
        {
            this.CantUnidades = cantUnidades;
            this.Producto = producto;
        }

        public Item()
        {
        }
    }
}