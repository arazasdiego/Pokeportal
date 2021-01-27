using Dapper;
using Pokeshop.Entities.Data;
using Pokeshop.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Pokeshop.Services.Dappers
{
    public class OrderItemProvider : IOrderItemProvider
    {
        public OrderItemProvider(IContext context)
        {
            ConnectionString = context.ConnectionString;
        }

        public string ConnectionString { get; }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(string orderCode)
        {
            try
            {
                string sql = @"SELECT o.Price, o.Quantity, p.ProductId, p.Name
                               FROM OrderItems AS o
                               INNER JOIN Products AS p ON o.ProductId=p.ProductId
                               WHERE o.OrderCode=@OrderCode
                               ORDER BY p.Name;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.QueryAsync<OrderItem, Product, OrderItem>(sql, (orderItem, product) =>
                    {
                        orderItem.Product = product;
                        return orderItem;
                    },
                    splitOn: "ProductId",
                    param: new { OrderCode = orderCode });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddOrderItemAsync(IEnumerable<OrderItem> orderItems)
        {
            try
            {
                string sql = @"INSERT INTO OrderItems(ProductId, Quantity, Price, OrderCode)
                               VALUES(@ProductId, @Quantity, @Price, @OrderCode);";

                foreach (var item in orderItems)
                {
                    using (var connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        await connection.ExecuteAsync(sql, item);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteAsync(string orderCode)
        {
            try
            {
                string sql = @"DELETE FROM OrderItems WHERE OrderCode=@OrderCode;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteAsync(sql, new { OrderCode = orderCode });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
