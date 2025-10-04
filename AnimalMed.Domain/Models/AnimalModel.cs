using AnimalMed.Domain.Records;

namespace AnimalMed.Domain.Models
{
    public class AnimalModel
    {
        public AnimalModel() { }

        public AnimalModel(AnimalRecord record)
        {
            Id = record.Id;
            Nome = record.Nome;
            Especie = record.Especie;
            Raca = record.Raca;
            Sexo = record.Sexo;
            DataNascimento = record.DataNascimento;
            Peso = record.Peso;
            Castrado = record.Castrado;
            Observacoes = record.Observacoes;
            CpfDono = record.CpfDono;
        }

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

        public AnimalRecord ToRecord()
        {
            return new AnimalRecord
            {
                Id = this.Id,
                Nome = this.Nome,
                Especie = this.Especie,
                Raca = this.Raca,
                Sexo = this.Sexo,
                DataNascimento = this.DataNascimento,
                Peso = this.Peso,
                Castrado = this.Castrado,
                Observacoes = this.Observacoes,
                CpfDono = this.CpfDono
            };
        }
    }
}
