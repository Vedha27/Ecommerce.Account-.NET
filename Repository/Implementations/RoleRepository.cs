using Dapper;
using Entities.Domain_Models;
using Repository.ConnectionFactory;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Repository.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public RoleRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> AssignRoleAsync(int userId, string role)
        {
            using var connection = _connectionFactory.CreateConnection();

          
            var roleFromDb = await GetRoleByNameAsync(role);

            var sql = @"
                INSERT INTO ""UserRole"" (""UserId"", ""RoleId"")
                VALUES (@UserId, @RoleId)
                ON CONFLICT (""UserId"", ""RoleId"") DO NOTHING;";

            var rowsAffected = await connection.ExecuteAsync(sql, new { UserId = userId, RoleId = roleFromDb.RoleId });


            return rowsAffected > 0;
        }


        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            using var connection = _connectionFactory.CreateConnection();

            var sql = @"SELECT ""RoleId"", ""RoleName"" FROM ""Role"" WHERE ""RoleName"" = @RoleName LIMIT 1;";

            var role = await connection.QuerySingleOrDefaultAsync<Role>(sql, new { RoleName = roleName });

            if (role == null)
                throw new KeyNotFoundException($"Role '{roleName}' not found.");

            return role;
        }

        Task IRoleRepository.AssignRoleAsync(int userId, string role)
        {
            return AssignRoleAsync(userId, role);
        }
    }
}
