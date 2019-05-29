using Models.Reports;
using System;

namespace Web.Models.Reports
{
    public class BalanceReportViewModel
    {
        public DateTimeOffset? From { get; set; }
    
        public DateTimeOffset? To { get; set; }

        public BalanceReport BalanceReport { get; set; }
    }
}
