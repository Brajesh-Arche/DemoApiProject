using System;
using System.ComponentModel.DataAnnotations;

namespace ArcheProjectWebApp.Models
{
    public class AccountViewModel
    {
        
        [Required(ErrorMessage ="Please Enter UserName") ]
        [Display(Name ="Old Password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required(ErrorMessage ="Please Enter Password")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage ="Please Enter same Password again")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="New Password and Confirm Password not Match Plz check...")]
        public string ConfirmPassword { get; set; }
        public Nullable<bool> Is_Deleted { get; set; }

    }
}
