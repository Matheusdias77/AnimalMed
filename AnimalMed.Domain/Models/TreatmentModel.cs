using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using AnimalMed.Domain.Records;

namespace AnimalMed.Domain.Models
{
    public class TreatmentModel
    {
        public TreatmentModel() { }

        public TreatmentModel(TreatmentRecord record)
        {
            Id = record.Id;
            Animal = record.Animal;
            Description = record.Description;
            Date = record.Date;
            Veterinarian = record.Veterinarian;
            Medicine = record.Medicine;
        }
        public int Id { get; set; }
        public AnimalRecord Animal { get; set; }
        public string Description { get; set; }
        public string Medicine { get; set; }
        public DateTime Date { get; set; }
        public UserRecord Veterinarian { get; set; }

        public TreatmentRecord ToRecord()
        {
            return new TreatmentRecord
            {
                Id = this.Id,
                Animal = this.Animal,
                Description = this.Description,
                Date = this.Date,
                Veterinarian = this.Veterinarian,
                Medicine = this.Medicine,
            };
        }
    }
}
