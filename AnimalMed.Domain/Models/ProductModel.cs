using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalMed.Domain.Records;

namespace AnimalMed.Domain.Models
{
    public class ProductModel
    {
        public ProductModel(){}

        public ProductModel(ProductRecord record)
        {
            Name = record.Name;
            Type = record.Type;
            Manufacturer = record.Manufacturer;
            Description = record.Description;
        }
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }

        public ProductRecord ToRecord()
        {
            return new ProductRecord
            {
                Id = this.Id,
                Name = this.Name,
                Type = this.Type,
                Manufacturer = this.Manufacturer,
                Description = this.Description
            };
        }
    }
}
