﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Distribuidora.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Contrasenia { get; set; }
        [Key]
        public string Email { get; set; }
    }
}