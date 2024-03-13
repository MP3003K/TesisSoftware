using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RolUsuario: Entity
    {
        public int UsuarioId { get; private set; }
        public int RolId { get; private set; }
        
        // Relaciones con otras tablas
        public virtual Usuario? Usuario { get; private set; }
        public virtual Rol? Rol { get; private set; }

        // Funciones
        public RolUsuario(int usuarioId, int rolId)
        {
            UsuarioId = usuarioId;
            RolId = rolId;
        }
        public void UpdateUsuarioId(int usuarioId)
        {
            UsuarioId = usuarioId;
        }
        public void UpdateRolId(int rolId)
        {
            RolId = rolId;
        }
        public void Update(int usuarioId, int rolId)
        {
            UpdateUsuarioId(usuarioId);
            UpdateRolId(rolId);
        }
    }
}
