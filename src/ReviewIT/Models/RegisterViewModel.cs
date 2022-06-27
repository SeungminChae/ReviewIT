using System.ComponentModel.DataAnnotations;
using ReviewIT.DataAccess;

namespace ReviewIT
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User Id (This will be used to log-in)")]
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Password must be between 5 and 255 characters", MinimumLength = 5)]
        public string Password { get; set;}

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [Display(Name = "Confirm your Password")]
        public string PasswordConfirm { get; set;}
        
        [Required]
        [Display(Name = "Nickname (This will be displayed to other users.)")]
        public string NickName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        
    }
}