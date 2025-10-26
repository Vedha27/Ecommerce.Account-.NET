using System.Threading.Tasks;
using Dapper;
using Repository.ConnectionFactory;
using System.Data;
using Entities.Domain_Models;
using Repository.Interfaces;

namespace Repository.Implementations
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public CustomerRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = @"INSERT INTO ""Customer"" (""UserId"") VALUES (@UserId)";
            await connection.ExecuteAsync(sql, customer);
        }
    }
}
