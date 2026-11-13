using Microsoft.AspNetCore.Mvc;
using Personero.API.Data;
using System.Threading.Tasks;

namespace Personero.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiscalizacionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FiscalizacionController(AppDbContext context)
        {
            _context = context;
        }

        // Endpoint para guardar una fase completa (Antes, Durante, Después)
        [HttpPost("guardar-fase")]
        public async Task<IActionResult> GuardarFase([FromBody] List<ObservacionDTO> observaciones)
        {
            if (observaciones == null || !observaciones.Any())
                return BadRequest("No se enviaron datos.");

            foreach (var obs in observaciones)
            {
                var nuevaObservacion = new ObservacionFiscalizacion
                {
                    Fase = obs.Fase,
                    ItemId = obs.ItemId,
                    TituloItem = obs.TituloItem,
                    Estado = obs.Estado,
                    TextoObservacion = obs.Observacion,
                    FotoUrl = obs.FotoUrl, // Aquí entrará la URL de Cloudinary luego
                    MesaId = obs.MesaId,
                    PersoneroId = obs.PersoneroId,
                    FechaRegistro = DateTime.UtcNow
                };

                _context.Observaciones.Add(nuevaObservacion);
            }

            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Fase registrada correctamente en la base de datos." });
        }
    }

    // Estructura de datos temporal para recibir la petición de React
    public class ObservacionDTO
    {
        public string Fase { get; set; } = string.Empty;
        public string ItemId { get; set; } = string.Empty;
        public string TituloItem { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string? Observacion { get; set; }
        public string? FotoUrl { get; set; }
        public int MesaId { get; set; }
        public int PersoneroId { get; set; }
    }
}