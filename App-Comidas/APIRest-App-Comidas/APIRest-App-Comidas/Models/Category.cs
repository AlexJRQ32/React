using System.ComponentModel.DataAnnotations;

namespace RappiDozApp.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Icon { get; set; }

        public string Slug { get; set; }

        public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
        public virtual ICollection<Dish> Dishes { get; set; } = new List<Dish>();
        public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
    }
}
