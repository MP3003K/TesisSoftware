using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class InformacionUsuarioDto
    {
        public PersonaDto Persona { get; set; } = new PersonaDto();
        public RolDto Rol { get; set; } = new RolDto();

    }
}
