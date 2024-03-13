﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class EscalaPsicologicaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int DimensionId { get; set; }
        public double? PromedioEscala { get; set; }
        public IList<IndicadorPsicologicoDto>? IndicadoresPsicologicos { get; set; }
    }
}
