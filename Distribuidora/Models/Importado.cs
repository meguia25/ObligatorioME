using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Distribuidora.Models
{
    public class Importado:Producto
    {
        #region Atributos
        [MaxLength(30)]
        public string PaisOrigen { get; set; }
        public int CantMinima { get; set; }

        #endregion

    }
}