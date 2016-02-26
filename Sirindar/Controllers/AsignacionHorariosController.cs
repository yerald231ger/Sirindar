using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using CNSirindar.Repositories;
using CNSirindar.Models;
using Sirindar.Models;
using System.Net;
using Sirindar.Helpers;


namespace Sirindar.Controllers
{
    public class AsignacionHorariosController : Controller
    {
        //
        // GET: /AsignacionHorarios/
        public ActionResult Index(string updateState)
        {
            ViewBag.UpdateState = updateState ?? "";
            return View();
        }

        public JsonResult GetDeportistaByMatricula(string matricula)
        {
            var deportistas = GeneralRepository.SearchDeportistasByMatricula(matricula);
            return Json(JsonConvert.SerializeObject(deportistas), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDeportistaByStringExpression(string expression)
        {
            var deportistas = GeneralRepository.SearchDeportistasByStringExpression(expression);
            return Json(JsonConvert.SerializeObject(deportistas), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDeportistasByDeporteNombre(string deporteName)
        {
            var deportistas = GeneralRepository.SearchDeportistasByDeporteName(deporteName);
            return Json(JsonConvert.SerializeObject(deportistas), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDeportistasByDepenenciaNombre(string dependenciaNombre)
        {
            var deportistas = GeneralRepository.SearchDeportistasByDepenenciaNombre(dependenciaNombre);
            return Json(JsonConvert.SerializeObject(deportistas), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDeportesByNombre(string deporte)
        {
            var deportes = GeneralRepository.SearchDeportesByName(deporte);
            return Json(JsonConvert.SerializeObject(deportes), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDependenciasByNombre(string dependencia)
        {
            var dependencias = GeneralRepository.SearchDependenciasByName(dependencia);
            return Json(JsonConvert.SerializeObject(dependencias), JsonRequestBehavior.AllowGet);
        }

        //UpdateByDeportista
        public ActionResult UpdateCantidadComidasDeportista(string nombre, int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var cantidadComidas = GeneralRepository.LoadHorarioDeportista(id.Value);
            ViewBag.Numero = new SelectList(SirindarControls.EnumAsList<NumeroComidas>(), "Value", "Text", (int)cantidadComidas.Cantidad);
            return PartialView(new AsignacionHorariosViewModel
            {
                Nombre = nombre,
                Id = id.Value,
                Monday = cantidadComidas.Lunes,
                Tuesday = cantidadComidas.Martes,
                Wednesday = cantidadComidas.Miercoles,
                Thursday = cantidadComidas.Jueves,
                Friday = cantidadComidas.Viernes,
                Saturday = cantidadComidas.Sabado,
                Sunday = cantidadComidas.Domingo
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCantidadComidasDeportista(AsignacionHorariosViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ok = GeneralRepository.UpdateCantidadComidas(
                    new CantidadComidas
                        {
                            Cantidad = model.Numero,
                            DeportistaId = model.Id,
                            Lunes = model.Monday,
                            Martes = model.Tuesday,
                            Miercoles = model.Wednesday,
                            Jueves = model.Thursday,
                            Viernes = model.Friday,
                            Sabado = model.Saturday,
                            Domingo = model.Sunday
                        });
                if (ok)
                    return RedirectToAction("Index", new { updateState = ok.ToString() });
            }
            return RedirectToAction("Index", new { updateState = "false" });
        }

        //UpdateByDeporte
        public ActionResult UpdateCantidadComidasByDeporte(string nombre, int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return PartialView(new AsignacionHorariosViewModel { Nombre = nombre, Id = id.Value });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCantidadComidasByDeporte(AsignacionHorariosViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ok = GeneralRepository.UpdateCantidadComidasByDeporte(model.Id,
                     new CantidadComidas
                     {
                         Cantidad = model.Numero,
                         DeportistaId = model.Id,
                         Lunes = model.Monday,
                         Martes = model.Tuesday,
                         Miercoles = model.Wednesday,
                         Jueves = model.Thursday,
                         Viernes = model.Friday,
                         Sabado = model.Saturday,
                         Domingo = model.Sunday
                     });
                if (ok)
                    return RedirectToAction("Index", new { updateState = ok.ToString() });
            }
            return RedirectToAction("Index", new { updateState = "false" });
        }

        //UpdateByDependencia
        public ActionResult UpdateCantidadComidasByDependencia(string nombre, int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return PartialView(new AsignacionHorariosViewModel { Nombre = nombre, Id = id.Value });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCantidadComidasByDependencia(AsignacionHorariosViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ok = GeneralRepository.UpdateCantidadComidasByDependencia(model.Id,
                     new CantidadComidas
                     {
                         Cantidad = model.Numero,
                         DeportistaId = model.Id,
                         Lunes = model.Monday,
                         Martes = model.Tuesday,
                         Miercoles = model.Wednesday,
                         Jueves = model.Thursday,
                         Viernes = model.Friday,
                         Sabado = model.Saturday,
                         Domingo = model.Sunday
                     });
                if (ok)
                    return RedirectToAction("Index", new { updateState = ok.ToString() });
            }
            return RedirectToAction("Index", new { updateState = "false" });
        }

    }
}