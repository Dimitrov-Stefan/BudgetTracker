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
        [MaxLength(256, ErrorMessageResourceType = typeof(Literals), ErrorMessageResourceName = nameof(Literals.MaxLengthError))]
        [EmailAddress(ErrorMessageResourceName = nameof(Literals.MailNotValid), ErrorMessageResourceType = typeof(Literals))]
        public string Email { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessageResourceName = nameof(Literals.PasswordsDoNotMatch), ErrorMessageResourceType = typeof(Literals))]
        public string ConfirmNewPassword { get; set; }
    }
}
