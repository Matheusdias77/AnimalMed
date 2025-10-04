using AnimalMed.Application.DbFactury;
using AnimalMed.Domain.Records;
using Npgsql;
using System.Diagnostics;
using Dapper;

namespace AnimalMed.Application.Data.Repositories.Implementations
{
    public class StockRepository : IStockRepository
    {
        private readonly string _connectionString;

        public StockRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> SaveEstoque(StockRecord record)
        {
            try
            {
                var stopwatch = Stopwatch.StartNew();

                var query = $@"insert into {DbNames.Stock} 
                            (""Product"", ""Quantity"", ""Location"", ""Validity"")
                           values 
                            (@Product, @Quantity, @Location, @Validity)";

                await using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                stopwatch.Stop();

                return true; 
            } catch (Exception ex)
            {
                Console.WriteLine($"Error ao salvar estoque: {ex.Message}");
                throw;
            }
        }
    }
}
