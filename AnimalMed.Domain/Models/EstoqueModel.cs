using AnimalMed.Domain.Records;

namespace AnimalMed.Domain.Models
{
    public class EstoqueModel
    {
        public EstoqueModel() { }
        public EstoqueModel(EstoqueRecord record)
        {
            Id = record.Id;
            Nome = record.Nome;
            Descricao = record.Descricao;
            Quantidade = record.Quantidade;
            Preco = record.Preco;
        }

        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int? Quantidade { get; set; }
        public decimal? Preco { get; set; }

        public EstoqueRecord ToRecord()
        {
            return new EstoqueRecord
            {
                Id = this.Id,
                Nome = this.Nome,
                Descricao = this.Descricao,
                Quantidade = this.Quantidade,
                Preco = this.Preco
            };
        }
    }
}
