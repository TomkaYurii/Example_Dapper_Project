using System.Data;
using System.Data.Common;
using System.Data.SqlClient;


namespace Dapper_Example_Project.Connection
{
    public class ConnectionFactory : IConnectionFactory
    {
        public IDbConnection Connect
        {
            get
            {
                DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
                var DbFactory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                var Connection = DbFactory.CreateConnection();
                Connection.ConnectionString = GetConnectionString();
                Connection.Open();
                return Connection;
            }
        }

        public string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfigurationRoot Configuration = builder.Build();
            var connectionString = Configuration.GetSection("ConnectionStrings:MSSQLConnection");

            return connectionString.Value;
        }


        public void Dispose()
        {

        }

    }
}
