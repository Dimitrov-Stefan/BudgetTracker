using System.Collections.Generic;
using Models.Entities;

namespace Web.Areas.Admin.Models.FinancialItems
{
    public class FinancialItemListViewModel
    {
        public IEnumerable<FinancialItem> FinancialItems { get; set; }
    }
}
