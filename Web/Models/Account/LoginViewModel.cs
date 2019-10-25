using Core.Resources;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceName = nameof(Literals.RequiredFieldError), ErrorMessageResourceType = typeof(Literals))]
        [MaxLength(256, ErrorMessageResourceType = typeof(Literals), ErrorMessageResourceName = nameof(Literals.MaxLengthError))]
        [EmailAddress(ErrorMessageResourceName = nameof(Literals.MailNotValid), ErrorMessageResourceType = typeof(Literals))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = nameof(Literals.RequiredFieldError), ErrorMessageResourceType = typeof(Literals))]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
