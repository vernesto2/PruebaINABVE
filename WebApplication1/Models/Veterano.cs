using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Veterano
    {
        public Veterano()
        {
            BeneficiosVeteranos = new HashSet<BeneficiosVeteranos>();
        }

        public long Id { get; set; }
        public string Dui { get; set; }
        public string Carnet { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public DateTime? FechaCreado { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<BeneficiosVeteranos> BeneficiosVeteranos { get; set; }
    }
}
