using System.Threading.Tasks;
using Dapper;
using Repository.ConnectionFactory;
using System.Data;
using Entities.Domain_Models;
using Repository.Interfaces;

namespace Repository.Implementations
{
    public class AdminRepository:IAdminRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public AdminRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task CreateAdminAsync(Admin admin)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = @"INSERT INTO ""Admin"" (""UserId"") VALUES (@UserId)";
            await connection.ExecuteAsync(sql, admin);
        }
    }
}
