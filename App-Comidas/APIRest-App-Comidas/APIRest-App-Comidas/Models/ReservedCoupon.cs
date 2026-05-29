using RappiDozApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIRest_App_Comidas.Models
{
    public class ReservedCoupon
    {
        [Key]
        public int Id { get; set; }
        public int CouponId { get; set; }
        [ForeignKey("CouponId")]
        public virtual Coupon? Coupon { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        public DateTime ReservedAt { get; set; } = DateTime.UtcNow;
    }
}
