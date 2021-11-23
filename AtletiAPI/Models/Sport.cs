using System;
using System.Collections.Generic;

#nullable disable

namespace AtletiAPI.Models
{
    public partial class Sport
    {
        public Sport()
        {
            Athletes = new HashSet<Athlete>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Athlete> Athletes { get; set; }
    }
}
