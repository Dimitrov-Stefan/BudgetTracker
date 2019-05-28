using Models.Entities;
using Models.Reports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Contracts.Services
{
    public interface IReportsService
    {
        Task<BalanceReport> GetBalanceAsync(IEnumerable<FinancialItem> financialItems);
    }
}
