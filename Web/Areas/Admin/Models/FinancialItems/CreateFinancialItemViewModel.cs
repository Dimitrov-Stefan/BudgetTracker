using Core.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.Models.FinancialItems
{
    public class CreateFinancialItemViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessageResourceName = nameof(Literals.RequiredFieldError), ErrorMessageResourceType = typeof(Literals))]
        [MaxLength(50, ErrorMessageResourceType = typeof(Literals), ErrorMessageResourceName = nameof(Literals.MaxLengthError))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = nameof(Literals.RequiredFieldError), ErrorMessageResourceType = typeof(Literals))]
        public FinancialItemType Type { get; set; }

        public IEnumerable<SelectListItem> Types { get; set; }
    }
}
