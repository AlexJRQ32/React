using System.Collections.Generic;

namespace RappiDozApp.Models
{
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Subtitle { get; set; }

        public string Site { get; set; }

        public string Icon { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}