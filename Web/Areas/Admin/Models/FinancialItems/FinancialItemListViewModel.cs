using System.Collections.Generic;
using Models.Entities;

namespace Web.Areas.Admin.Models.FinancialItems
{
    public class FinancialItemListViewModel
    {
        public int UserId { get; set; }

        public IEnumerable<FinancialItem> FinancialItems { get; set; }
    }
}
