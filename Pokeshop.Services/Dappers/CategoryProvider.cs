using Dapper;
using Pokeshop.Entities.Data;
using Pokeshop.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Pokeshop.Services.Dappers
{
    public class CategoryProvider : ICategoryProvider
    {
        public CategoryProvider(IContext context)
        {
            ConnectionString = context.ConnectionString;
        }

        public string ConnectionString { get; }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            try
            {
                string sql = @"SELECT CategoryId, Name, Created, Modified 
                               FROM Categories 
                               ORDER BY Name;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.QueryAsync<Category>(sql);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Category>> GetByPageAsync(int pageNumber, int pageSize)
        {
            try
            {
                var offset = (pageNumber - 1) * pageSize;
                string sql = @"SELECT CategoryId, Name, Created, Modified 
                               FROM Categories 
                               ORDER BY Name
                               OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.QueryAsync<Category>(sql, new { Offset = offset, PageSize = pageSize });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            try
            {
                string sql = @"SELECT CategoryId, Name, Created, Modified 
                               FROM Categories 
                               WHERE CategoryId=@CategoryId;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.QueryFirstAsync<Category>(sql, new { CategoryId = id });
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
                string sql = @"SELECT COUNT(*) FROM Categories;";

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

        public async Task<int> AddAsync(Category newCategory)
        {
            try
            {
                newCategory.Created = DateTime.Now;
                string sql = @"INSERT INTO Categories(Name, Created)VALUES(@Name, @Created);";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteAsync(sql, newCategory);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpdateAsync(Category modifiedCategory)
        {
            try
            {
                modifiedCategory.Modified = DateTime.Now;
                string sql = @"UPDATE Categories SET Name=@Name, Modified=@Modified WHERE CategoryId=@CategoryId;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteAsync(sql, modifiedCategory);
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
                string sql = @"DELETE FROM Categories WHERE CategoryId=@CategoryId;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteAsync(sql, new { CategoryId = id });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
