using AnimalMed.Domain.Records;

namespace AnimalMed.Application.Data.Repositories
{
    public interface IAnimalRepository
    {
        Task AddAnimal(AnimalRecord record);
        Task<IEnumerable<AnimalRecord>> GetAllAnimals();
        Task<AnimalRecord> GetAnimalById(int id);
        Task UpdateAnimal(AnimalRecord record);
        Task DeleteAnimal(int id);

    }
}
