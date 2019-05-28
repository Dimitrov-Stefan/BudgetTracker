using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Reports
{
    public class BalanceReport
    {
        public IEnumerable<FinancialItemReportRow> FinancialItemReportRow { get; set; }
    }
}
