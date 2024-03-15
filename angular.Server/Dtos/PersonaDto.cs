namespace DTOs
{
    public class PersonaDto
    {
        public int Id { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string ApellidoPaterno { get; set; } = string.Empty;
        public string ApellidoMaterno { get; set; } = string.Empty;
        public int DNI { get; set; }
    }
}
