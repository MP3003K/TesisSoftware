using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Usuario : Entity
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public int PersonaId { get; private set; }

        
        // Relaciones con otras tablas
        public virtual Persona? Persona{get ; private set; }
        public virtual IList<RolUsuario>? RolesUsuarios { get; private set; }

        // Funciones
        public Usuario(string username, string password, int personaId)
        {
            Username = username;
            Password = password;
            PersonaId = personaId;
        }
        public void UpdateUsername(string username)
        {
            Username = username;
        }
        public void UpdatePassword(string password)
        {
            Password = password;
        }
        public void UpdatePersonaId(int personaId)
        {
            PersonaId = personaId;
        }
        public void Update(string username, string password, int personaId)
        {
            UpdateUsername(username);
            UpdatePassword(password);
            UpdatePersonaId(personaId);
        }
    }
}
