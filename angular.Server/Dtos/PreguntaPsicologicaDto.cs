﻿namespace DTOs
{
    public class PreguntaPsicologicaDto
    {
        public int Id { get; set; }
        public string Pregunta { get; set; } = string.Empty;
        public int IndicadorPsicologicoId { get; set; }
        public int NPregunta { get; set; }
        public IList<RespuestaPsicologicaDto>? RespuestasPsicologicas { get; set; } 
    }
}
