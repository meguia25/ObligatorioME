using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Distribuidora.Models;

namespace Distribuidora.ViewModels
{
    public class ViewProductos
    {
        public Fabricado Fabricado { get; set; }
        public Importado Importado { get; set; }

        public ViewProductos(Fabricado fabricado, Importado importado)
        {
            this.Fabricado = fabricado;
            this.Importado = importado;
        }
    }
}