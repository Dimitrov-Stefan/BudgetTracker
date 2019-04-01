using System.Threading.Tasks;
using Models.Entities;

namespace Core.Contracts.Services
{
    public interface IFinancialItemsService
    {
        Task CreateAsync(FinancialItem item);
    }
}
