using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using CNSirindar.Models;
using CNSirindar;
using CNSirindar.Repositories;
using SirindarApi.Models.Remotos;

namespace SirindarApi.Controllers
{
    public class AsistenciaController : ApiController
    {
        private readonly SirindarDbContext db = new SirindarDbContext();
        private readonly IRepository<Asistencia, int> _asistenciaR;
        private IRepository<Deportista, int> deportistaR;
        private readonly IRepository<Horario, int> _horarioR;

        public AsistenciaController(
            IRepository<Asistencia, int> asistenciaR,
            IRepository<Deportista, int> deportistaR,
            IRepository<Horario, int> horarioR)
        {
            _asistenciaR = asistenciaR;
            this.deportistaR = deportistaR;
            _horarioR = horarioR;
        }

        // POST api/Asistencia
        [ResponseType(typeof(AsistenciaResultado))]
        public IHttpActionResult PostRegistrarAsistencia(RegistrarAsistenciaViewModel asistencia)
        {
            var tiempoAhora = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dia = DateTime.Now.DayOfWeek;
            var deportista = db.Deportistas.Where(d => d.EsActivo).First(d => d.DeportistaId == asistencia.DeportistaId);
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
                var horarioAhora = _horarioR.List().FirstOrDefault(h => tiempoAhora.TimeOfDay >= h.Inicia && tiempoAhora.TimeOfDay <= h.Finaliza);

                if (horarioAhora != null)
                {
                    var asistenciasHoy = db.Asistencias.Where(a => a.EsActivo).Where(a =>
                            a.HoraAsistencia > DateTime.Today &&
                            a.DeportistaId == asistencia.DeportistaId);

                    if (asistenciasHoy.Count() >= (int)deportista.CantidadComidas.Cantidad)
                        return Json(new AsistenciaResultado { Aceptado = false, Razon = "Becas agotadas" });

                    var yaAsistioHoy = asistenciasHoy.FirstOrDefault(a => a.HorarioId == horarioAhora.HorarioId);

                    if (yaAsistioHoy == null)
                    {
                        if (_asistenciaR.Create(new Asistencia
                        {
                            HorarioId = horarioAhora.HorarioId,
                            DeportistaId = asistencia.DeportistaId,
                            HoraAsistencia = tiempoAhora,
                            EsActivo = true,
                            FechaModificacion = DateTime.Now,
                            FechaAlta = DateTime.Now
                        }))
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