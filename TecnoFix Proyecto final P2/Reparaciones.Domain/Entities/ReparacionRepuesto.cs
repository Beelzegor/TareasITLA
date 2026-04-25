namespace Reparaciones.Domain.Entities
{
    public class ReparacionRepuesto
    {
        public int ReparacionId { get; set; }
        public Reparacion? Reparacion { get; set; }

        public int RepuestoId { get; set; }
        public Repuesto? Repuesto { get; set; }

        public int Cantidad { get; set; }
    }
}
