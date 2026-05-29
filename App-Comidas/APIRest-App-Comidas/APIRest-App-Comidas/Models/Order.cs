using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RappiDozApp.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string Restaurant { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending";

        public string Date { get; set; } = string.Empty;

        public string Time { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int CustomerId { get; set; }

        public virtual User Customer { get; set; }

        public string PaymentMethodId { get; set; } = string.Empty;

        public virtual PaymentMethod PaymentMethod { get; set; }

        public string AddressId { get; set; } = string.Empty;

        public virtual Address Address { get; set; }

        public int Total { get; set; }

        public int RestaurantId { get; set; }
        [ForeignKey("RestaurantId")]
        public virtual Restaurant? RestaurantRef { get; set; }

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}