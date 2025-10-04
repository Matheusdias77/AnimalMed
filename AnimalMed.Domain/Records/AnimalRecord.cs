namespace AnimalMed.Domain.Records
{
    public record AnimalRecord
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Especie { get; set; }
        public string Raca { get; set; }
        public string Sexo { get; set; }
        public DateTime? DataNascimento { get; set; }
        public double? Peso { get; set; }
        public bool Castrado { get; set; }
        public string? Observacoes { get; set; }
        public string CpfDono { get; set; }
    }
}
