using AnimalMed.Domain.Records;

namespace AnimalMed.Application.Data.Repositories
{
    public interface IEstoqueRepository
    {
        Task<bool> SaveEstoque(EstoqueRecord record);
    }
}
