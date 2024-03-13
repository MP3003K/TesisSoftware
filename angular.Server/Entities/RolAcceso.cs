using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RolAcceso: Entity
    {
        public int RolId { get; private set; }
        public int AccesoId { get; private set; }
        
        // Relaciones con otras tablas
        public virtual Rol? Rol { get; private set; }
        public virtual Acceso? Acceso { get; private set; }

        public RolAcceso(int rolId, int accesoId)
        {
            RolId = rolId;
            AccesoId = accesoId;
        }
        public void UpdateRolId(int rolId)
        {
            RolId = rolId;
        }
        public void UpdateAccesoId(int accesoId)
        {
            AccesoId = accesoId;
        }
        public void Update(int rolId, int accesoId)
        {
            UpdateRolId(rolId);
            UpdateAccesoId(accesoId);
        }
    }
}
