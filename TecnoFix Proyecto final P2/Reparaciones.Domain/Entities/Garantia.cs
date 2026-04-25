using Reparaciones.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace Reparaciones.Domain.Entities
{
    public class Garantia : BaseEntity
    {
        public int ReparacionId { get; set; }
        public Reparacion? Reparacion { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        [MaxLength(500)]
        public string? Descripcion { get; set; }

        public bool EstaVigente => DateTime.UtcNow <= FechaFin;
    }
}
