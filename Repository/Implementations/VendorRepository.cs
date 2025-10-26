using System.Threading.Tasks;
using Dapper;
using Repository.ConnectionFactory;
using System.Data;
using Entities.Domain_Models;
using Repository.Interfaces;

namespace Repository.Implementations
{
    public class VendorRepository: IVendorRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public VendorRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task CreateVendorAsync(Vendor vendor)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = @"INSERT INTO ""Vendor"" (""UserId"") VALUES (@UserId)";
            await connection.ExecuteAsync(sql, vendor);
        }
    }
}
