using Npgsql;
using System.Data;

namespace AnimalMed.Util.Data
{
    public class DataBase
    {
        private readonly string _connectionString;

        public DataBase(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable ExecuteQuery(string sql)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            using var command = new NpgsqlCommand(sql, connection);
            using var adapter = new NpgsqlDataAdapter(command);
            var dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public void ExecuteNonQuery(string sql)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            using var command = new NpgsqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }
    }
}
