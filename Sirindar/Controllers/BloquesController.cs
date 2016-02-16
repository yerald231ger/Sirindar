using System;
using System.Net;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Sirindar.Models;
using System.Data.Entity;
using System.Collections.Generic;
using CNSirindar.Models;
using CNSirindar.Repositories;

namespace Sirindar.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BloquesController : Controller
    {
        private IRepository<GrupoAlimenticio, int> _grupoAlimenticio;
        private IRepository<Bloque, int> _bloque;
        private IRepository<Grupo, int> _grupo;

        public BloquesController(
            IRepository<GrupoAlimenticio, int> grupoAlimenticio,
            IRepository<Bloque, int> bloque,
            IRepository<Grupo, int> grupo
            )
        {
            this._grupo = grupo;
            this._bloque = bloque;
            this._grupoAlimenticio = grupoAlimenticio;
        }


        public ActionResult Index()
        {
            ViewBag.json = new HtmlString(JsonConvert.SerializeObject(_bloque.List().Take(1000), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
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
                var ok = _bloque.Create(bloque);
                if (ok)
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
            var bloque = _bloque.Read(id);
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
                _bloque.Update(bloque);
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
            var bloque = _bloque.Read(id);
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
            _bloque.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: /Bloques/GrupoCreate
        public ActionResult GrupoCreate(int bloqueId)
        {
            ViewBag.BloqueId = bloqueId;
            ViewBag.GrupoAlimenticioId = new SelectList(_grupoAlimenticio.List(), "GrupoAlimenticioId", "Grupo");
            return PartialView("GrupoCreate");
        }

        // POST: /Bloques/GrupoCreate
        [HttpPost, ActionName("GrupoCreate")]
        [ValidateAntiForgeryToken]
        public ActionResult GrupoCreate([Bind(Include = "GrupoId,Nombre,Grupo,Porcentaje,Gramos,Kilocalorias,Equivalencias,BloqueId,GrupoAlimenticioId")] Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                var ok = _grupo.Create(grupo);
                if (ok)
                {
                    return RedirectToAction("Edit", new { id = grupo.BloqueId });
                }
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
            var grupo = _grupo.Read(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            ViewBag.GrupoAlimenticioId = new SelectList(_grupoAlimenticio.List(), "GrupoAlimenticioId", "Grupo", grupo.GrupoAlimenticio.GrupoAlimenticioId);
            return PartialView("GrupoEdit", grupo);
        }

        // POST: /Bloques/GrupoEdit/1
        [HttpPost, ActionName("GrupoEdit")]
        [ValidateAntiForgeryToken]
        public ActionResult GrupoEdit([Bind(Include = "Nombre,Grupo,Porcentaje,Gramos,Kilocalorias,Equivalencias,GrupoId,BloqueId,GrupoAlimenticioId")] Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                var ok = _grupo.Update(grupo);
                if (ok)
                {
                    return RedirectToAction("Edit", new { id = grupo.BloqueId });
                }
            }
            return RedirectToAction("Edit", new { id = grupo.BloqueId });
        }

        // GET: /Bloques/GrupoDelete/1
        public ActionResult GrupoDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var grupo = _grupo.Read(id);
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
            var bloqueId = _grupo.Read(id).BloqueId;
            _grupo.Delete(id);
            return RedirectToAction("Edit", new { id = bloqueId });
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
