using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalMed.Domain.Records;

namespace AnimalMed.Application.Data.Repositories
{
   public interface IUserRepository
    {
        Task AddUser(UserRecord record);
        Task<IEnumerable<UserRecord>> GetAllUsers();
        Task<UserRecord> GetUserById(int id);
        Task<UserRecord> GetUserByEmail(string email);
        Task UpdateUser(UserRecord record);
        Task DeleteUser(int id);
    }
}
