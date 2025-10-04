using AnimalMed.Domain.Records;

namespace AnimalMed.Application.Data.Repositories
{
   public interface IUserRepository
    {
        Task AddUser(UserRecord record);
        Task<IEnumerable<UserRecord>> GetAllUsers();
        Task<UserRecord> GetUserById(int id);
        Task<UserRecord> GetUserByEmail(string email);
        Task<bool> UpdateUser(UserRecord record);
        Task<bool> DeleteUser(int id);
    }
}
