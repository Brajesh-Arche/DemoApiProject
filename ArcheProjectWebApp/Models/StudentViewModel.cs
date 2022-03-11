using System.ComponentModel.DataAnnotations;

namespace ArcheProjectWebApp.Models
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        [Display(Name ="First Name")]
        [Required(ErrorMessage ="Please Enter First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Gender { get; set; }
        [Required(ErrorMessage ="Please Enter Your Age")]
        public int Age { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
    }
}
