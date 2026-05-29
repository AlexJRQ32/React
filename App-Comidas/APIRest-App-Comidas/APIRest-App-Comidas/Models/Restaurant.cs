using System.Collections.Generic;

namespace RappiDozApp.Models
{
    public class Restaurant
    {
        public int Id { get; set; }

        public string TradeName { get; set; }

        public string Address { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public string OpeningTime { get; set; }

        public string ClosingTime { get; set; }

        public string Img { get; set; }

        public string Rating { get; set; }

        public bool IsOpen { get; set; }

        public string DeliveryFee { get; set; }

        public string DeliveryTime { get; set; }

        public virtual ICollection<Dish> Dishes { get; set; } = new List<Dish>();

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}