using Pokeshop.Entities.Entities;
using System.Threading.Tasks;

namespace Pokeshop.Services.Dappers
{
    public interface IInvoiceProvider
    {
        Task<int> PayAsync(string orderCode, decimal amountPaid);
        Task<Invoice> GetAsync(string orderCode);
    }
}
