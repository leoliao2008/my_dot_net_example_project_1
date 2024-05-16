using Microsoft.Data.SqlClient;
using MinimalApiTutorial.IService;
using System.Data;

namespace MinimalApiTutorial.Service
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private string _connectionString;

        public DbConnectionFactory(IWebHostEnvironment env, IConfiguration config) {
            if (env.IsDevelopment())
            {
                _connectionString = config.GetConnectionString("Develope")!;
            }
            else {
                _connectionString = config.GetConnectionString("Production")!;
            }
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
