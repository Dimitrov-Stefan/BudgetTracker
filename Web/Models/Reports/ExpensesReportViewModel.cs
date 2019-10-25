using Models.Reports;
using System;
using System.Collections.Generic;

namespace Web.Models.Reports
{
    public class ExpensesReportViewModel
    {
        public DateTimeOffset? From { get; set; }

        public DateTimeOffset? To { get; set; }

        public IList<FinancialItemSelectViewModel> SelectedItems { get; set; }

        public ExpensesReport ExpensesReport { get; set; }
    }
}
