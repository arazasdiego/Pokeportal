using Pokeshop.Services.Dappers;

namespace Pokeshop.Services
{
    public interface IUnitOfWork
    {
        ICategoryProvider Categories { get; }
        IIdentityProvider Users { get; }
        IProductProvider Products { get; }
        ICartProvider Carts { get; }
        IOrderProvider Orders { get; }
        IOrderItemProvider OrderItems { get; }
        IInvoiceProvider Invoices { get; }
    }
}
