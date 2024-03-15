namespace DTOs
{
    public class EvaluacionPsicologicaAulaDto
    {
        public int Id { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Estado { get; set; } = string.Empty;
        public int UnidadId { get; set; }
        public int AulaId { get; set; }
    }
}
