
namespace AnimalMed.Domain.Records
{
    public record EstoqueRecord
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int? Quantidade { get; set; }
        public decimal? Preco { get; set; }
    }
}
