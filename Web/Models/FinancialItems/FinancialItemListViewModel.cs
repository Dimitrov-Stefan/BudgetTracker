using System.Collections.Generic;
using Models.Entities;

namespace Web.Models.FinancialItems
{
    public class FinancialItemListViewModel
    {
        public IEnumerable<FinancialItem> FinancialItems { get; set; }
    }
}
