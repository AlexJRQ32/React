using System.ComponentModel.DataAnnotations;

namespace RappiDozApp.Models
{
    public class Address
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; } // Only the name of the location/address

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}