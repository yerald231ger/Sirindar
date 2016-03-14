using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Sirindar.Core;
using Sirindar.Core.Repositories;
using Sirindar.Core.UnitOfWork;
using Sirindar.Entity;
using SirindarApi.Models.Remotos;

namespace SirindarApi.Controllers
{
    [Authorize]
    public class AsistenciaController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SirindarDbContext db = new SirindarDbContext();

        public AsistenciaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // POST api/Asistencia
        [ResponseType(typeof(AsistenciaResultado))]
        public IHttpActionResult PostRegistrarAsistencia(RegistrarAsistenciaViewModel asistencia)
        {
            var tiempoAhora = DateTime.Now;

            if (asistencia == null)
            {
                ModelState.AddModelError("Model", new NullReferenceException("El objeto 'RegistrarAsistenciaViewModel' contiene una referencia nula"));
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dia = DateTime.Now.DayOfWeek;
            var horarioComidas = _unitOfWork.Deportistas.GetHorarioComidas(asistencia.DeportistaId.Value);

            var esteDiaSi = false;

            switch (dia)
            {
                case DayOfWeek.Monday:
                    esteDiaSi = horarioComidas.Lunes;
                    break;
                case DayOfWeek.Tuesday:
                    esteDiaSi = horarioComidas.Martes;
                    break;
                case DayOfWeek.Wednesday:
                    esteDiaSi = horarioComidas.Miercoles;
                    break;
                case DayOfWeek.Thursday:
                    esteDiaSi = horarioComidas.Jueves;
                    break;
                case DayOfWeek.Friday:
                    esteDiaSi = horarioComidas.Viernes;
                    break;
                case DayOfWeek.Saturday:
                    esteDiaSi = horarioComidas.Sabado;
                    break;
                case DayOfWeek.Sunday:
                    esteDiaSi = horarioComidas.Domingo;
                    break;
            }

            if (!esteDiaSi)
                return Json(new AsistenciaResultado {Aceptado = false, Razon = "No tiene beca para este dia"});

            var horarioAhora = _unitOfWork.Horarios.GetHorarioBetweenHour(tiempoAhora);

            if (horarioAhora == null)
                return Json(new AsistenciaResultado {Aceptado = false, Razon = "No se sirve comida en esta horario"});

            var asistenciasHoy = _unitOfWork.Asistencias.GetAsistenciasDeportistaToday(asistencia.DeportistaId.Value).ToList();

            if (asistenciasHoy.Count >= (int)horarioComidas.Cantidad)
                return Json(new AsistenciaResultado { Aceptado = false, Razon = "Becas agotadas" });

            var yaAsistioHoy = asistenciasHoy.FirstOrDefault(a => a.HorarioId == horarioAhora.HorarioId);

            if (yaAsistioHoy != null)
                return Json(new AsistenciaResultado {Aceptado = false, Razon = "Ya ha entrado ha este horario"});

            _unitOfWork.Asistencias.Add(new Asistencia
            {
                HorarioId = horarioAhora.HorarioId,
                DeportistaId = asistencia.DeportistaId.Value,
                HoraAsistencia = tiempoAhora,
                EsActivo = true,
                FechaModificacion = DateTime.Now,
                FechaAlta = DateTime.Now
            });
            _unitOfWork.Complete();
            return Json(new AsistenciaResultado { Aceptado = true, Razon = "Registro Exitoso" });
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