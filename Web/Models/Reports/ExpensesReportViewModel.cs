using Models.Reports;
using System;

namespace Web.Models.Reports
{
    public class ExpensesReportViewModel
    {
        public DateTimeOffset? From { get; set; }
    
        public DateTimeOffset? To { get; set; }

        public ExpensesReport ExpensesReport { get; set; }
    }
}
