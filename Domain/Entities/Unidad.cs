﻿using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Unidad : Entity
    {
        public string Nombre { get; private set; }
        public int NUnidad { get; private set; }
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFin { get; private set; }
        public int EscuelaId { get; private set; }


        // Relaciones con otras tablas
        public virtual Escuela? Escuela { get; private set; }
        public virtual IList<EvaluacionAula>? EvaluacionesAula { get; private set; }

        //Funciones
        public Unidad(string nombre, int nUnidad, DateTime fechaInicio, DateTime fechaFin, int escuelaId)
        {
            Nombre = nombre;
            NUnidad = nUnidad;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            EscuelaId = escuelaId;
        }
        public void UpdateNombre(string nombre)
        {
            Nombre = nombre;
        }
        public void UpdateNUnidad(int nUnidad)
        {
            NUnidad = nUnidad;
        }
        public void UpdateFechaInicio(DateTime fechaInicio)
        {
            FechaInicio = fechaInicio;
        }
        public void UpdateFechaFin(DateTime fechaFin)
        {
            FechaFin = fechaFin;
        }
        public void UpdateEscuelaId(int escuelaId)
        {
            EscuelaId = escuelaId;
        }
        public void Update(string nombre, int nUnidad, DateTime fechaInicio, DateTime fechaFin, int escuelaId)
        {
            UpdateNombre(nombre);
            UpdateNUnidad(nUnidad);
            UpdateFechaInicio(fechaInicio);
            UpdateFechaFin(fechaFin);
            UpdateEscuelaId(escuelaId);
        }
    }
}
