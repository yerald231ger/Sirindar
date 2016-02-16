using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Sirindar.Helpers.Extensions;
using Sirindar.Helpers;
using CNSirindar.Models;
using CNSirindar.Repositories;

namespace Sirindar.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DeportesController : Controller
    {
        private IRepository<Deporte, int> _deporte;
        private IRepository<ClasificacionDeporte, int> _clasificacion;

        public DeportesController(
            IRepository<Deporte, int> deporte,
            IRepository<ClasificacionDeporte, int> clasificacion
            )
        {
            this._deporte = deporte;
            this._clasificacion = clasificacion;
        }

        public ActionResult Index()
        {
            ViewBag.json = new HtmlString(JsonConvert.SerializeObject(_deporte.List().Take(100), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, NullValueHandling = NullValueHandling.Include }));
            return View();
        }

        public JsonResult GetItemsJson(int start, int count)
        {
            var l = _deporte.List().Count;
            if (start > l)
                return null;
            count = (start + count > l) ? l - start : count;
            return Json(JsonConvert.SerializeObject(_deporte.List().GetRange(start, count), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.clasificacionDeporteId = new SelectList(_clasificacion.List(), "ClasificacionDeporteId", "Descripcion");
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
                _deporte.Create(deporte);
                return RedirectToAction("Index");
            }
            ViewBag.clasificacionDeporteId = new SelectList(_clasificacion.List(), "ClasificacionDeporteId", "Descripcion");
            return View(deporte);
        }

        // GET: /Deportes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deporte deporte = _deporte.Read(id);
            if (deporte == null)
            {
                return HttpNotFound();
            }

            ViewBag.TipoEnergia = new SelectList(SirindarControls.EnumAsList<Energia>(), "Value", "Text", (int)deporte.TipoEnergia);
            ViewBag.clasificacionDeporteId = new SelectList(_clasificacion.List(), "ClasificacionDeporteId", "Descripcion", deporte.Clasificacion.ClasificacionDeporteId);
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
                deporte.ClasificacionDeporteId = clasificacionDeporteId;
                _deporte.Update(deporte);
                return RedirectToAction("Index");
            }
            ViewBag.TipoEnergia = new SelectList(SirindarControls.EnumAsList<Energia>(), "Value", "Text", (int)deporte.TipoEnergia);
            ViewBag.clasificacionDeporteId = new SelectList(_clasificacion.List(), "ClasificacionDeporteId", "Descripcion", deporte.Clasificacion.ClasificacionDeporteId);
            return View(deporte);
        }

        // GET: /Deportes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deporte deporte = _deporte.Read(id);
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
            _deporte.Delete(id);
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
