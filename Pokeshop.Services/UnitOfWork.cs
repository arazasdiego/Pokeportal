using Pokeshop.Services.Dappers;

namespace Pokeshop.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            IProductProvider productProvider,
            ICategoryProvider categoryProvider,
            IIdentityProvider identityProvider,
            ICartProvider cartProvider,
            IOrderProvider orderProvider,
            IOrderItemProvider orderItemProvider,
            IInvoiceProvider invoiceProvider)
        {
            Products = productProvider;
            Categories = categoryProvider;
            Users = identityProvider;
            Carts = cartProvider;
            Orders = orderProvider;
            Invoices = invoiceProvider;
            OrderItems = orderItemProvider;
        }

        public IProductProvider Products { get; }
        public ICategoryProvider Categories { get; }
        public IIdentityProvider Users { get; }
        public ICartProvider Carts { get; }
        public IOrderProvider Orders { get; }
        public IInvoiceProvider Invoices { get; }
        public IOrderItemProvider OrderItems { get; }
    }
}
