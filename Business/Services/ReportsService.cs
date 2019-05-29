using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Models.Entities;
using Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ReportsService : IReportsService
    {
        private readonly IFinancialItemsRepository _financialItemsRepository;
        private readonly IFinancialOperationsRepository _financialOperationsRepository;

        public ReportsService(IFinancialItemsRepository financialItemsRepository, IFinancialOperationsRepository financialOperationsRepository)
        {
            _financialItemsRepository = financialItemsRepository;
            _financialOperationsRepository = financialOperationsRepository;
        }

        public async Task<BalanceReport> GetBalanceAsync(IEnumerable<FinancialItem> financialItems = null, DateTimeOffset? from = null, DateTimeOffset? to = null)
        {
            var financialOperations = await _financialOperationsRepository
                .GetByMultuipleFinancialItemIdsAndDateRangeAsync(financialItems?
                .Select(fi => fi.Id)
                .ToList(), from, to);

            var rows = financialOperations.GroupBy(fo => fo.FinancialItemId).Select(fo => new FinancialItemReportRow()
            {
                Balance = Convert.ToDecimal(fo.Sum(fo2 => (fo2.FinancialItem.Type == Models.Enums.FinancialItemType.Expense) ? -fo2.Amount : fo2.Amount)),
                FinancialItem = fo.First().FinancialItem
            });

            var balanceReport = new BalanceReport();
            balanceReport.FinancialItemReportRows = rows;
            balanceReport.Total = rows.Sum(r => r.Balance);

            return balanceReport;
        }
    }
}
