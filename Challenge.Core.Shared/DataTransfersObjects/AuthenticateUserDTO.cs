using System.ComponentModel.DataAnnotations;

namespace Challenge.Core.Shared.DataTransferObjects
{
    public class AuthenticateUserDTO
    {
        [Required(ErrorMessage = "User is Required")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string? Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
        public string? Blob { get; set; }

    }
}