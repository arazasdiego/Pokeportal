using Dapper;
using Pokeshop.Entities.Data;
using Pokeshop.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Pokeshop.Services.Dappers
{
    public class IdentityProvider : IIdentityProvider
    {
        public IdentityProvider(IContext context)
        {
            ConnectionString = context.ConnectionString;
        }

        public string ConnectionString { get; }

        public async Task<ApplicationUser> LoginAsync(string email, string password)
        {
            try
            {
                string sql = @"SELECT UserId, FullName, Email, Created, Modified FROM ApplicationUsers
                               WHERE Email=@Email AND Password=@Password;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.QueryFirstOrDefaultAsync<ApplicationUser>(sql, new { Email = email, Password = password });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> RegisterAsync(ApplicationUser newUser)
        {
            try
            {
                newUser.Created = DateTime.Now;
                string sql = @"INSERT INTO ApplicationUsers(UserId, FullName, Email, Password, Created)
                               VALUES(@UserId, @FullName, @Email, @Password, @Created);";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteAsync(sql, newUser);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ApplicationUser>> GetByPageAsync(int pageNumber, int pageSize)
        {
            try
            {
                var offset = (pageNumber - 1) * pageSize;
                string sql = @"SELECT UserId, FullName, Email, Created, Modified FROM ApplicationUsers
                               ORDER BY FullName
                               OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.QueryAsync<ApplicationUser>(sql, new { Offset = offset, PageSize = pageSize });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApplicationUser> GetByIdAsync(string userId)
        {
            try
            {
                string sql = @"SELECT UserId, FullName, Email, Created, Modified FROM ApplicationUsers
                               WHERE UserId=@UserId;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.QueryFirstAsync<ApplicationUser>(sql, new { UserId = userId });
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
                string sql = @"SELECT COUNT(*) FROM ApplicationUsers;";

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

        public async Task<int> UpdateAsync(ApplicationUser modifiedUser)
        {
            try
            {
                modifiedUser.Modified = DateTime.Now;
                string sql = @"UPDATE ApplicationUser SET Fullname=@FullName, Email=@Email, Modified=@Modified
                               WHERE UserId=@UserId;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteAsync(sql, modifiedUser);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteAsync(string userId)
        {
            try
            {
                string sql = @"DELETE FROM ApplicationUsers WHERE UserId=@UserId;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteAsync(sql, new { UserId = userId });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
