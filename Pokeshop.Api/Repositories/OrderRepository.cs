using AutoMapper;
using Pokeshop.Api.Common;
using Pokeshop.Api.Dtos.OrderDtos;
using Pokeshop.Entities.Entities;
using Pokeshop.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokeshop.Api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedList<OrderReadDto>> GetAllAsync(int pageNumber, int pageSize, OrderStatus orderStatus)
        {
            IEnumerable<Order> orders;

            if (orderStatus == OrderStatus.Pending)
                orders = await _unitOfWork.Orders.GetAllAsync(pageNumber, pageSize);
            else
                orders = await _unitOfWork.Orders.GetAllAsync(pageNumber, pageSize, orderStatus);

            var ordersCount = await _unitOfWork.Orders.CountAllOrdersAsync(orderStatus);
            var ordersDto = _mapper.Map<IEnumerable<OrderReadDto>>(orders);

            return PagedList<OrderReadDto>.ToPagedList(pageNumber, pageSize, ordersCount, ordersDto);
        }

        public async Task<PagedList<OrderReadDto>> GetAllByUserAsync(int pageNumber, int pageSize, string userId, OrderStatus orderStatus)
        {
            IEnumerable<Order> orders;

            if (orderStatus == OrderStatus.Pending)
                orders = await _unitOfWork.Orders.GetAllByUserAsync(pageNumber, pageSize, userId);
            else
                orders = await _unitOfWork.Orders.GetAllByUserAsync(pageNumber, pageSize, userId, orderStatus);
            
            var ordersCount = await _unitOfWork.Orders.CountByUserAsync(userId, orderStatus);
            var ordersDto = _mapper.Map<IEnumerable<OrderReadDto>>(orders);

            return PagedList<OrderReadDto>.ToPagedList(pageNumber, pageSize, ordersCount, ordersDto);
        }

        public async Task<OrderFullReadDto> GetByOrderCodeAsync(string orderCode)
        {
            var order = await _unitOfWork.Orders.GetByOrderCodeAsync(orderCode);
            var orderItems = await _unitOfWork.OrderItems.GetOrderItemsAsync(orderCode);
            order.OrderItems = orderItems.ToList();
            order.Invoice = await _unitOfWork.Invoices.GetAsync(orderCode);

            return _mapper.Map<OrderFullReadDto>(order);
        }

        public async Task<OrderFullReadDto> GetByUserAsync(string userId, string orderCode)
        {
            var order = await _unitOfWork.Orders.GetByUserAsync(userId, orderCode);
            var orderItems = await _unitOfWork.OrderItems.GetOrderItemsAsync(orderCode);
            order.OrderItems = orderItems.ToList();
            order.Invoice = await _unitOfWork.Invoices.GetAsync(orderCode);

            return _mapper.Map<OrderFullReadDto>(order);
        }

        public async Task<bool> PlaceOrderAsync(string userId)
        {

            if (await _unitOfWork.Carts.HasUserCartAsync(userId) == false) return false;

            var newOrder = new Order();
            newOrder.UserId = userId;
            newOrder.OrderCode = await GenerateOrderCode();
            newOrder.TotalAmount = await _unitOfWork.Carts.GetTotalAmountAsync(userId);
            newOrder.OrderStatus = OrderStatus.Pending;

            var cart = await _unitOfWork.Carts.GetAsync(userId);
            var newOrderItems = new List<OrderItem>();
            var productIds = new List<int>();

            foreach (var item in cart)
            {
                var orderItem = new OrderItem
                {
                    OrderCode = newOrder.OrderCode,
                    ProductId = item.Product.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price

                };
                newOrderItems.Add(orderItem);
                productIds.Add(orderItem.ProductId);
            }
            await _unitOfWork.Orders.PlaceOrderAsync(newOrder);
            await _unitOfWork.OrderItems.AddOrderItemAsync(newOrderItems);
            await _unitOfWork.Carts.DeleteAsync(productIds, userId);

            return true;
        }

        public async Task<bool> PayAsync(string orderCode, decimal amountPaid)
        {
            if (await _unitOfWork.Orders.OrderExistsAsync(orderCode) == false)
                return false;

            await _unitOfWork.Invoices.PayAsync(orderCode, amountPaid);
            await _unitOfWork.Orders.UpdateStatusAsync(OrderStatus.Paid, orderCode);
            return true;
        }

        public async Task<int> DeleteAsync(string orderCode)
        {
            await _unitOfWork.OrderItems.DeleteAsync(orderCode);
            return await _unitOfWork.Orders.DeleteAsync(orderCode);
        }

        public async Task<int> ChangeStatusAsync(string orderCode)
        {
            var status = OrderStatus.Completed;
            return await _unitOfWork.Orders.UpdateStatusAsync(status, orderCode);
        }

        private async Task<string> GenerateOrderCode()
        {
            var orderCount = await _unitOfWork.Orders.CountAllOrdersAsync();
            var sb = new StringBuilder();
            sb.Append("ORDER");
            sb.Append(orderCount + 1);

            return sb.ToString();
        }
    }
}
