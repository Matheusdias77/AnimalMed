using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalMed.Domain.Records;
using Dapper;
using AnimalMed.Application.DbFactury;

namespace AnimalMed.Application.Data.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task AddUser(UserRecord record)
        {
            try
            {
                const string query = @" INSERT INTO ""User"" (""Name"", ""Email"", ""Password"", ""Type"")
                                        VALUES (@Name, @Email, @Password, @Type)
                                        RETURNING ""Id"";";
                await using var connection = new Npgsql.NpgsqlConnection(_connectionString);
                await connection.OpenAsync();
                await connection.ExecuteScalarAsync<int>(query, record);

            } catch(Npgsql.NpgsqlException ex)
            {
                Console.WriteLine($"Erro no banco: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ao adicionar usuário: {ex.Message}");
                throw;
            }
        }
        public async Task<IEnumerable<UserRecord>> GetAllUsers()
        {
            try
            {
                const string query = $@"SELECT * FROM {DbNames.User}";

                await using var connection = new Npgsql.NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                var users = await connection.QueryAsync<UserRecord>(query);
                return users;

            }
            catch (Npgsql.NpgsqlException ex)
            {
                Console.WriteLine($"Erro no banco: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ao obter usuários: {ex.Message}");
                throw;
            }
        }
        public async Task<UserRecord> GetUserById(int id)
        {
            try
            {
                const string query = $@"SELECT * FROM {DbNames.User} WHERE ""Id"" = @Id";
                await using var connection = new Npgsql.NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                var user = await connection.QuerySingleOrDefaultAsync<UserRecord>(query);
                return user; 
            }
            catch (Npgsql.NpgsqlException ex)
            {
                Console.WriteLine($"Erro no banco: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ao obter usuário por ID: {ex.Message}");
                throw;
            }
        }
        public async Task<UserRecord> GetUserByEmail(string email)
        {
            try
            {
                const string query = $@"SELECT * FROM {DbNames.User} WHERE ""Email"" = @Email";
                await using var connection = new Npgsql.NpgsqlConnection(_connectionString);
                await connection.OpenAsync();
                var user = await connection.QuerySingleOrDefaultAsync<UserRecord>(query, new { Email = email });
                return user;
            }
            catch (Npgsql.NpgsqlException ex)
            {
                Console.WriteLine($"Erro no banco: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ao obter usuário por email: {ex.Message}");
                throw;
            }
        }
        public async Task<bool> UpdateUser(UserRecord record)
        {
            try
            {
                const string query = $@"UPDATE {DbNames.User} 
                                    SET ""Name"" = @Name,
                                        ""Email"" = @Email,
                                        ""Password"" = @Password,
                                        ""Type"" = @Type
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
                return true;
            }
            catch (Npgsql.NpgsqlException ex)
            {
                Console.WriteLine($"Erro no banco: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ao atualizar usuário: {ex.Message}");
                throw;
            }
        }
        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                const string query = $@"DELETE FROM {DbNames.User} WHERE ""Id"" = @Id;";
                await using var connection = new Npgsql.NpgsqlConnection(_connectionString);
                await connection.OpenAsync();
                var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
                if (affectedRows == 0)
                {
                    Console.WriteLine($"Nenhum usuário encontrado com Id = {id}");
                }
                else
                {
                    Console.WriteLine($"Usuário com Id = {id} deletado com sucesso!");
                }
                return true;
            }
            catch (Npgsql.NpgsqlException ex)
            {
                Console.WriteLine($"Erro no banco: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ao deletar usuário: {ex.Message}");
                throw;
            }
        }
    }
}
