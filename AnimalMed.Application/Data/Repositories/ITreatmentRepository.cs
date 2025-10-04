using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalMed.Domain.Records;

namespace AnimalMed.Application.Data.Repositories
{
    public interface ITreatmentRepository
    {
        Task AddTreatment(TreatmentRecord record);
        Task<IEnumerable<TreatmentRecord>> GetAllTreatments();
        Task<TreatmentRecord> GetTreatmentById(int id);
        Task UpdateTreatment(TreatmentRecord record);
        Task DeleteTreatment(int id);
    }
}
