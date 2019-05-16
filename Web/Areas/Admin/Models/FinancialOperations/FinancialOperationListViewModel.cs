using System.Collections.Generic;
using Models.Entities;

namespace Web.Areas.Admin.Models.FinancialOperations
{
    public class FinancialOperationListViewModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public IEnumerable<FinancialOperation> FinancialOperations { get; set; }
    }
}
