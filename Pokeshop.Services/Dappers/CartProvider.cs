using Dapper;
using Pokeshop.Entities.Data;
using Pokeshop.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Pokeshop.Services.Dappers
{
    public class CartProvider : ICartProvider
    {
        public CartProvider(IContext context)
        {
            ConnectionString = context.ConnectionString;
        }

        public string ConnectionString { get; }

        public async Task<int> AddAsync(Cart newItem)
        {
            try
            {
                newItem.Created = DateTime.Now;
                string sql = @"INSERT INTO Carts(ProductId, Quantity, Price, UserId, Created)
                               VALUES(@ProductId, @Quantity, @Price, @UserId, @Created);";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteAsync(sql, newItem);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteAsync(IEnumerable<int> productIds, string userId)
        {
            try
            {
                string sql = @"DELETE FROM Carts WHERE ProductId=@ProductId AND UserId=@UserId;";
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    foreach (var productId in productIds)
                    {
                       await connection.ExecuteAsync(sql, new { ProductId = productId, UserId = userId });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Cart>> GetAsync(string userId)
        {
            try
            {
                string sql = @"SELECT c.Id, c.Quantity, c.Price, p.ProductId, p.Name
                               FROM Carts AS c
                               INNER JOIN Products AS p ON c.ProductId=p.ProductId  
                               WHERE c.UserId=@UserId
                               ORDER BY p.Name;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.QueryAsync<Cart, Product, Cart>(sql, (cart, product) =>
                    {
                        cart.Product = product;
                        return cart;
                    },
                    splitOn: "ProductId",
                    param: new { UserId = userId });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpdateAsync(Cart modifiedItem)
        {
            try
            {
                string sql = @"UPDATE Carts SET Quantity=Quantity+@Quantity, Price=Price+@Price
                               WHERE ProductId=@ProductId AND UserId=@UserId;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteAsync(sql, modifiedItem);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ExistsAsync(int productId, string userId)
        {
            try
            {
                string sql = @"SELECT * FROM Carts WHERE ProductId=@ProductId AND UserId=@UserId;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteScalarAsync<bool>(sql, new { ProductId = productId, UserId = userId });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> HasUserCartAsync(string userId)
        {
            try
            {
                string sql = @"SELECT * FROM Carts WHERE UserId=@UserId;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteScalarAsync<bool>(sql, new { UserId = userId });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<decimal> GetTotalAmountAsync(string userId)
        {
            try
            {
                string sql = @"SELECT SUM(Price) FROM Carts WHERE UserId=@UserId;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteScalarAsync<decimal>(sql, new { UserId = userId });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
