using System.Collections.Generic;

namespace RappiDozApp.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

        public string Email { get; set; }

        public string Img { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

        public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
    }
}