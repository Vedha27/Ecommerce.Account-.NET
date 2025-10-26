using Dapper;
using Entities.Domain_Models;
using Repository.ConnectionFactory;
using Repository.Interfaces;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public UserRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var sql = @"
                INSERT INTO ""User"" 
                    (""FullName"", ""Email"", ""PhoneNumber"", ""PasswordHash"", ""IsActive"")
                VALUES 
                    (@FullName, @Email, @PhoneNumber, @PasswordHash, @IsActive)
                RETURNING 
                    ""UserId"", ""FullName"", ""Email"", ""PhoneNumber"", ""PasswordHash"", 
                    ""IsActive"", ""CreatedDate"", ""UpdatedDate"", ""UserGuid"";";

            using var connection = _connectionFactory.CreateConnection();

            var createdUser = await connection.QuerySingleAsync<User>(sql, user);

            return createdUser;
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            var sql = @"SELECT * FROM ""User"" WHERE ""Email"" = @Email AND ""PasswordHash"" = @PasswordHash;";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Email = email, PasswordHash = password });
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var sql = @"SELECT * FROM ""User"" WHERE ""Email"" = @Email;";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Email = email });
        }

    }
}
