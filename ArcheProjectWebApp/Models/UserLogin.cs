using System;
using System.ComponentModel.DataAnnotations;

namespace ArcheProjectWebApp.Models
{
    public class UserLogin
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage ="Please Enter UserName")]
        [Display(Name ="User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Please Enter Your EmailId")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
        [Required(ErrorMessage ="Please Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage ="Please Enter Same Password again")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "New Password and Confirm Password not Match Plz check...")]
        public string ConfirmPassword { get; set; }
        public bool? Is_Deleted { get; set; }
        public string ResetCode { get; set; }
    }
}
