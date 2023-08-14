﻿using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Acceso: Entity
    {
         public string Nombre { get; private set; }
        public string Estado { get;private set; }

        // Relaciones con otras tablas
        public virtual  IList<RolAcceso>? RolesAccesos { get; private set; }

        // Funciones

        public Acceso(string nombre, string estado)
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
