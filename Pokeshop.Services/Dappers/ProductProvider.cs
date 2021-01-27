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
    public class ProductProvider : IProductProvider
    {
        public ProductProvider(IContext context)
        {
            ConnectionString = context.ConnectionString;
        }

        private readonly string ConnectionString;

        public async Task<int> AddAsync(Product newProduct)
        {
            try
            {
                newProduct.Created = DateTime.Now;
                newProduct.Stock = 0;
                string sql = @"INSERT INTO Products(Name, Description, Stock, Price, CategoryId, Created)
                               VALUES(@Name, @Description, @Stock, @Price, @CategoryId, @Created);";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteAsync(sql, newProduct);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> AddStockAsync(int id, int quantityAdded)
        {
            try
            {
                string sql = @"UPDATE Products SET Stock=Stock+@QuantityAdded
                               WHERE ProductId=@ProductId;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteAsync(sql, new { ProductId = id, QuantityAdded = quantityAdded });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> CountAsync()
        {
            try
            {
                string sql = @"SELECT COUNT(*) FROM Products;";

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

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                string sql = @"DELETE FROM Products WHERE ProductId=@ProductId;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteAsync(sql, new { ProductId = id });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            try
            {
                string sql = @"SELECT p.ProductId, p.Name, p.Description, p.Price, p.Stock,
                               c.CategoryId, c.Name
                               FROM Products AS p
                               INNER JOIN Categories AS C ON p.CategoryId=c.CategoryId
                               ORDER BY p.Name;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.QueryAsync<Product, Category, Product>(sql, (product, category) =>
                    {
                        product.Category = category;
                        return product;
                    },
                    splitOn: "CategoryId");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            try
            {
                string sql = @"SELECT p.ProductId, p.Name, p.Description, p.Price, p.Stock,
                               c.CategoryId, c.Name
                               FROM Products AS p
                               INNER JOIN Categories AS C ON p.CategoryId=c.CategoryId
                               WHERE p.Productid=@ProductId;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    var productFromDb =  await connection.QueryAsync<Product, Category, Product>(sql, (product, category) =>
                    {
                        product.Category = category;
                        return product;
                    },
                    splitOn: "CategoryId",
                    param: new { ProductId = id });

                    return productFromDb.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Product>> GetByPageAsync(int pageNumber, int pageSize)
        {
            try
            {
                var offset = (pageNumber - 1) * pageSize;
                string sql = @"SELECT p.ProductId, p.Name, p.Description, p.Price, p.Stock,
                               c.CategoryId, c.Name
                               FROM Products AS p
                               INNER JOIN Categories AS C ON p.CategoryId=c.CategoryId
                               ORDER BY p.Name
                               OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.QueryAsync<Product, Category, Product>(sql, (product, category) =>
                    {
                        product.Category = category;
                        return product;
                    },
                    splitOn: "CategoryId",
                    param: new { Offset = offset, PageSize = pageSize });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpdateAsync(Product modifiedProduct)
        {
            try
            {
                string sql = @"UPDATE Products SET Name=@Name, Description=@Description, Price=@Price, CategoryId=@CategoryId WHERE ProductId=@ProductId;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteAsync(sql, modifiedProduct);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<decimal> GetPriceAsync(int id)
        {
            try
            {
                string sql = @"SELECT Price FROM Products WHERE ProductId=@ProductId;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteScalarAsync<decimal>(sql, new { ProductId = id });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
