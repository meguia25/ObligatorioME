using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Distribuidora.Models
{
    [Table("Fabricacion")]
    public class Fabricacion
    {

        #region Atributos
        [Key]
        public int IdFabricacion { get; set; }
        [Required]
        [MaxLength(100)]
        public string Descripcion { get; set; }

        public int Tiempo { get; set; }


        #endregion

    }
}