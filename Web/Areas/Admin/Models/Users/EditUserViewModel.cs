﻿using Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models.Users
{
    public class EditUserViewModel
    {
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
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Literals), ErrorMessageResourceName = nameof(Literals.RequiredFieldError))]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
