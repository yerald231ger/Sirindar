using System.Web.Mvc;
using Newtonsoft.Json;
using Sirindar.Models;
using System.Net;
using Sirindar.Core;
using Sirindar.Core.UnitOfWork;
using Sirindar.Helpers;


namespace Sirindar.Controllers
{
    public class AsignacionHorariosController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AsignacionHorariosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //
        // GET: /AsignacionHorarios/
        public ActionResult Index(string updateState)
        {
            ViewBag.UpdateState = updateState ?? "";
            return View();
        }

        public JsonResult GetDeportistaByMatricula(string matricula)
        {
            var deportistas = _unitOfWork.Deportistas.SearchByMatricula(matricula);
            return Json(JsonConvert.SerializeObject(deportistas), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDeportistaByStringExpression(string expression)
        {
            var deportistas = _unitOfWork.Deportistas.SearchByStringExpression(expression);
            return Json(JsonConvert.SerializeObject(deportistas), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDeportistasByDeporteNombre(string deporteName)
        {
            var deportistas = _unitOfWork.Deportistas.SearchByDeporte(deporteName);
            return Json(JsonConvert.SerializeObject(deportistas), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDeportistasByDepenenciaNombre(string dependenciaNombre)
        {
            var deportistas = _unitOfWork.Deportistas.SearchByDependencia(dependenciaNombre);
            return Json(JsonConvert.SerializeObject(deportistas), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDeportesByNombre(string deporte)
        {
            var deportes = _unitOfWork.Deportes.SearchByNombre(deporte);
            return Json(JsonConvert.SerializeObject(deportes), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDependenciasByNombre(string dependencia)
        {
            var dependencias = _unitOfWork.Dependencias.SearchByNombre(dependencia);
            return Json(JsonConvert.SerializeObject(dependencias), JsonRequestBehavior.AllowGet);
        }

        //UpdateByDeportista
        public ActionResult UpdateCantidadComidasDeportista(string nombre, int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var cantidadComidas = _unitOfWork.HorariosComidas.Get(id.Value);
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
                var horarioComidas = _unitOfWork.HorariosComidas.Get(model.Id);
                horarioComidas.Cantidad = model.Numero;
                horarioComidas.Lunes = model.Monday;
                horarioComidas.Martes = model.Tuesday;
                horarioComidas.Miercoles = model.Wednesday;
                horarioComidas.Jueves = model.Tuesday;
                horarioComidas.Viernes = model.Friday;
                horarioComidas.Sabado = model.Saturday;
                horarioComidas.Domingo = model.Sunday;
                _unitOfWork.Complete();
                return RedirectToAction("Index", new { updateState = "Ok" });
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
                var horarioComidas = _unitOfWork.HorariosComidas.Get(model.Id);
                horarioComidas.Cantidad = model.Numero;
                horarioComidas.Lunes = model.Monday;
                horarioComidas.Martes = model.Tuesday;
                horarioComidas.Miercoles = model.Wednesday;
                horarioComidas.Jueves = model.Tuesday;
                horarioComidas.Viernes = model.Friday;
                horarioComidas.Sabado = model.Saturday;
                horarioComidas.Domingo = model.Sunday;
                _unitOfWork.Complete();
                return RedirectToAction("Index", new { updateState = "Ok" });
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
                var horarioComidas = _unitOfWork.HorariosComidas.Get(model.Id);
                horarioComidas.Cantidad = model.Numero;
                horarioComidas.Lunes = model.Monday;
                horarioComidas.Martes = model.Tuesday;
                horarioComidas.Miercoles = model.Wednesday;
                horarioComidas.Jueves = model.Tuesday;
                horarioComidas.Viernes = model.Friday;
                horarioComidas.Sabado = model.Saturday;
                horarioComidas.Domingo = model.Sunday;
                _unitOfWork.Complete();
                return RedirectToAction("Index", new { updateState = "Ok" });
            }
            return RedirectToAction("Index", new { updateState = "false" });
        }

    }
}