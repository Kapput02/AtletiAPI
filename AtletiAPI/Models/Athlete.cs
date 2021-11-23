using System;
using System.Collections.Generic;

#nullable disable

namespace AtletiAPI.Models
{
    public partial class Athlete
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? FkNationality { get; set; }
        public string Sex { get; set; }
        public DateTime? Dob { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public int FkSport { get; set; }
        public int? Gold { get; set; }
        public int? Silver { get; set; }
        public int? Bronze { get; set; }

        public virtual Nationality Nationality { get; set; }
        public virtual Sport Sport { get; set; }
    }
}
