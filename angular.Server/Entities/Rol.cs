using Entities.Base;

namespace Entities
{
    public class Rol: Entity
    {
        public string Nombre { get; private set; }
        public string Estado { get; private set; }
        public virtual IList<RolAcceso>? RolesAccesos { get; private set; }
        public virtual IList<RolUsuario>? RolesUsuarios { get; private set; }

        public Rol(string nombre, string estado)
        {
            Nombre = nombre;
            Estado = estado;
        }
        public void UpdateNombre(string nombre)
        {
            Nombre = nombre;
        }
        public void UpdateEstado(string estado)
        {
            Estado = estado;
        }
        public void Update(string nombre, string estado)
        {
            UpdateNombre(nombre);
            UpdateEstado(estado);
        }
    }
}
