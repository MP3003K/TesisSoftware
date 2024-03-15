namespace DTOs
{
    public class EstudianteAulaDto
    {
        public int Id { get; set; }
        public int EstudianteId { get; set; }
        public int AulaId { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}
