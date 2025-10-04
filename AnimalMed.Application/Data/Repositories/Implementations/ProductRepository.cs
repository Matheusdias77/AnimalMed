using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalMed.Application.DbFactury;
using AnimalMed.Domain.Records;
using Dapper;

namespace AnimalMed.Application.Data.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;
        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddProduct(ProductRecord record)
        {
            try
            {
                var query = $@"INSERT INTO {DbNames.Product}
                           (""Name"", ""Description"", ""Manufacturer"", ""Type"")
                           values 
                           (@Name, @Description, @Manufacturer, @Type)";
            } catch (Npgsql.NpgsqlException ex)
            {
                Console.WriteLine($"Erro no banco: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ao adicionar produto: {ex.Message}");
                throw;
            }
        }
        public async Task<IEnumerable<ProductRecord>> GetAllProducts()
        {
            try
            {
                var query = $@"SELECT * FROM {DbNames.Product}";
                await using var connection = new Npgsql.NpgsqlConnection(_connectionString);
                await connection.OpenAsync();
                var result = await connection.QueryAsync<ProductRecord>(query);
                return result;
            } catch (Npgsql.NpgsqlException ex)
            {
                Console.WriteLine($"Erro no banco: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ao carregar os produtos: {ex.Message}");
                throw;
            }
        }
        public async Task<ProductRecord> GetProductById(int id)
        {
            try
            {
                const string query = $@"SELECT * FROM {DbNames.Product} WHERE ""Id"" = @Id";
                await using var connection = new Npgsql.NpgsqlConnection(_connectionString);
                await connection.OpenAsync();
                var result = await connection.QuerySingleOrDefaultAsync<ProductRecord>(query, new { Id = id });
                return result;
            }
            catch (Npgsql.NpgsqlException ex)
            {
                Console.WriteLine($"Erro no banco: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ao carregar o produto: {ex.Message}");
                throw;
            }
        }
        public async Task UpdateProduct(ProductRecord record)
        {
            try
            {
                const string query = $@"UPDATE {DbNames.Product}
                                     SET ""Name"" = @Name,
                                         ""Description"" = @Description, 
                                         ""Manufacturer"" = @Manufacturer,
                                         ""Type"" = @Type 
                                     WHERE ""Id"" = @Id";

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
            catch (Npgsql.NpgsqlException ex)
            {
                Console.WriteLine($"Erro no banco: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ao atualizar o produto: {ex.Message}");
                throw;
            }
        }
        public async Task DeleteProduct(int id)
        {
            try
            {
                const string query = $@"DELETE FROM {DbNames.Product} WHERE ""Id"" = @Id";
                await using var connection = new Npgsql.NpgsqlConnection(_connectionString);
                await connection.OpenAsync();
                var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
                if (affectedRows == 0)
                {
                    Console.WriteLine($"Nenhum produto encontrado com Id = {id}");
                }
                else
                {
                    Console.WriteLine($"Produto com Id = {id} deletado com sucesso!");
                }
            }
            catch (Npgsql.NpgsqlException ex)
            {
                Console.WriteLine($"Erro no banco: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ao deletar o produto: {ex.Message}");
                throw;
            }
        }
    }
}
