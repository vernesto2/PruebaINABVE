using System;

namespace WebApplication1.Models
{
    public class BeneficiosVeteranosDTO
    {
        public long Id { get; set; }
        public long? IdVeterano { get; set; }
        public long? IdBeneficio { get; set; }
        public DateTime? FechaCreado { get; set; }
        public bool? Activo { get; set; }
        public string Nombre { get; set; }
    }
}
