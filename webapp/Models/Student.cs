using System;
using System.Collections.Generic;

namespace webapp.Models
{
    public partial class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool Gender { get; set; }
        public string? DepartId { get; set; }
        public DateTime? Dob { get; set; }
        public double Gpa { get; set; }

        public virtual Department? Depart { get; set; }
    }
}
