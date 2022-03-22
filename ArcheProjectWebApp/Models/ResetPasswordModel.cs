using System.ComponentModel.DataAnnotations;

namespace ArcheProjectWebApp.Models
{
    public class ResetPasswordModel
    {
        public string Code { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage ="Please Enter New Password")]
        [DataType(DataType.Password)]
        [Display(Name ="New Password")]
        public string NewPassword { get; set; }
        [Compare("NewPassword",ErrorMessage ="")]
        public string ConfirmPassword { get; set; }
    }
}
