namespace DTOs
{
    public class EvaluacionPsicologicaEstudianteDto
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FehcaFin { get; set; } 
        public string Estado { get; set; } = string.Empty;
        public int EstudianteId { get; set; }
        public int EvaluacionAulaId { get; set; }
    }
}
