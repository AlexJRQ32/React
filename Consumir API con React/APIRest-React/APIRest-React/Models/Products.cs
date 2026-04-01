using System.ComponentModel.DataAnnotations;

namespace APIRest_React.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public decimal Precio { get; set; }
        [Required]
        public int Stock { get; set; }
    }
}
