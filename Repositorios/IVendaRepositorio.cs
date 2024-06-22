using VendasAPI.Modelos;

namespace VendasAPI.Repositorios
{
    public interface IVendaRepositorio
    {
        void Adicionar(Venda venda);
        Venda Obter(int id);
        void Atualizar(Venda venda);
        Venda BuscarVenda(int v);
    }
}
