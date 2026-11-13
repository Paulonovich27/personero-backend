namespace Personero.API.Models
{
    public class RegistroPersonero
    {
        public int Id { get; set; }
        public string Dni { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Provincia { get; set; } = string.Empty;
        public string Distrito { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } // Fecha en la que se guardó en el celular
    }
}