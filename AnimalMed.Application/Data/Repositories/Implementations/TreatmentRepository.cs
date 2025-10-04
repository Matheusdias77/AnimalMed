using AnimalMed.Domain.Records;
using Dapper;
using Npgsql;

namespace AnimalMed.Application.Data.Repositories.Implementations
{
    public class TreatmentRepository : ITreatmentRepository
    {
        private readonly string _connectionString;
        public TreatmentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddTreatment(TreatmentRecord record)
        {
            try
            {
                const string query = @"
                                    INSERT INTO ""Treatment"" (""AnimalId"", ""Description"", ""Date"", ""VeterinarianId"", ""Medicine"")
                                    VALUES (@AnimalId, @Description, @Date, @VeterinarianId, @Medicine)
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
                Console.WriteLine($"Error ao salvar tratamento: {ex.Message}");
                throw;
            }
        }
        public async Task<IEnumerable<TreatmentRecord>> GetAllTreatments()
        {
            try
            {
                const string query = @"SELECT * FROM ""Treatment"";";

                await using var connection = new Npgsql.NpgsqlConnection(_connectionString);
                await connection.OpenAsync();
                var treatments = await connection.QueryAsync<TreatmentRecord>(query);
                return treatments.ToList();
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Erro no banco: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ao carregar tratamentos: {ex.Message}");
                throw;
            }
        }
        public async Task<TreatmentRecord> GetTreatmentById(int id)
        {
            try
            {
                const string query = @"SELECT * FROM ""Treatment"" WHERE ""Id"" = @Id;";
                await using var connection = new Npgsql.NpgsqlConnection(_connectionString);
                await connection.OpenAsync();
                var treatment = await connection.QuerySingleOrDefaultAsync<TreatmentRecord>(query, new { Id = id });
                return treatment;

            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Erro no banco: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ao carregar tratamento: {ex.Message}");
                throw;
            }
        }
        public async Task UpdateTreatment(TreatmentRecord record)
        {
            try
            {
                const string query = @"
                                    UPDATE ""Treatment""
                                    SET ""AnimalId"" = @AnimalId,
                                        ""Description"" = @Description,
                                        ""Date"" = @Date,
                                        ""VeterinarianId"" = @VeterinarianId,
                                        ""Medicine"" = @Medicine
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error ao atualizar tratamento: {ex.Message}");
                throw;
            }
        }
        public async Task DeleteTreatment(int id)
        {
            try
            {
                const string query = @"DELETE FROM ""Treatment"" WHERE ""Id"" = @Id;";
                await using var connection = new Npgsql.NpgsqlConnection(_connectionString);
                await connection.OpenAsync();
                var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
                if (affectedRows == 0)
                {
                    Console.WriteLine($"Nenhum tratamento encontrado com Id = {id}");
                }
                else
                {
                    Console.WriteLine($"Tratamento com Id = {id} deletado com sucesso!");
                }
            } 
            catch (Exception ex)
            {
              Console.WriteLine($"Error ao deletar tratamento: {ex.Message}");
               throw;
            }
        }
    }
}