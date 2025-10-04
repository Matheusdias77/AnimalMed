using AnimalMed.Application.DbFactury;
using AnimalMed.Domain.Records;
using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace AnimalMed.Application.Data.Repositories.Implementations
{
    public class AnimalRepository : IAnimalRepository
    {
        #region.: Properties :.
        private readonly string _connectionString;
        private readonly ILogger<AnimalRepository> _logger;
        #endregion

        #region .: Contructor:.
        public AnimalRepository(IConfiguration configuration, ILogger<AnimalRepository> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }
        #endregion

        public async Task<bool>SaveAnimal(AnimalRecord record)
        {
            var query = $@"
                        INSERT INTO {DbNames.Animal} (""nome"", ""especie"", ""raca"", ""sexo"", ""datanascimento"", ""peso"", ""castrado"", ""observacoes"", ""cpfdono"")
                        VALUES (@nome, @especie, @raca, @sexo, @dataNascimento, @peso, @castrado, @observacoes, @cpfdono)";

            await using var connection = new Npgsql.NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            try
            {
                await connection.ExecuteAsync(query, record);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar animal no banco de dados.");
            }

            return true;
        }
        public async Task<IEnumerable<AnimalRecord>> GetAllAnimals()
        {
            try
            {
                const string query = $@"SELECT * FROM {DbNames.Animal}";
                await using var connection = new Npgsql.NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                var animals = await connection.QueryAsync<AnimalRecord>(query);
                return animals.ToList();
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Erro no banco: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ao obter animais: {ex.Message}");
                throw;
            }
        }
        public async Task<AnimalRecord> GetAnimalById(int id)
        {
            try
            {
                const string query = $@"SELECT * FROM {DbNames.Animal} WHERE ""Id"" = @Id";
                await using var connection = new Npgsql.NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                var animal = await connection.QuerySingleOrDefaultAsync<AnimalRecord>(query, new { Id = id });
                return animal;
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Erro no banco: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ao obter animal por ID: {ex.Message}");
                throw;
            }
        }
        public async Task UpdateAnimal(AnimalRecord record)
        {
            try
            {
                const string query = $@"
                                    UPDATE {DbNames.Animal}
                                    SET 
                                        ""Name"" = @Name, ""Race"" = @Race, ""Weight"" = @Weight,
                                        ""SeverityStatus"" = @SeverityStatus
                                    WHERE ""Id"" = @Id;";

                await using var connection = new Npgsql.NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                var affectedRows = await connection.ExecuteAsync(query, record);

                if (affectedRows == 0)
                {
                    Console.WriteLine($"Nenhum animal encontrado com Id = {record.Id}");
                }
                else
                {
                    Console.WriteLine($"Animal com Id = {record.Id} atualizado com sucesso!");
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Erro no banco: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar animal: {ex.Message}");
                throw;
            }
        }
        public async Task DeleteAnimal(int id)
        {
            try
            {
                const string query = $@"DELETE FROM {DbNames.Animal} WHERE ""Id"" = @Id;";
                await using var connection = new Npgsql.NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
                if (affectedRows == 0)
                {
                    Console.WriteLine($"Nenhum animal encontrado com Id = {id}");
                }
                else
                {
                    Console.WriteLine($"Animal com Id = {id} deletado com sucesso!");
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Erro no banco: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao deletar animal: {ex.Message}");
                throw;
            }
        }
    }
}
