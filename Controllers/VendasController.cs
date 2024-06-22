using Microsoft.AspNetCore.Mvc;
using VendasAPI.Modelos;
using VendasAPI.Servicos;

namespace VendasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendasController : ControllerBase
    {
        private readonly VendaServico _vendaServico;

        public VendasController(VendaServico vendaService)
        {
            _vendaServico = vendaService;
        }

        [HttpPost]
        public IActionResult RegistrarVenda([FromBody] Venda venda)
        {
            if (venda.Itens == null || !venda.Itens.Any())
            {
                return BadRequest("A venda deve conter pelo menos um item");
            }

            _vendaServico.RegistrarVenda(venda);
            return Ok(venda);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarVenda(int id)
        {
            var venda = _vendaServico.BuscarVenda(id);
            if (venda == null)
            {
                return NotFound();
            }
            return Ok(venda);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarStatus(int id, [FromBody] string status)
        {
            try
            {
                _vendaServico.AtualizarStatus(id, status);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
