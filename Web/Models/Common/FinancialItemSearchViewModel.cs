using System.Collections.Generic;

namespace Web.Models.Common
{
    public class FinancialItemSearchViewModel
    {
        public IEnumerable<FinancialItemSimplifiedModel> FinancialItems { get; set; }
    }
}
