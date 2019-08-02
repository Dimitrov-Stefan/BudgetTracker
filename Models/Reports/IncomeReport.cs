using System.Collections.Generic;

namespace Models.Reports
{
    public class IncomeReport
    {
        public IEnumerable<FinancialItemReportRow> FinancialItemReportRows { get; set; }

        public decimal Total { get; set; }
    }
}
