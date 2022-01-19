using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class BeneficiosVeteranos
    {
        public long Id { get; set; }
        public long? IdVeterano { get; set; }
        public long? IdBeneficio { get; set; }
        public DateTime? FechaCreado { get; set; }
        public bool? Activo { get; set; }

        public virtual Beneficio IdBeneficioNavigation { get; set; }
        public virtual Veterano IdVeteranoNavigation { get; set; }
    }
}
