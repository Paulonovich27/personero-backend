using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Personero.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<ObservacionFiscalizacion> Observaciones { get; set; }
    }

    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Dni { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Rol { get; set; } = "Personero"; // Puede ser "Admin"
        public string PasswordHash { get; set; } = string.Empty; // Para el Login

        // Datos de Registro
        public string Telefono { get; set; } = string.Empty;
        public string Provincia { get; set; } = string.Empty;
        public string Distrito { get; set; } = string.Empty;
        public string LocalVotacion { get; set; } = string.Empty;

        // Relación: Un personero tiene una sola mesa asignada
        public int? MesaId { get; set; }
        [ForeignKey("MesaId")]
        public Mesa? MesaAsignada { get; set; }
    }

    public class Mesa
    {
        [Key]
        public int Id { get; set; }
        public string NumeroMesa { get; set; } = string.Empty;
        public string Local { get; set; } = string.Empty;

        // Relación: Una mesa puede tener muchas observaciones durante el día
        public ICollection<ObservacionFiscalizacion> Observaciones { get; set; } = new List<ObservacionFiscalizacion>();
    }

    public class ObservacionFiscalizacion
    {
        [Key]
        public int Id { get; set; }
        public string Fase { get; set; } = string.Empty; // "Antes", "Durante", "Despues"
        public string ItemId { get; set; } = string.Empty; // Ej. "a1", "d2" (del código de React)
        public string TituloItem { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty; // "OK" o "NO-OK"
        public string? TextoObservacion { get; set; }
        public string? FotoUrl { get; set; } // URL que nos devolverá Cloudinary
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        // Llaves foráneas
        public int MesaId { get; set; }
        [ForeignKey("MesaId")]
        public Mesa Mesa { get; set; } = null!;

        public int PersoneroId { get; set; }
        [ForeignKey("PersoneroId")]
        public Usuario Personero { get; set; } = null!;
    }
}