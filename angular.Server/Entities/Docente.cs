using Entities.Base;

namespace Entities
{
    public class Docente: Entity
    {
        public int PersonaId { get; private set; }
        public virtual Persona? Persona { get; private set; }
        public virtual IList<Aula>? Aulas { get; private set; }

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
