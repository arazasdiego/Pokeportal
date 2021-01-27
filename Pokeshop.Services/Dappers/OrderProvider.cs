using Dapper;
using Pokeshop.Entities.Data;
using Pokeshop.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Pokeshop.Services.Dappers
{
    public class OrderProvider : IOrderProvider
    {
        public OrderProvider(IContext context)
        {
            ConnectionString = context.ConnectionString;
        }

        public string ConnectionString { get; }        

        public async Task<IEnumerable<Order>> GetAllAsync(int pageNumber, int pageSize)
        {
            try
            {
                var offset = (pageNumber - 1) * pageSize;
                string sql = @"SELECT * FROM Orders
                               ORDER BY DateOrdered
                               OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.QueryAsync<Order>(sql, new { Offset = offset, PageSize = pageSize });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Order>> GetAllAsync(int pageNumber, int pageSize, OrderStatus orderStatus)
        {
            try
            {
                var offset = (pageNumber - 1) * pageSize;
                string sql = @"SELECT * FROM Orders
                               WHERE OrderStatus=@OrderStatus
                               ORDER BY DateOrdered
                               OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.QueryAsync<Order>(sql, new { Offset = offset, PageSize = pageSize, OrderStatus = orderStatus });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Order>> GetAllByUserAsync(int pageNumber, int pageSize, string userId)
        {
            try
            {
                var offset = (pageNumber - 1) * pageSize;
                string sql = @"SELECT * FROM Orders
                               WHERE UserId=@UserId
                               ORDER BY DateOrdered
                               OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.QueryAsync<Order>(sql, new { Offset = offset, PageSize = pageSize, UserId = userId });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Order>> GetAllByUserAsync(int pageNumber, int pageSize, string userId, OrderStatus orderStatus)
        {
            try
            {
                var offset = (pageNumber - 1) * pageSize;
                string sql = @"SELECT * FROM Orders
                               WHERE UserId=@UserId AND OrderStats=@OrderStatus
                               ORDER BY DateOrdered
                               OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.QueryAsync<Order>(sql, new 
                    { 
                        Offset = offset, 
                        PageSize = pageSize, 
                        UserId = userId,
                        OrderStatus = orderStatus
                    });
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
                string sql = @"DELETE FROM Orders WHERE OrderCode=@OrderCode;";
                string sql2 = @"DELETE FROM OrderItems WHERE OrderCode=@OrderCode;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    await connection.ExecuteAsync(sql, new { OrderCode = orderCode });
                    return await connection.ExecuteAsync(sql2, new { OrderCode = orderCode });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Order> GetByOrderCodeAsync(string orderCode)
        {
            try
            {
                string sql = @"SELECT o.OrderCode, o.TotalAmount, o.OrderStatus, o.DateOrdered,
                               u.UserId, u.FullName, u.Email
                               FROM Orders AS o
                               INNER JOIN ApplicationUsers AS u ON o.UserId=u.UserId
                               WHERE OrderCode=@OrderCode;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    var orderFromDb =  await connection.QueryAsync<Order, ApplicationUser, Order>(sql, (order, user) => 
                    {
                        order.User = user;
                        return order;
                    },
                    splitOn: "UserId",
                    param: new { OrderCode = orderCode });

                    return orderFromDb.First();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<Order> GetByUserAsync(string userId, string orderCode)
        {
            try
            {
                string sql = @"SELECT o.OrderCode, o.TotalAmount, o.OrderStatus, o.DateOrdered,
                               u.UserId, u.FullName, u.Email
                               FROM Orders AS o
                               INNER JOIN ApplicationUsers AS u ON o.UserId=u.UserId
                               WHERE OrderCode=@OrderCode AND UserId=@UserId;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    var orderFromDb = await connection.QueryAsync<Order, ApplicationUser, Order>(sql, (order, user) =>
                    {
                        order.User = user;
                        return order;
                    },
                    splitOn: "UserId",
                    param: new { OrderCode = orderCode, UserId = userId });

                    return orderFromDb.First();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> CountAllOrdersAsync()
        {
            try
            {
                string sql = @"SELECT COUNT(*) FROM Orders;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteScalarAsync<int>(sql);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> CountAllOrdersAsync(OrderStatus orderStatus)
        {
            try
            {
                string sql = @"SELECT COUNT(*) FROM Orders WHERE OrderStatus=@OrderStatus;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteScalarAsync<int>(sql, new { OrderStatus = orderStatus });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> CountByUserAsync(string userId)
        {
            try
            {
                string sql = @"SELECT COUNT(*) FROM Orders WHERE UserId=@UserId;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteScalarAsync<int>(sql, new { UserId = userId });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> CountByUserAsync(string userId, OrderStatus orderStatus)
        {
            try
            {
                string sql = @"SELECT COUNT(*) FROM Orders WHERE UserId=@UserId AND OrderStatus=@OrderStatus;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteScalarAsync<int>(sql, new { UserId = userId, OrderStatus = orderStatus });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> PlaceOrderAsync(Order newOrder)
        {
            try
            {
                newOrder.DateOrdered = DateTime.Now;
                string sql = @"INSERT INTO Orders(OrderCode, UserId, TotalAmount, OrderStatus, DateOrdered)
                               VALUES(@OrderCode, @UserId, @TotalAmount, @OrderStatus, @DateOrdered);";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteAsync(sql, newOrder);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> CountUserOrderAsync(string userId)
        {
            try
            {
                string sql = @"SELECT COUNT(*) FROM Orders WHERE UserId=@UserId;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteScalarAsync<int>(sql, new { UserId = userId });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<decimal> GetTotalPayableAsync(string userId, string orderCode)
        {
            try
            {
                string sql = @"SELECT TotalAmount FROM Orders WHERE UserId=@UserId AND OrderCode=@OrderCode;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteScalarAsync<decimal>(sql, new { UserId = userId, OrderCode = orderCode });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpdateStatusAsync(OrderStatus orderStatus, string orderCode)
        {
            try
            {
                var modified = DateTime.Now;
                string sql = @"UPDATE Orders SET OrderStatus=@OrderStatus, Modified=@Modified 
                               WHERE OrderCode=@OrderCode;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteAsync(sql, new 
                    { 
                        OrderStatus = orderStatus, 
                        OrderCode = orderCode, 
                        Modified = modified 
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> OrderExistsAsync(string orderCode)
        {
            try
            {
                string sql = @"SELECT COUNT(*) FROM Orders WHERE OrderCode=@OrderCode;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteScalarAsync<bool>(sql, new { OrderCode = orderCode });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
