using AnimalMed.Domain.Records;
using AnimalMed.Application.DbFactury;
using Dapper;
using Npgsql;
using AnimalMed.Application.DbFactury;

namespace AnimalMed.Application.Data.Repositories.Implementations
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly string _connectionString;
        public AnimalRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddAnimal(AnimalRecord record)
        {
            try
            {
                const string query = $@"
                                    INSERT INTO {DbNames.Animal} (""Name"", ""Race"", ""Weight"", ""SeverityStatus"")
                                    VALUES (@Name, @Race, @Weight, @SeverityStatus)
                                    RETURNING ""Id"";";

                await using var connection = new Npgsql.NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                await connection.ExecuteScalarAsync<int>(query, record);
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Erro no banco: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ao adicionar animal: {ex.Message}");
                throw;

            }
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
