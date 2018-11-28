using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Distribuidora.Models
{
    [Table("Producto")]
    public class Producto
    {
        #region Atributos

        [Key]
        public int Codigo { get; set; }
        [MaxLength(50)]
        [Required]
        public string Nombre { get; set; }
        [MaxLength(100)]
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public double Costo { get; set; }
        [Required]
        public double PrecioSugerido { get; set; }

        public double PrecioVenta { get; set; }
        public List<Empleado> Empleados { get; set; }

        #endregion

    }
}