using System.ComponentModel.DataAnnotations;

namespace Corporation.PL.ViewModels.Identity
{
    public class SignUpViewModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "LastName")]
        public string LastName { get; set; } = null!;
        
        [Display(Name = "UserName")]
        public string UserName { get; set; } = null!;

        [EmailAddress]
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Confirm password doesn't match with password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = null!;
       
        [Display(Name = "Is Agree")]
        public bool IsAgree { get; set; }
    }
}
