using System;
using System.Collections.Generic;

#nullable disable

namespace AtletiAPI.Models
{
    public partial class Nationality
    {
        public Nationality()
        {
            Athletes = new HashSet<Athlete>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Athlete> Athletes { get; set; }
    }
}
