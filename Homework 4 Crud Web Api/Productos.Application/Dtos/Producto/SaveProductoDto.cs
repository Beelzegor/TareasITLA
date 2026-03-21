using System.ComponentModel.DataAnnotations;

namespace Productos.Application.Dtos.Producto
{
    public class SaveProductoDto
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string Descripcion { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(0.01, 999999.99, ErrorMessage = "El campo {0} debe estar entre {1} y {2}.")]
        public decimal Precio { get; set; }
    }
}
