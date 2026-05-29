using System.ComponentModel.DataAnnotations.Schema;

namespace RappiDozApp.Models
{
    public class Coupon
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Discount { get; set; }

        public bool IsPercentage { get; set; }

        public string ExpirationDate { get; set; }

        public bool Active { get; set; }

        public int? Stock { get; set; }

        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int? OrderId { get; set; }

        public virtual Order Order { get; set; }

        public int? UserId { get; set; } // ID of user who reserved the coupon

        public virtual User User { get; set; }
    }
}