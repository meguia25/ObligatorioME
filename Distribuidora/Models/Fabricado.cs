using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Distribuidora.Models
{
    
    public class Fabricado:Producto
    {

        #region Atributos
        public double Tiempo { get; set; }
        public ICollection<Fabricacion> listaFab { get; set; }


        #endregion

    }
}