using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CNSirindar.Models;
using Microsoft.AspNet.Identity;
using CNSirindar;
using CNSirindar.Repositories;
using SirindarApi.Models.Remotos;

namespace SirindarApi.Controllers
{
    public class AsistenciaController : ApiController
    {
        private SirindarDbContext db = new SirindarDbContext();
        private IRepository<Asistencia, int> asistenciaR;
        private IRepository<Deportista, int> deportistaR;
        private IRepository<Horario, int> horarioR;

        public AsistenciaController(
            IRepository<Asistencia, int> asistenciaR,
            IRepository<Deportista, int> deportistaR,
            IRepository<Horario, int> horarioR)
        {
            this.asistenciaR = asistenciaR;
            this.deportistaR = deportistaR;
            this.horarioR = horarioR;
        }

        // POST api/Asistencia
        [ResponseType(typeof(AsistenciaResultado))]
        public IHttpActionResult PostRegistrarAsistencia(Asistencia asistencia)
        {
            if (!ModelState.IsValid || asistencia == null)
            {
                return BadRequest(ModelState);
            }

            var dia = DateTime.Now.DayOfWeek;
            var deportista = db.Deportistas.First(d => d.DeportistaId == asistencia.DeportistaId);
            var esteDiaSi = false;

            switch (dia)
            {
                case DayOfWeek.Monday:
                    esteDiaSi = deportista.CantidadComidas.Lunes;
                    break;
                case DayOfWeek.Tuesday:
                    esteDiaSi = deportista.CantidadComidas.Martes;
                    break;
                case DayOfWeek.Wednesday:
                    esteDiaSi = deportista.CantidadComidas.Miercoles;
                    break;
                case DayOfWeek.Thursday:
                    esteDiaSi = deportista.CantidadComidas.Jueves;
                    break;
                case DayOfWeek.Friday:
                    esteDiaSi = deportista.CantidadComidas.Viernes;
                    break;
                case DayOfWeek.Saturday:
                    esteDiaSi = deportista.CantidadComidas.Sabado;
                    break;
                case DayOfWeek.Sunday:
                    esteDiaSi = deportista.CantidadComidas.Domingo;
                    break;
            }

            if (esteDiaSi)
            {
                var tiempoAhora = DateTime.Now;

                var asistencias = db.Asistencias.Count(a =>
                        a.DeportistaId == asistencia.DeportistaId &&
                        tiempoAhora > a.HoraAsistencia
                        );

                if (asistencias >= (int)deportista.CantidadComidas.Cantidad)
                    return Json(new AsistenciaResultado { Aceptado = false, Razon = "Becas agotadas" });

                var horarioAhora = horarioR.List().Where(h => tiempoAhora.TimeOfDay >= h.Inicia && tiempoAhora.TimeOfDay <= h.Finaliza).FirstOrDefault();

                if (horarioAhora != null)
                {
                    var yaAsistioHoy = db.Asistencias.Where(a =>
                        a.HorarioId == asistencia.HorarioId &&
                        a.DeportistaId == asistencia.DeportistaId &&
                        tiempoAhora > a.HoraAsistencia
                        );

                    if (yaAsistioHoy == null)
                    {
                        if (asistenciaR.Create(asistencia))
                            return Json(new AsistenciaResultado { Aceptado = true, Razon = "Registro Exitoso" });
                    }
                    else
                        return Json(new AsistenciaResultado { Aceptado = false, Razon = "Ya ha entrado ha este horario" });
                }
                else
                    return Json(new AsistenciaResultado { Aceptado = false, Razon = "No se sirve comida en esta horario" });
            }

            return Json(new AsistenciaResultado { Aceptado = false, Razon = "No tiene beca para este dia" });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}