using AnimalMed.Domain.Records;

namespace AnimalMed.Application.Data.Repositories
{
    public interface IStockRepository
    {
        Task<bool> SaveEstoque(StockRecord record);
    }
}
