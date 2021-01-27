using Dapper;
using Pokeshop.Entities.Data;
using Pokeshop.Entities.Entities;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Pokeshop.Services.Dappers
{
    public class InvoiceProvider : IInvoiceProvider
    {
        public InvoiceProvider(IContext context)
        {
            ConnectionString = context.ConnectionString;
        }

        public string ConnectionString { get; }

        public async Task<int> PayAsync(string orderCode, decimal amountPaid)
        {
            try
            {
                var datePaid = DateTime.Now;
                string sql = @"INSERT INTO Invoices(OrderCode, AmountPaid, DatePaid)VALUES(@OrderCode, @AmountPaid, @DatePaid);";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteAsync(sql, new { OrderCode = orderCode, AmountPaid = amountPaid, DatePaid = datePaid });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Invoice> GetAsync(string orderCode)
        {
            try
            {
                string sql = @"SELECT * FROM Invoices WHERE OrderCode=@OrderCode;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return await connection.QueryFirstAsync<Invoice>(sql, new { OrderCode = orderCode });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
