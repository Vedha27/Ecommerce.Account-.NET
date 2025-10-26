using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class UserRegisterDto
    {

        public string FullName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email is required.")] 
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Phone number is required.")]
        public string PhoneNumber { get; set; } = string.Empty;

        public string RoleName { get; set; } = "Customer";

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = string.Empty;


        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
