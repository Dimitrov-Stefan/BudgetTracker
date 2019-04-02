﻿using Core.Resources;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.FinancialOperations
{
    public class EditFinancialOperationViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = nameof(Literals.RquiredFieldError), ErrorMessageResourceType = typeof(Literals))]
        [MaxLength(50, ErrorMessageResourceType = typeof(Literals), ErrorMessageResourceName = nameof(Literals.MaxLengthError))]
        public string Description { get; set; }
    }
}
