using System.ComponentModel.DataAnnotations;

namespace ReviewIT
{
    public class EditProfileViewModel
    {
        // [Display(Name = "UserId"), Required]
        // public string UserId { get; set; }

        [Display(Name = "NickName"), Required]
        public string NickName { get; set; }

        [Display(Name = "Email Address"), Required]
        public string EmailAddress { get; set; }

        [Required, Display(Name = "Current password")]
        public string OldPassword { get; set; }

        // [Compare("ConfirmPassword"), Display(Name = "New password")]
        [Compare(nameof(ConfirmPassword)), Display(Name = "New password")]        
        public string NewPassword { get; set; }

        [Display(Name = "Confirm your password.")]
        public string ConfirmPassword { get; set; }
    }
}