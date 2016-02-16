using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CNSirindar.Repositories;
using CNSirindar.Models;
using Newtonsoft.Json;
using System.Net;
using Sirindar.Models;

namespace Sirindar.Controllers
{
    public class AsignacionBloquesController : Controller
    {
        private IRepository<AsignacionBloque, int> _asignacionBloque;
        private IRepository<Deporte, int> _deporte;
        private IRepository<Deportista, int> _deportista;
        private IRepository<Bloque, int> _bloque;

        public AsignacionBloquesController(
            IRepository<AsignacionBloque, int> asignacionBloque,
            IRepository<Deporte, int> deporte,
            IRepository<Deportista, int> deportista,
            IRepository<Bloque, int> bloque)
        {
            this._asignacionBloque = asignacionBloque;
            this._deporte = deporte;
            this._deportista = deportista;
            this._bloque = bloque;
        }

        // GET: /AsignacionBloques
        public ActionResult Index()
        {
            ViewBag.json = new HtmlString(JsonConvert.SerializeObject(GeneralRepository.GridAsignaciones().Deportistas, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            return View();
        }

        public ActionResult CreateNew()
        {
            ViewBag.BloqueId = new SelectList(_bloque.List(), "BloqueId", "Nombre");
            ViewBag.DeportistaId = new SelectList(_deportista.List(), "DeportistaId", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNew(
            [Bind(Include = "DeporteId,BloqueId,DeportistaId")]
            CreateAsignacionBloquesViewModel model
            )
        {
            var asignacionBloque = new AsignacionBloque
            {
                DeporteId = model.DeporteId,
                DeportistaId = model.DeportistaId,
                BloqueId = model.BloqueId
            };

            if (GeneralRepository.IsDeporteDeportista(asignacionBloque))
                ModelState.AddModelError("ExistDeporteDeportista", "La asignacion seleccionada ya existe");

            if (ModelState.IsValid)
            {
                _asignacionBloque.Create(asignacionBloque);
                return RedirectToAction("Index");
            }

            ViewBag.BloqueId = new SelectList(_bloque.List(), "BloqueId", "Nombre", model.BloqueId);
            ViewBag.DeportistaId = new SelectList(_deportista.List(), "DeportistaId", "Nombre", model.DeportistaId);
            return View();
        }

        public ActionResult Create(int? deportistaId, int? deporteId)
        {
            if (deportistaId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var deportista = _deportista.Read(deportistaId);
            if (deportista == null)
            {
                return HttpNotFound();
            }

            ViewBag.DeportistaId = deportistaId;
            ViewBag.DeportistaNombre = GeneralRepository.GetFullName(deportistaId.Value);
            ViewBag.DeporteId = new SelectList(deportista.Deportes, "DeporteId", "Nombre", deporteId);
            ViewBag.BloqueId = new SelectList(_bloque.List(), "BloqueId", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "DeporteId,BloqueId,DeportistaId")]
            CreateAsignacionBloquesViewModel model
            )
        {
            var asignacionBloque = new AsignacionBloque
                {
                    DeporteId = model.DeporteId,
                    DeportistaId = model.DeportistaId,
                    BloqueId = model.BloqueId
                };

            if (GeneralRepository.IsDeporteDeportista(asignacionBloque))
                ModelState.AddModelError("ExistDeporteDeportista", "La asignacion seleccionada ya existe");

            if (ModelState.IsValid)
            {
                _asignacionBloque.Create(asignacionBloque);
                return RedirectToAction("Index");
            }

            ViewBag.DeportistaId = model.DeportistaId;
            ViewBag.DeportistaNombre = GeneralRepository.GetFullName(asignacionBloque.DeportistaId);
            ViewBag.BloqueId = new SelectList(_bloque.List(), "BloqueId", "Nombre");
            ViewBag.DeporteId = new SelectList(_deportista.Read(model.DeportistaId).Deportes, "DeporteId", "Nombre");
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var asignacionBloque = _asignacionBloque.Read(id);
            if (asignacionBloque == null)
            {
                return HttpNotFound();
            }
            var model = new UpdateAsignacionBloquesViewModel
            {
                AsignacionBloqueId = asignacionBloque.AsignacionBloqueId,
                DeportistaId = asignacionBloque.DeportistaId,
                DeporteId = asignacionBloque.DeporteId,
                BloqueId = asignacionBloque.BloqueId ?? 0
            };

            ViewBag.DeportistaId = model.DeportistaId;
            ViewBag.DeportistaNombre = asignacionBloque.Deportista.ToString();
            ViewBag.BloqueId = new SelectList(_bloque.List(), "BloqueId", "Nombre", asignacionBloque.BloqueId);
            ViewBag.DeporteId = new SelectList(asignacionBloque.Deportista.Deportes, "DeporteId", "Nombre", asignacionBloque.DeporteId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AsignacionBloqueId,BloqueId,DeporteId,DeportistaId")]UpdateAsignacionBloquesViewModel model)
        {
            var asignacionBloque = new AsignacionBloque
                            {
                                AsignacionBloqueId = model.AsignacionBloqueId,
                                DeporteId = model.DeporteId,
                                DeportistaId = model.DeportistaId,
                                BloqueId = model.BloqueId
                            };

            if (GeneralRepository.IsDeporteDeportista(asignacionBloque))
                ModelState.AddModelError("ExistDeporteDeportista", "La asignacion seleccionada ya existe");

            if (ModelState.IsValid)
            {
                _asignacionBloque.Update(asignacionBloque);
                return RedirectToAction("Index");
            }

            ViewBag.DeportistaNombre = GeneralRepository.GetFullName(asignacionBloque.DeportistaId);
            ViewBag.BloqueId = new SelectList(_bloque.List(), "BloqueId", "Nombre", model.BloqueId);
            ViewBag.DeportistaId = new SelectList(_deportista.List(), "DeportistaId", "Nombre", model.DeportistaId);
            ViewBag.DeporteId = new SelectList(_deportista.Read(model.DeportistaId).Deportes, "DeporteId", "Nombre", model.DeporteId);
            return View(model);
        }

        // GET: /Bloques/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var asignacionBloque = _asignacionBloque.Read(id);
            if (asignacionBloque == null)
            {
                return HttpNotFound();
            }
            return PartialView(asignacionBloque);
        }

        // POST: /Bloques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _asignacionBloque.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
