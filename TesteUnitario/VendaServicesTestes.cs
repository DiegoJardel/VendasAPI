using Moq;
using VendasAPI.Modelos;
using VendasAPI.Repositorios;
using VendasAPI.Servicos;
using Xunit;

namespace VendasAPI.TesteUnitario
{
    public class VendaServiceTests
    {
        [Fact]
        public void RegistrarVenda_DeveAdicionarVenda()
        {

            var vendaRepositorioMock = new Mock<IVendaRepositorio>();
            var vendaService = new VendaServico(vendaRepositorioMock.Object);

            var venda = new Venda
            {
                Vendedor = new Vendedor { Id = 1, Cpf = "12345678900", Nome = "Teste", Email = "teste@teste.com", Telefone = "123456789" },
                Itens = new List<Item> { new Item { Id = 1, Nome = "Item 1", Preco = 10, Quantidade = 1 } }
            };

            vendaService.RegistrarVenda(venda);
            vendaRepositorioMock.Verify(v => v.Adicionar(It.Is<Venda>(v => v.Status == "Aguardando pagamento" && v.Data != default)), Times.Once);
        }

        [Fact]
        public void AtualizarStatus_DeveAtualizarStatusCorretamente()
        {
            var vendaRepositoryMock = new Mock<IVendaRepositorio>();
            var vendaService = new VendaServico(vendaRepositoryMock.Object);

            var venda = new Venda
            {
                Id = 1,
                Status = "Aguardando pagamento" 
            };

            vendaRepositoryMock.Setup(r => r.Obter(1)).Returns(venda);

            vendaService.AtualizarStatus(1, "Pagamento aprovado");

            Assert.Equal("Pagamento aprovado", venda.Status);
            vendaRepositoryMock.Verify(r => r.Atualizar(venda), Times.Once);
        }

        [Fact]
        public void AtualizarStatus_DeveLancarExcecaoParaTransicaoInvalida()
        {
            var vendaRepositoryMock = new Mock<IVendaRepositorio>();
            var vendaService = new VendaServico(vendaRepositoryMock.Object);

            var venda = new Venda
            {
                Id = 1,
                Status = "Aguardando pagamento" 
            };

            vendaRepositoryMock.Setup(r => r.Obter(1)).Returns(venda);

            var exception = Assert.Throws<Exception>(() => vendaService.AtualizarStatus(1, "Entregue"));
            Assert.Equal("Transição de status inválida", exception.Message);

            Assert.Equal("Aguardando pagamento", venda.Status);
            vendaRepositoryMock.Verify(r => r.Atualizar(It.IsAny<Venda>()), Times.Never); 
        }
    }

}

