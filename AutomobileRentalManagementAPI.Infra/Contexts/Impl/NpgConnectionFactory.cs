using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace AutomobileRentalManagementAPI.Infra.Contexts.Impl
{
    public class NpgConnectionFactory : IDbConnectionFactory
    {
        private readonly string? _config;

        public NpgConnectionFactory(IConfiguration configuration)
        {
            _config = configuration.GetConnectionString("PostgresConnection");
        }

        public IDbConnection GetNewConnection() => new NpgsqlConnection(_config);
    }
}