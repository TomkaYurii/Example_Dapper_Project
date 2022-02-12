using System.Data;


namespace Dapper_Example_Project.Connection
{
    public interface IConnectionFactory : IDisposable
    {
        IDbConnection Connect { get; }

        String GetConnectionString();

    }
}
