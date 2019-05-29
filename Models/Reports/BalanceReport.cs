using System.Collections.Generic;

namespace Models.Reports
{
    public class BalanceReport
    {
        public IEnumerable<FinancialItemReportRow> FinancialItemReportRows { get; set; }

        public decimal Total { get; set; }
    }
}
