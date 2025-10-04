using System;
using System.Collections.Generic;
using AnimalMed.Domain.Records;

namespace AnimalMed.Domain.Models
{
    public class AnimalModel
    {
        public AnimalModel() { }

        public AnimalModel(AnimalRecord record)
        {
            Id = record.Id;
            Name = record.Name;
            Race = record.Race;
            Weight = record.Weight;
            SeverityStatus = record.SeverityStatus;
        }
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Race { get; set; }
        public double? Weight { get; set; }
        public string SeverityStatus { get; set; }
        public List<TreatmentRecord> Treatments { get; set; } = new List<TreatmentRecord>();

        public AnimalRecord ToRecord()
        {
            return new AnimalRecord
            {
                Id = this.Id,
                Name = this.Name,
                Race = this.Race,
                Weight = this.Weight,
                SeverityStatus = this.SeverityStatus,
            };
        }
    }
}
