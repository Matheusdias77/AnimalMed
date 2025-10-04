using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalMed.Domain.Records
{
    public record TreatmentRecord
    {
        public int Id { get; set; }
        public AnimalRecord Animal { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public UserRecord Veterinarian { get; set; }
        public string Medicine { get; set; }

    }
}
