using System.ComponentModel.DataAnnotations.Schema;

namespace RappiDozApp.Models
{
    public class Dish
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public string Img { get; set; }

        public string Description { get; set; }

        public int RestaurantId { get; set; } // Link to restaurant menu

        public virtual Restaurant Restaurant { get; set; }
    }
}