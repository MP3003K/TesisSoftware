namespace DTOs
{
    public class RespuestasPsicologicasAulaDto
    {
        public IList<RespuestasPsicologicasEstudianteDto>? RespuestasEstudiantesDto { get; set; }
        public IList<EscalaPsicologicaDto>? RespuestaAulaDto { get; set; }
    }
}
