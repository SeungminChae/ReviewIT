using System.ComponentModel.DataAnnotations;
using ReviewIT.DataAccess;

namespace ReviewIT
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email Address or ID")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password{ get; set; }
    }
}