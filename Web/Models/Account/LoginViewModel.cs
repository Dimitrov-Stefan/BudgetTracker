using System.ComponentModel.DataAnnotations;
using Core.Resources;

namespace Web.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceName = nameof(Literals.RequiredFieldError), ErrorMessageResourceType = typeof(Literals))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = nameof(Literals.RequiredFieldError), ErrorMessageResourceType = typeof(Literals))]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
