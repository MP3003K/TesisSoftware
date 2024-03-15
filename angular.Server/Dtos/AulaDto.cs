namespace DTOs
{
    public class AulaDto
    {
        public int Id { get; set; }
        public string Secccion { get; set; } = string.Empty;
        public int GradoId { get; set; }
        public int EscuelaId { get; set; }
        public GradoDto? Grado { get; set; }
    }
}
