using Core.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.Models.Users
{
    public class EditUserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessageResourceName = nameof(Literals.RequiredFieldError), ErrorMessageResourceType = typeof(Literals))]
        [MaxLength(50, ErrorMessageResourceType = typeof(Literals), ErrorMessageResourceName = nameof(Literals.MaxLengthError))]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessageResourceName = nameof(Literals.RequiredFieldError), ErrorMessageResourceType = typeof(Literals))]
        [MaxLength(50, ErrorMessageResourceType = typeof(Literals), ErrorMessageResourceName = nameof(Literals.MaxLengthError))]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Literals), ErrorMessageResourceName = nameof(Literals.RequiredFieldError))]
        [MaxLength(256, ErrorMessageResourceType = typeof(Literals), ErrorMessageResourceName = nameof(Literals.MaxLengthError))]
        [EmailAddress(ErrorMessageResourceName = nameof(Literals.MailNotValid), ErrorMessageResourceType = typeof(Literals))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Literals), ErrorMessageResourceName = nameof(Literals.RequiredFieldError))]
        public string Password { get; set; }

        public bool IsActive { get; set; }
    }
}
