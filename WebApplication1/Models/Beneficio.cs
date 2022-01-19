using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Beneficio
    {
        public Beneficio()
        {
            BeneficiosVeteranos = new HashSet<BeneficiosVeteranos>();
        }

        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaCreado { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<BeneficiosVeteranos> BeneficiosVeteranos { get; set; }
    }
}
