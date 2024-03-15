using Entities.Base;

namespace Entities
{
    public class RolUsuario: Entity
    {
        public int UsuarioId { get; private set; }
        public int RolId { get; private set; }
        public virtual Usuario? Usuario { get; private set; }
        public virtual Rol? Rol { get; private set; }

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
