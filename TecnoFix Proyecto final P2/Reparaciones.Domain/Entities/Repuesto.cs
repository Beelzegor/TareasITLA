using Reparaciones.Domain.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reparaciones.Domain.Entities
{
    public class Repuesto : BaseEntity
    {
        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; } = null!;

        [MaxLength(500)]
        public string? Descripcion { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public ICollection<ReparacionRepuesto> ReparacionRepuestos { get; set; } = new List<ReparacionRepuesto>();
    }
}
