﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class AulaDto
    {
        public int Id { get; set; }
        public string Secccion { get; set; } = string.Empty;
        public int GradoId { get; set; }
        public int EscuelaId { get; set; }
        public string? GradoNombre { get; set; } = string.Empty;
    }
}
