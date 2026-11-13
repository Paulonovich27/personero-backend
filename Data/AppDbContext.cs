using Microsoft.EntityFrameworkCore;
using Personero.API.Models;

namespace Personero.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<RegistroPersonero> Registros { get; set; }
    }
}