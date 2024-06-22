using System.Globalization;
using VendasAPI.Modelos;
using VendasAPI.Repositorios;

namespace VendasAPI.Servicos
{
    public class VendaServico
    {
        private readonly IVendaRepositorio _vendaRepository;

        public VendaServico(IVendaRepositorio vendaRepository)
        {
            _vendaRepository = vendaRepository;
        }

        public void RegistrarVenda(Venda venda)
        {
            venda.Status = "Aguardando pagamento";
            venda.Data = DateTime.Now;
            _vendaRepository.Adicionar(venda);
        }

        public Venda BuscarVenda(int id)
        {
            return _vendaRepository.Obter(id);
        }

        public void AtualizarStatus(int id, string status)
        {
            var venda = _vendaRepository.Obter(id);
            if (venda == null)
                throw new Exception("Venda não encontrada");

            var transicoesValidas = new Dictionary<string, List<string>>
    {
        { "Aguardando pagamento", new List<string> { "Pagamento aprovado", "Cancelada" } },
        { "Pagamento aprovado", new List<string> { "Enviado para transportadora", "Cancelada" } },
        { "Enviado para transportadora", new List<string> { "Entregue" } }
    };

            status = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(status.ToLower());

            if (transicoesValidas.ContainsKey(venda.Status) && transicoesValidas[venda.Status].Contains(status))
            {
                venda.Status = status;
                _vendaRepository.Atualizar(venda);
            }
            else
            {
                throw new Exception("Transição de status inválida");
            }
        }

    }
}
