using System.Data;
using Npgsql;

namespace TodoList.Infrastructure.Database;

public class SqlConnectionFactory : IDisposable
{
    private readonly string _connectionString;
    private IDbConnection _connection;

    public SqlConnectionFactory(string connectionString) => _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));

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
        if (this._connection != null || this._connection.State != ConnectionState.Closed)
        {
            this._connection?.Close();
            this._connection?.Dispose();
        }
    }
}