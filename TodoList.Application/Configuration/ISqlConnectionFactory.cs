using System.Data;

namespace TodoList.Application.Configuration;

public interface ISqlConnectionFactory
{
    IDbConnection GetOpenConnection();
}