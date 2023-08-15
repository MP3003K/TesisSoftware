using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Persona : Entity
    {
        public string Nombres { get; private set; }
        public string ApellidoPaterno { get; private set; }
        public string ApellidoMaterno { get; private set; }
        public int DNI { get; private set; }

        // Relaciones con otras tablas
        public virtual Docente? Docente { get; private set; }
        public virtual Usuario? Usuario { get; private set; }
        public virtual Estudiante? Estudiante { get; private set; }
        
        
        // Funciones
        public Persona(string nombres, string apellidoPaterno, string apellidoMaterno, int dNI)
        {
            Nombres = nombres;
            ApellidoPaterno = apellidoPaterno;
            ApellidoMaterno = apellidoMaterno;
            DNI = dNI;
        }
        public void UpdateNombres(string nombres)
        {
            Nombres = nombres;
        }
        public void UpdateApellidoPaterno(string apellidoPaterno)
        {
            ApellidoPaterno = apellidoPaterno;
        }
        public void UpdateApellidoMaterno(string apellidoMaterno)
        {
            ApellidoMaterno = apellidoMaterno;
        }
        public void UpdateDNI(int dNI)
        {
            DNI = dNI;
        }
        public void Update(string nombres, string apellidoPaterno, string apellidoMaterno, int dNI)
        {
            UpdateNombres(nombres);
            UpdateApellidoPaterno(apellidoPaterno);
            UpdateApellidoMaterno(apellidoMaterno);
            UpdateDNI(dNI);
        }

    }
}
