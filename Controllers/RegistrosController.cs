using Microsoft.AspNetCore.Mvc;
using Personero.API.Data;
using Personero.API.Models;

namespace Personero.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RegistrosController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/registros/sync
        [HttpPost("sync")]
        public async Task<IActionResult> SyncRegistros([FromBody] List<RegistroPersonero> registrosNuevos)
        {
            if (registrosNuevos == null || registrosNuevos.Count == 0)
                return BadRequest("No hay datos para sincronizar.");

            try
            {
                // Agregamos todos los registros recibidos a la base de datos
                await _context.Registros.AddRangeAsync(registrosNuevos);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"{registrosNuevos.Count} registros sincronizados correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno al sincronizar: {ex.Message}");
            }
        }
    }
}