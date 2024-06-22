using VendasAPI.Modelos;

namespace VendasAPI.Repositorios
{
    public class InMemoryVendaRepository : IVendaRepositorio
    {
        private readonly List<Venda> _vendas = new();

        public void Adicionar(Venda venda)
        {
            venda.Id = _vendas.Count + 1;
            _vendas.Add(venda);
        }

        public Venda Obter(int id)
        {
            return _vendas.SingleOrDefault(v => v.Id == id);
        }

        public void Atualizar(Venda venda)
        {
            var vendaExistente = Obter(venda.Id);
            if (vendaExistente != null)
            {
                _vendas[_vendas.IndexOf(vendaExistente)] = venda;
            }
        }
        public Venda BuscarVenda(int id)
        {
            return _vendas.FirstOrDefault(v => v.Id == id);
        }
    }
}
