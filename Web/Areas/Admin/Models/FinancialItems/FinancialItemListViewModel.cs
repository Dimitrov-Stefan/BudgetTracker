using Models;
using Models.Entities;

namespace Web.Areas.Admin.Models.FinancialItems
{
    public class FinancialItemListViewModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public PagedList<FinancialItem> FinancialItems { get; set; }
    }
}
