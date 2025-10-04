namespace AnimalMed.Domain.Records
{
    public record StockRecord
    {
        public int? Id { get; set; }
        public ProductRecord Product { get; set; }
        public string Location { get; set; }
        public int? Quantity { get; set; }
        public Boolean Available { get; set; }
        public DateTime Validity { get; set; }

    }
}
