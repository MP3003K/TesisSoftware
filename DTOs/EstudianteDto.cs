﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class EstudianteDto
    {
        public int Id { get; set; }
        public string CodigoEstudiante { get; set; } = string.Empty;
        public int PersonaId { get; set; }
        public int? EvaPsiEstId { get; set; }
        public AulaDto? AulaDto { get; set; }
        public UnidadDto? UnidadDto { get; set; }
    }
}
