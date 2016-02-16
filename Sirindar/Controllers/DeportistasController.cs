using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.UI;
using Sirindar.Helpers;
using CNSirindar.Repositories;
using CNSirindar.Models;
using Sirindar.Models;

namespace Sirindar.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DeportistasController : Controller
    {
        private IRepository<DeporteDeportista, int> _deporteDeportista;
        private IRepository<Dependencia, int> _dependencia;
        private IRepository<Deportista, int> _deportista;
        private IRepository<Deporte, int> _deporte;

        public DeportistasController(
            IRepository<DeporteDeportista, int> deporteDeportista,
            IRepository<Dependencia, int> dependencia,
            IRepository<Deportista, int> deportista,
            IRepository<Deporte, int> deporte            
            ) 
        {
            this._deporte = deporte;
            this._deportista = deportista;
            this._dependencia = dependencia;
            this._deporteDeportista = deporteDeportista;
        }
 
        // GET: /Deportistas/
        public ActionResult Index()
        {
            ViewBag.json = new HtmlString(JsonConvert.SerializeObject(_deportista.List(), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            return View();
        }

        //public ActionResult GetItemsJson(int start, int count)
        //{
        //    var l = deportistaR.List().Count;
        //    if (start > l)
        //        return null;
        //    count = (start + count > l) ? l - start : count;
        //    return Json(JsonConvert.SerializeObject(deportistaR.List().GetRange(start, count), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }), JsonRequestBehavior.AllowGet);
        //}

        // GET: /Deportistas/Create
        public ActionResult Create()
        {
            ViewBag.DependenciaId = new SelectList(_dependencia.List(), "DependenciaId", "Nombre");
            ViewBag.DeporteId = new SelectList(_deporte.List(), "DeporteId", "Nombre");
            return View();
        }

        // POST: /Deportistas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Matricula,Nombre,Apellidos,Genero,FechaNacimiento,IniciaEntrenamiento,FinalizaEntrenamiento,DependenciaId,Status,DeporteId")] DeportistaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var deportista = new Deportista
                {
                    Matricula = model.Matricula,
                    Nombre = model.Nombre,
                    Apellidos = model.Apellidos,
                    Genero = model.Genero,
                    Status = model.Status,
                    DependenciaId = model.DependenciaId,
                    FechaNacimiento = model.FechaNacimiento,
                    DeportesDeportistas = new List<DeporteDeportista> 
                    {
                        new DeporteDeportista
                        {
                            DeporteId = model.DeporteId,
                            IniciaEntrenamiento = model.IniciaEntrenamiento,
                            FinalizaEntrenamiento = model.FinalizaEntrenamiento,
                            FechaAlta = DateTime.Now,                        
                            EsActivo = true
                        }
                    },
                    AsignacionesBloques = new List<AsignacionBloque> 
                    {
                        new AsignacionBloque
                        {
                            DeporteId = model.DeporteId,
                            FechaAlta = DateTime.Now,
                            EsActivo = true
                        }
                    },
                    CantidadComidas = new CantidadComidas
                    {
                        Cantidad = NumeroComidas.Uno,
                        FechaAlta = DateTime.Now,
                        EsActivo = true
                    }
                };
                _deportista.Create(deportista);
                return RedirectToAction("Index");
            }
            ViewBag.DeporteId = new SelectList(_deporte.List(), "DeporteId", "Nombre");
            ViewBag.DependenciaId = new SelectList(_dependencia.List(), "DependenciaId", "Nombre");
            return View(model);
        }

        // GET: /Deportistas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var deportista = _deportista.Read(id);
            var model = new DeportistaEditViewModel
            {
                DeportistaId = deportista.DeportistaId,
                Matricula = deportista.Matricula,
                Nombre = deportista.Nombre,
                Apellidos = deportista.Apellidos,
                Genero = deportista.Genero,
                FechaNacimiento = (DateTime)deportista.FechaNacimiento,
                DependenciaId = deportista.DependenciaId,
                Status = deportista.Status
            };
            if (deportista == null)
            {
                return HttpNotFound();
            }

            ViewBag.Deportes = deportista.Deportes;
            ViewBag.Edad = ((TimeSpan)(DateTime.Now - deportista.FechaNacimiento)).Days / 365;
            ViewBag.Status = new SelectList(SirindarControls.EnumAsList<Status>(), "Value", "Text", (int)deportista.Status);
            ViewBag.Genero = new SelectList(SirindarControls.EnumAsList<Generos>(), "Value", "Text", (int)deportista.Genero);
            ViewBag.DependenciaId = new SelectList(_dependencia.List(), "DependenciaId", "Nombre", deportista.DependenciaId);
            return View(model);
        }

        // POST: /Deportistas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeportistaId,Matricula,Nombre,Apellidos,Genero,FechaNacimiento,DependenciaId,Status")] DeportistaEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var deportista = new Deportista
                {
                    DeportistaId = model.DeportistaId,
                    Matricula = model.Matricula,
                    Nombre = model.Nombre,
                    Apellidos = model.Apellidos,
                    Genero = model.Genero,
                    Status = model.Status,
                    DependenciaId = model.DependenciaId,
                    FechaNacimiento = model.FechaNacimiento
                };
                _deportista.Update(deportista);
                return RedirectToAction("Edit", new { id = model.DeportistaId });
            }

            ViewBag.Deportes = GeneralRepository.GetDeportes(model.DeportistaId);
            ViewBag.Edad = ((TimeSpan)(DateTime.Now - model.FechaNacimiento)).Days / 365;
            ViewBag.Status = new SelectList(SirindarControls.EnumAsList<Status>(), "Value", "Text", (int)model.Status);
            ViewBag.Genero = new SelectList(SirindarControls.EnumAsList<Generos>(), "Value", "Text", (int)model.Genero);
            ViewBag.DependenciaId = new SelectList(_dependencia.List(), "DependenciaId", "Nombre", model.DependenciaId);
            return View(model);
        }

        // GET: /Deportistas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deportista deportista = _deportista.Read(id);
            if (deportista == null)
            {
                return HttpNotFound();
            }
            return PartialView(deportista);
        }

        // POST: /Deportistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Deportista deportista = _deportista.Read(id);
            _deportista.Delete(deportista.DeportistaId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddDeporte(int? deportistaId)
        {
            if (deportistaId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ViewBag.DeportistaId = deportistaId;
            ViewBag.DeporteId = new SelectList(_deporte.List(), "DeporteId", "Nombre");
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDeporte([Bind(Include = "DeporteId,DeportistaId,IniciaEntrenamiento,FinalizaEntrenamiento")] CreateDeporteDeportistaViewModel model)
        {
            _deporteDeportista.Create(new DeporteDeportista
            {
                DeporteId = model.DeporteId,
                DeportistaId = model.DeportistaId,
                IniciaEntrenamiento = model.IniciaEntrenamiento,
                FinalizaEntrenamiento = model.FinalizaEntrenamiento
            });
            return RedirectToAction("Edit", new { id = model.DeportistaId });
        }

        [HttpGet]
        public ActionResult DeleteDeporte(int? deporteId)
        {
            if (deporteId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ///
            var deportistaDeporte = GeneralRepository.FindByDeporteId((int)deporteId);
            return PartialView(deportistaDeporte);
        }

        [HttpPost, ActionName("DeleteDeporte")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDeporte([Bind(Include = "DeporteDeportistaId,DeportistaId")]DeporteDeportista model)
        {
            _deporteDeportista.Delete(model.DeporteDeportistaId);
            return RedirectToAction("Edit", new { id = model.DeportistaId });
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
