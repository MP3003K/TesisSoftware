using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Indicador: Entity
    {
        public string NombreIndicador { get; private set; }
        public string Pregunta { get; private set; }
        public int Puntaje { get; private set; }
        public int EscalaId { get; private set; }
        public virtual Escala? Escala { get; private set; }
        public int EvaEstudianteId { get; private set; }
        public virtual EvaluacionEstudiante? EvaluacionEstudiante { get; private set; }

        public Indicador(string nombreIndicador, string pregunta, int puntaje, int escalaId)
        {
            NombreIndicador = nombreIndicador;
            Pregunta = pregunta;
            Puntaje = puntaje;
            EscalaId = escalaId;
        }
        public void UpdateNombreIndicador(string nombreIndicador)
        {
            NombreIndicador = nombreIndicador;
        }
        public void UpdatePregunta(string pregunta)
        {
            Pregunta = pregunta;
        }
        public void UpdatePuntaje(int puntaje)
        {
            Puntaje = puntaje;
        }
        public void UpdateEscalaId(int escalaId)
        {
            EscalaId = escalaId;
        }
        public void Update(string nombreIndicador, string pregunta, int puntaje, int escalaId)
        {
            UpdateNombreIndicador(nombreIndicador);
            UpdatePregunta(pregunta);
            UpdatePuntaje(puntaje);
            UpdateEscalaId(escalaId);
        }
    }
}
