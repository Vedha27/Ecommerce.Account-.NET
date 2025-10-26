using System.Threading.Tasks;
using Dapper;

using Repository.ConnectionFactory;
using System.Data;
using Entities.Domain_Models;
using Repository.Interfaces;

namespace Repository.Implementations
{
    public class DeliveryBoyRepository: IDeliveryBoyRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public DeliveryBoyRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task CreateDeliveryBoyAsync(DeliveryBoy deliveryBoy)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = @"INSERT INTO ""DeliveryBoy"" (""UserId"") VALUES (@UserId)";
            await connection.ExecuteAsync(sql, deliveryBoy);
        }
    }
}
