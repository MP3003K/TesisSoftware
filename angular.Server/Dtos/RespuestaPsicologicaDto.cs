namespace DTOs
{
    public class RespuestaPsicologicaDto
    {
        public int Id { get; set; }
        public string Respuesta { get; set; } = string.Empty;
        public int PreguntaPsicologicaId { get; set; }
        public int EvaPsiEstId { get; set; }
    }
}
