using MySql.Data.MySqlClient;
using System.Data;

namespace IngSw_Infraestructure.Data.Connection;

public class SqlConnection
{
    private readonly string _connectionString;

    public SqlConnection(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}
