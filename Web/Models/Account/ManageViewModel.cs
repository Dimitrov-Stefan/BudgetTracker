using Core.Resources;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.Account
{
    public class ManageViewModel
    {
        [Required(ErrorMessageResourceName = nameof(Literals.RequiredFieldError), ErrorMessageResourceType = typeof(Literals))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = nameof(Literals.RequiredFieldError), ErrorMessageResourceType = typeof(Literals))]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = nameof(Literals.RequiredFieldError), ErrorMessageResourceType = typeof(Literals))]
        public string Email { get; set; }

        //[Required(ErrorMessageResourceName = nameof(Literals.RequiredFieldError), ErrorMessageResourceType = typeof(Literals))]
        public string Password { get; set; }

        [Compare("Password", ErrorMessageResourceName = nameof(Literals.PasswordsDoNotMatch), ErrorMessageResourceType = typeof(Literals))]
        public string ConfirmPassword { get; set; }
    }
}
