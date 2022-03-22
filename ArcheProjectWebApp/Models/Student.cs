using System;
using System.Collections.Generic;

#nullable disable

namespace ArcheProjectWebApp.Models
{
    public partial class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
    }
}
