namespace VendasAPI.Modelos
{
    public class Venda
    {
        public int Id { get; set; }
        public Vendedor? Vendedor { get; set; }
        public DateTime Data { get; set; }
        public List<Item>? Itens { get; set; }
        public string? Status { get; set; }
    }
}
