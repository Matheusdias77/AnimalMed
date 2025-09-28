using AnimalMed.Application.DbFactury;
using AnimalMed.Domain.Records;
using Npgsql;
using System.Diagnostics;
using Dapper;

namespace AnimalMed.Application.Data.Repositories.Implementations
{
    public class EstoqueRepository : IEstoqueRepository
    {
        private readonly string _connectionString;

        public EstoqueRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> SaveEstoque(EstoqueRecord record)
        {
            var stopwatch = Stopwatch.StartNew();

            var query = $@"insert into {DbNames.Estoque} 
                            (""Nome"", ""Descricao"", ""Quantidade"", ""Preco"")
                           values 
                            (@Nome, @Descricao, @Quantidade, @Preco)";

            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            await connection.ExecuteAsync(query, record);

            stopwatch.Stop();

            return true;
        }
    }
}
