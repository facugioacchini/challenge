namespace challenge.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Email { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string? Telefono { get; set; }

        public string Pais { get; set; }

        public bool Info { get; set; } = false;
    }
}