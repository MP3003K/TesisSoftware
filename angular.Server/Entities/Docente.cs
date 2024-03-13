using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Docente: Entity
    {
        public int PersonaId { get; private set; }


        // Relaciones con otras tablas
        public virtual Persona? Persona { get; private set; }
        public virtual IList<Aula>? Aulas { get; private set; }
        
        
        // Funciones
        public Docente(int personaId)
        {
            PersonaId = personaId;
        }
        public void UpdatePersonaId(int personaId)
        {
            PersonaId = personaId;
        }
        public void Update(int personaId)
        {
            UpdatePersonaId(personaId);
        }
    }
}
