using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Sirindar.Core;
using Sirindar.Core.UnitOfWork;
using Sirindar.Helpers;
using Sirindar.Models;

namespace Sirindar.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DeportistasController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeportistasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: /Deportistas/
        public ActionResult Index()
        {
            ViewBag.json = new HtmlString(JsonConvert.SerializeObject(_unitOfWork.Deportistas.GetAll(), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            return View();
        }

        // GET: /Deportistas/Create
        public ActionResult Create()
        {
            ViewBag.DependenciaId = new SelectList(_unitOfWork.Dependencias.GetAll(), "DependenciaId", "Nombre");
            ViewBag.DeporteId = new SelectList(_unitOfWork.Deportes.GetAll(), "DeporteId", "Nombre");
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
                            FinalizaEntrenamiento = model.FinalizaEntrenamiento
                        }
                    },
                    AsignacionesBloques = new List<AsignacionBloque>
                    {
                        new AsignacionBloque
                        {
                            DeporteId = model.DeporteId
                        }
                    },
                    HorarioComidas = new HorarioComidas
                    {
                        Cantidad = NumeroComidas.Uno,
                        Lunes = true,
                        Martes = true,
                        Miercoles = true,
                        Jueves = true,
                        Viernes = true,
                        Sabado = true,
                        Domingo = true
                    }
                };
                _unitOfWork.Deportistas.Add(deportista);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            ViewBag.DeporteId = new SelectList(_unitOfWork.Deportes.GetAll(), "DeporteId", "Nombre");
            ViewBag.DependenciaId = new SelectList(_unitOfWork.Dependencias.GetAll(), "DependenciaId", "Nombre");
            return View(model);
        }

        // GET: /Deportistas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var deportista = _unitOfWork.Deportistas.GetWithDeportes(id.Value);

            if (deportista == null)
            {
                return HttpNotFound();
            }

            var model = new DeportistaEditViewModel
            {
                DeportistaId = deportista.DeportistaId,
                Matricula = deportista.Matricula,
                Nombre = deportista.Nombre,
                Apellidos = deportista.Apellidos,
                Genero = deportista.Genero,
                FechaNacimiento = deportista.FechaNacimiento,
                DependenciaId = deportista.DependenciaId,
                Status = deportista.Status
            };

            ViewBag.Deportes = deportista.Deportes;
            ViewBag.Edad = (DateTime.Now - deportista.FechaNacimiento).Days / 365;
            ViewBag.Status = new SelectList(SirindarControls.EnumAsList<Status>(), "Value", "Text", (int)deportista.Status);
            ViewBag.Genero = new SelectList(SirindarControls.EnumAsList<Generos>(), "Value", "Text", (int)deportista.Genero);
            ViewBag.DependenciaId = new SelectList(_unitOfWork.Dependencias.GetAll(), "DependenciaId", "Nombre", deportista.DependenciaId);
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

                var deportista = _unitOfWork.Deportistas.GetWithDeportes(model.DeportistaId);

                deportista.DeportistaId = model.DeportistaId;
                deportista.Matricula = model.Matricula;
                deportista.Nombre = model.Nombre;
                deportista.Apellidos = model.Apellidos;
                deportista.Genero = model.Genero;
                deportista.Status = model.Status;
                deportista.DependenciaId = model.DependenciaId;
                deportista.FechaNacimiento = model.FechaNacimiento;

                _unitOfWork.Complete();

                return RedirectToAction("Edit", new { id = model.DeportistaId });
            }

            ViewBag.Deportes = _unitOfWork.Deportistas.GetWithDeportes(model.DeportistaId).Deportes;
            ViewBag.Edad = (DateTime.Now - model.FechaNacimiento).Days / 365;
            ViewBag.Status = new SelectList(SirindarControls.EnumAsList<Status>(), "Value", "Text", (int)model.Status);
            ViewBag.Genero = new SelectList(SirindarControls.EnumAsList<Generos>(), "Value", "Text", (int)model.Genero);
            ViewBag.DependenciaId = new SelectList(_unitOfWork.Dependencias.GetAll(), "DependenciaId", "Nombre", model.DependenciaId);
            return View(model);
        }

        // GET: /Deportistas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var deportista = _unitOfWork.Deportistas.Get(id.Value);
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
            _unitOfWork.Deportistas.Remove(id);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddDeporte(int? deportistaId)
        {
            if (deportistaId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ViewBag.DeportistaId = deportistaId;
            ViewBag.DeporteId = new SelectList(_unitOfWork.Deportes.GetAll(), "DeporteId", "Nombre");
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDeporte([Bind(Include = "DeporteId,DeportistaId,IniciaEntrenamiento,FinalizaEntrenamiento")] CreateDeporteDeportistaViewModel model)
        {
            _unitOfWork.Deportistas.AddDeporte(model.DeportistaId, model.DeporteId, model.IniciaEntrenamiento,
                model.FinalizaEntrenamiento);
            _unitOfWork.Complete();
            return RedirectToAction("Edit", new { id = model.DeportistaId });
        }

        [HttpGet]
        public ActionResult DeleteDeporte(int? deporteId)
        {
            if (deporteId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var deportistaDeporte = _unitOfWork.DeportesDeportistas.FindByDeporteId(deporteId.Value);
            return PartialView(deportistaDeporte);
        }

        [HttpPost, ActionName("DeleteDeporte")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDeporte([Bind(Include = "DeporteDeportistaId,DeportistaId,DeporteId")]DeporteDeportista model)
        {
            _unitOfWork.Deportistas.RemoveDeporte(model.DeportistaId, model.DeporteId);
            _unitOfWork.Complete();
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
