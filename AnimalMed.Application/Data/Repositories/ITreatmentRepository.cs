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
        Task<bool>AddTreatment(TreatmentRecord record);
        Task<IEnumerable<TreatmentRecord>> GetAllTreatments();
        Task<TreatmentRecord> GetTreatmentById(int id);
        Task<bool> UpdateTreatment(TreatmentRecord record);
        Task<bool> DeleteTreatment(int id);
    }
}
