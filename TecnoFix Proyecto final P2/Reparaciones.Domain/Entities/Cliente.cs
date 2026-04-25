using Reparaciones.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace Reparaciones.Domain.Entities
{
    public class Cliente : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Apellido { get; set; } = null!;

        [MaxLength(20)]
        public string Telefono { get; set; } = null!;

        [MaxLength(150)]
        [EmailAddress]
        public string? Email { get; set; }

        [MaxLength(250)]
        public string? Direccion { get; set; }

        public ICollection<Reparacion> Reparaciones { get; set; } = new List<Reparacion>();
    }
}
