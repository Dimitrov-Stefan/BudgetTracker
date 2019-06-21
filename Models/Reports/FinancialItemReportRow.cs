using Models.Entities;

namespace Models.Reports
{
    public class FinancialItemReportRow
    {
        public FinancialItem FinancialItem { get; set; }

        public decimal Sum { get; set; }
    }
}