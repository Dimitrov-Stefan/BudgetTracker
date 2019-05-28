using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Models.Entities;
using Models.Reports;
using System;
using System.Collections.Generic;
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

        public Task<BalanceReport> GetBalanceAsync(IEnumerable<FinancialItem> financialItems = null)
        {
            var balanceReport = new BalanceReport();
            

            throw new NotImplementedException();
        }
    }
}
