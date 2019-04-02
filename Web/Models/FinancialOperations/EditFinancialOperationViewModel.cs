using Core.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.FinancialOperations
{
    public class EditFinancialOperationViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = nameof(Literals.RquiredFieldError), ErrorMessageResourceType = typeof(Literals))]
        public double Amount { get; set; }

        [Required(ErrorMessageResourceName = nameof(Literals.RquiredFieldError), ErrorMessageResourceType = typeof(Literals))]
        public DateTimeOffset Timestamp { get; set; }

        [Required(ErrorMessageResourceName = nameof(Literals.RquiredFieldError), ErrorMessageResourceType = typeof(Literals))]
        public int FinancialItemId { get; set; }

        public IEnumerable<SelectListItem> FinancialItems { get; set; }

        [MaxLength(50, ErrorMessageResourceType = typeof(Literals), ErrorMessageResourceName = nameof(Literals.MaxLengthError))]
        public string Description { get; set; }
    }
}
