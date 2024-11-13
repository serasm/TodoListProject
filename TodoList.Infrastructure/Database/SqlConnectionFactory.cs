using System.Data;
using Npgsql;

namespace TodoList.Infrastructure.Database;

public interface ISqlConnectionFactory
{
    IDbConnection GetOpenConnection();
}

public class SqlConnectionFactory : ISqlConnectionFactory, IDisposable
{
    private string _connectionString;
    private IDbConnection _connection;

    public SqlConnectionFactory(string connectionString) => _connectionString = connectionString;
    
    public IDbConnection GetOpenConnection()
    {
        if (this._connection == null || this._connection.State != ConnectionState.Open)
        {
            this._connection = new NpgsqlConnection(this._connectionString);
            this._connection.Open();
        }

        return this._connection;
    }

    public void Dispose()
    {
        if (this._connection != null && this._connection.State != ConnectionState.Open)
        {
            this._connection.Dispose();
        }
    }
}