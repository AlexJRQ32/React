using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RappiDozApp.Models
{
    public class PaymentMethod
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Tipo { get; set; }

        public string Descripcion { get; set; }

        public string Icono { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}