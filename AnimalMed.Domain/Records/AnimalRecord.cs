namespace AnimalMed.Domain.Records
{
    public record AnimalRecord
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Race { get; set; }
        public double? Weight { get; set; }
        public string SeverityStatus { get; set; }

    }
}
