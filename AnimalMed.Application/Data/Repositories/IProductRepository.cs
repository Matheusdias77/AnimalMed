using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalMed.Domain.Records;

namespace AnimalMed.Application.Data.Repositories
{
    public interface IProductRepository
    {
        Task AddProduct(ProductRecord record); 
        Task UpdateProduct(ProductRecord record);
        Task DeleteProduct(int id);
        Task<ProductRecord> GetProductById(int id);
        Task<IEnumerable<ProductRecord>> GetAllProducts();
    }
}
