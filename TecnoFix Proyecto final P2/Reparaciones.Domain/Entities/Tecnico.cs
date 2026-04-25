using Reparaciones.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace Reparaciones.Domain.Entities
{
    public class Tecnico : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Apellido { get; set; } = null!;

        [MaxLength(100)]
        public string Especialidad { get; set; } = null!;

        [MaxLength(20)]
        public string Telefono { get; set; } = null!;

        public ICollection<Reparacion> Reparaciones { get; set; } = new List<Reparacion>();
    }
}
