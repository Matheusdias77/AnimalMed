using AnimalMed.Domain.Records;

namespace AnimalMed.Domain.Models
{
    public class StockModel
    {
        public StockModel() { }
        public StockModel(StockRecord record)
        {
            Id = record.Id;
            Product = record.Product;
            Location = record.Location;
            Quantity = record.Quantity;
            Validity = record.Validity;

        }

        public int? Id { get; set; }
        public ProductRecord Product { get; set; }
        public string Location { get; set; }
        public int? Quantity { get; set; }
        public Boolean Available { get; set; } 
        public DateTime Validity { get; set; }


        public StockRecord ToRecord()
        {
            return new StockRecord
            {
                Id = this.Id,
                Product = this.Product,
                Location = this.Location,
                Quantity = this.Quantity,
                Validity = this.Validity
            };
        }
    }
}
