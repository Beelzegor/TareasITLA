using Reparaciones.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace Reparaciones.Domain.Entities
{
    public class Electrodomestico : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Marca { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Modelo { get; set; } = null!;

        [MaxLength(100)]
        public string? NumeroSerie { get; set; }

        [Required]
        [MaxLength(80)]
        public string Tipo { get; set; } = null!;

        public ICollection<Reparacion> Reparaciones { get; set; } = new List<Reparacion>();
    }
}
