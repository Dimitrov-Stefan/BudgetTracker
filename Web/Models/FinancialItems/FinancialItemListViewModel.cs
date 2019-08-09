using System.Collections.Generic;
using Models;
using Models.Entities;

namespace Web.Models.FinancialItems
{
    public class FinancialItemListViewModel
    {
        public PagedList<FinancialItem> FinancialItems { get; set; }
    }
}
