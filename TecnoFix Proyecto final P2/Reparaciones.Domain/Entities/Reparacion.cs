using Reparaciones.Domain.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reparaciones.Domain.Entities
{
    public class Reparacion : BaseEntity
    {
        public DateTime FechaRecepcion { get; set; } = DateTime.UtcNow;

        public DateTime? FechaEntrega { get; set; }

        [MaxLength(1000)]
        public string? Diagnostico { get; set; }

        [Required]
        [MaxLength(50)]
        public string Estado { get; set; } = "Recibido";

        [Column(TypeName = "decimal(18,2)")]
        public decimal CostoManoObra { get; set; }

        [MaxLength(500)]
        public string? Observaciones { get; set; }

        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public int ElectrodomesticoId { get; set; }
        public Electrodomestico? Electrodomestico { get; set; }

        public int TecnicoId { get; set; }
        public Tecnico? Tecnico { get; set; }

        public Garantia? Garantia { get; set; }
        public ICollection<ReparacionRepuesto> ReparacionRepuestos { get; set; } = new List<ReparacionRepuesto>();
    }
}
