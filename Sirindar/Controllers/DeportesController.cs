using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Sirindar.Core;
using Sirindar.Core.UnitOfWork;
using Sirindar.Helpers;

namespace Sirindar.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DeportesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeportesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            ViewBag.json = new HtmlString(JsonConvert.SerializeObject(_unitOfWork.Deportes.GetAll(), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, NullValueHandling = NullValueHandling.Include }));
            return View();
        }
        
        public ActionResult Create()
        {
            ViewBag.clasificacionDeporteId = new SelectList(_unitOfWork.ClasificacionesDeportes.GetAll(), "ClasificacionDeporteId", "Descripcion");
            return View();
        }

        // POST: /Deportes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeporteId,Nombre,TipoEnergia,Clasificacion,ClasificacionDeporteId")] Deporte deporte)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Deportes.Add(deporte);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            ViewBag.clasificacionDeporteId = new SelectList(_unitOfWork.ClasificacionesDeportes.GetAll(), "ClasificacionDeporteId", "Descripcion");
            return View(deporte);
        }

        // GET: /Deportes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var deporte = _unitOfWork.Deportes.Get(id.Value);
            if (deporte == null)
            {
                return HttpNotFound();
            }

            ViewBag.TipoEnergia = new SelectList(SirindarControls.EnumAsList<Energia>(), "Value", "Text", (int)deporte.TipoEnergia);
            ViewBag.clasificacionDeporteId = new SelectList(_unitOfWork.ClasificacionesDeportes.GetAll(), "ClasificacionDeporteId", "Descripcion", deporte.Clasificacion.ClasificacionDeporteId);
            return View(deporte);
        }

        // POST: /Deportes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeporteId,Nombre,TipoEnergia")] Deporte deporte, int clasificacionDeporteId)
        {
            if (ModelState.IsValid)
            {
                var _deporte = _unitOfWork.Deportes.Get(deporte.DeporteId);
                _deporte.Nombre = deporte.Nombre;
                _deporte.TipoEnergia = deporte.TipoEnergia;
                _deporte.Clasificacion = deporte.Clasificacion;
                _deporte.ClasificacionDeporteId = deporte.ClasificacionDeporteId;
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            ViewBag.TipoEnergia = new SelectList(SirindarControls.EnumAsList<Energia>(), "Value", "Text", (int)deporte.TipoEnergia);
            ViewBag.clasificacionDeporteId = new SelectList(_unitOfWork.ClasificacionesDeportes.GetAll(), "ClasificacionDeporteId", "Descripcion", deporte.Clasificacion.ClasificacionDeporteId);
            return View(deporte);
        }

        // GET: /Deportes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var deporte = _unitOfWork.Deportes.Get(id.Value);
            if (deporte == null)
            {
                return HttpNotFound();
            }
            return PartialView(deporte);
        }

        // POST: /Deportes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _unitOfWork.Deportes.Remove(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}
