﻿using Core.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.Models.FinancialOperations
{
    public class CreateFinancialOperationViewModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = nameof(Literals.RequiredFieldError), ErrorMessageResourceType = typeof(Literals))]
        public double Amount { get; set; }

        [Required(ErrorMessageResourceName = nameof(Literals.RequiredFieldError), ErrorMessageResourceType = typeof(Literals))]
        public DateTimeOffset Timestamp { get; set; }

        [Required(ErrorMessageResourceName = nameof(Literals.RequiredFieldError), ErrorMessageResourceType = typeof(Literals))]
        public int FinancialItemId { get; set; }

        public IEnumerable<SelectListItem> FinancialItems { get; set; }

        [MaxLength(50, ErrorMessageResourceType = typeof(Literals), ErrorMessageResourceName = nameof(Literals.MaxLengthError))]
        public string Description { get; set; }
    }
}
