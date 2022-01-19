﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public int Tipo { get; set; }
    }
}
