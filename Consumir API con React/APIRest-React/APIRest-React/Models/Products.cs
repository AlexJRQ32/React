using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIRest_React.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("NOMBRE_PRODUCTO")]
        public string Nombre { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [Required]
        public int Stock { get; set; }

        public bool? Activo { get; set; } = true;
    }
}
