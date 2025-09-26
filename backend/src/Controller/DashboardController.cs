using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyApp.Service;

namespace MyApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        // Endpoint para visualizar o dashboard consolidado
        [HttpGet]
        public async Task<IActionResult> ObterDashboard([FromQuery] string status, [FromQuery] DateTime? dataInspecao)
        {
            var resultado = await _dashboardService.ObterDashboardAsync(status, dataInspecao);
            return Ok(resultado);
        }

        // Endpoint para exibir detalhes de uma correia específica
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterDetalhesCorreia(int id)
        {
            var correia = await _dashboardService.ObterDetalhesCorreiaAsync(id);
            if (correia == null)
            {
                return NotFound();
            }
            return Ok(correia);
        }
    }
}
