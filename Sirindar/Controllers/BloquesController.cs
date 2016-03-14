using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Sirindar.Core;
using Sirindar.Core.UnitOfWork;

namespace Sirindar.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BloquesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BloquesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public ActionResult Index()
        {
            ViewBag.json = new HtmlString(JsonConvert.SerializeObject(_unitOfWork.Bloques.GetAll(), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            return View();
        }

        // GET: /Bloques/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Bloques/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BloqueId,Nombre,KilocaloriasTotales")] Bloque bloque)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Bloques.Add(bloque);
                _unitOfWork.Complete();
                return RedirectToAction("Edit", new { id = bloque.BloqueId });
            }
            return View(bloque);
        }

        // GET: /Bloques/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bloque = _unitOfWork.Bloques.GetWithGrupos(id.Value);
            if (bloque == null)
            {
                return HttpNotFound();
            }
            ViewBag.json = new HtmlString(JsonConvert.SerializeObject(bloque.Grupos, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            return View(bloque);
        }

        // POST: /Bloques/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BloqueId,Nombre,KilocaloriasTotales")] Bloque bloque)
        {
            if (ModelState.IsValid)
            {
                var _bloque = _unitOfWork.Bloques.Get(bloque.BloqueId);

                _bloque.Nombre = bloque.Nombre;
                _bloque.KilocaloriasTotales = bloque.KilocaloriasTotales;
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(bloque);
        }

        // GET: /Bloques/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bloque = _unitOfWork.Bloques.Get(id.Value);
            if (bloque == null)
            {
                return HttpNotFound();
            }
            return PartialView(bloque);
        }

        // POST: /Bloques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _unitOfWork.Bloques.Remove(id);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }

        // GET: /Bloques/GrupoCreate
        public ActionResult GrupoCreate(int bloqueId)
        {
            ViewBag.BloqueId = bloqueId;
            ViewBag.GrupoAlimenticioId = new SelectList(_unitOfWork.GruposAlimenticios.GetAll(), "GrupoAlimenticioId", "Grupo");
            return PartialView("GrupoCreate");
        }

        // POST: /Bloques/GrupoCreate
        [HttpPost, ActionName("GrupoCreate")]
        [ValidateAntiForgeryToken]
        public ActionResult GrupoCreate([Bind(Include = "GrupoId,Nombre,Grupo,Porcentaje,Gramos,Kilocalorias,Equivalencias,BloqueId,GrupoAlimenticioId")] Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Grupos.Add(grupo);
                _unitOfWork.Complete();
                return RedirectToAction("Edit", new { id = grupo.BloqueId });

            }
            return RedirectToAction("Edit", grupo.BloqueId);
        }

        // GET: /Bloques/GrupoEdit/1
        public ActionResult GrupoEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var grupo = _unitOfWork.Grupos.Get(id.Value);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            ViewBag.GrupoAlimenticioId = new SelectList(_unitOfWork.GruposAlimenticios.GetAll(), "GrupoAlimenticioId", "Grupo", grupo.GrupoAlimenticio.GrupoAlimenticioId);
            return PartialView("GrupoEdit", grupo);
        }

        // POST: /Bloques/GrupoEdit/1
        [HttpPost, ActionName("GrupoEdit")]
        [ValidateAntiForgeryToken]
        public ActionResult GrupoEdit([Bind(Include = "Nombre,Grupo,Porcentaje,Gramos,Kilocalorias,Equivalencias,GrupoId,BloqueId,GrupoAlimenticioId")] Grupo model)
        {
            if (ModelState.IsValid)
            {
                var _grupo = _unitOfWork.Grupos.Get(model.GrupoId);
                _grupo.Nombre = model.Nombre;
                _grupo.Porcentaje = model.Porcentaje;
                _grupo.Gramos = model.Gramos;
                _grupo.Kilocalorias = model.Kilocalorias;
                _grupo.Equivalencias = model.Equivalencias;
                _grupo.BloqueId = model.BloqueId;
                _grupo.GrupoAlimenticioId = model.GrupoAlimenticioId;
                _unitOfWork.Complete();
                    return RedirectToAction("Edit", new { id = model.BloqueId });
                
            }
            return RedirectToAction("Edit", new { id = model.BloqueId });
        }

        // GET: /Bloques/GrupoDelete/1
        public ActionResult GrupoDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var grupo = _unitOfWork.Grupos.Get(id.Value);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            return PartialView("GrupoDelete", grupo);
        }

        // POST: /Bloques/GrupoDelete/1
        [HttpPost, ActionName("GrupoDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult GrupoDelete(int id)
        {
            var grupo = _unitOfWork.Grupos.Get(id);
            grupo.EsActivo = false;
            _unitOfWork.Complete();
            return RedirectToAction("Edit", new { id = grupo.BloqueId });
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
