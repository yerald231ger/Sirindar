using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net;
using Sirindar.Core;
using Sirindar.Core.UnitOfWork;
using Sirindar.Models;

namespace Sirindar.Controllers
{
    public class AsignacionBloquesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AsignacionBloquesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: /AsignacionBloques
        public ActionResult Index()
        {
            ViewBag.json = new HtmlString(JsonConvert.SerializeObject(GridAsignaciones().Deportistas, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            return View();
        }

        public ActionResult CreateNew()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNew(
            [Bind(Include = "DeporteId,BloqueId,DeportistaId")]
            CreateAsignacionBloquesViewModel model
            )
        {
            if (_unitOfWork.AsignacionesBloques.IsAsigancionGrupos(model.DeporteId, model.DeportistaId, model.BloqueId))
                ModelState.AddModelError("ExistDeporteDeportista", "La asignacion seleccionada ya existe");

            if (ModelState.IsValid)
            {
                _unitOfWork.AsignacionesBloques.Add(new AsignacionBloque
                {
                    DeporteId = model.DeporteId,
                    DeportistaId = model.DeportistaId,
                    BloqueId = model.BloqueId
                });
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            ViewBag.BloqueId = new SelectList(_unitOfWork.Bloques.GetAll(), "BloqueId", "Nombre", model.BloqueId);
            ViewBag.DeportistaId = new SelectList(_unitOfWork.Deportistas.GetAll(), "DeportistaId", "Nombre", model.DeportistaId);
            return View();
        }

        public ActionResult Create(int? deportistaId, int? deporteId)
        {
            if (deportistaId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var deportista = _unitOfWork.Deportistas.GetWithDeportes(deportistaId.Value);
            if (deportista == null)
            {
                return HttpNotFound();
            }

            ViewBag.DeportistaId = deportistaId;
            ViewBag.DeportistaNombre = deportista.ToString();
            ViewBag.DeporteId = new SelectList(deportista.Deportes, "DeporteId", "Nombre", deporteId);
            ViewBag.BloqueId = new SelectList(_unitOfWork.Bloques.GetAll(), "BloqueId", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "DeporteId,BloqueId,DeportistaId")]
            CreateAsignacionBloquesViewModel model
            )
        {
            if (_unitOfWork.AsignacionesBloques.IsAsigancionGrupos(model.DeporteId, model.DeportistaId, model.BloqueId))
                ModelState.AddModelError("ExistDeporteDeportista", "La asignacion seleccionada ya existe");

            if (ModelState.IsValid)
            {
                _unitOfWork.AsignacionesBloques.Add(new AsignacionBloque
                {
                    DeporteId = model.DeporteId,
                    DeportistaId = model.DeportistaId,
                    BloqueId = model.BloqueId
                });
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            var deportista = _unitOfWork.Deportistas.GetWithDeportes(model.DeportistaId);

            ViewBag.DeportistaId = deportista.DeportistaId;
            ViewBag.DeportistaNombre = deportista.ToString();
            ViewBag.BloqueId = new SelectList(_unitOfWork.Bloques.GetAll(), "BloqueId", "Nombre");
            ViewBag.DeporteId = new SelectList(deportista.Deportes, "DeporteId", "Nombre");
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var asignacionBloque = _unitOfWork.AsignacionesBloques.Get(id.Value);
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

            var deportista = _unitOfWork.Deportistas.GetWithDeportes(model.DeportistaId);

            ViewBag.DeportistaId = deportista.DeportistaId;
            ViewBag.DeportistaNombre = deportista.ToString();
            ViewBag.BloqueId = new SelectList(_unitOfWork.Bloques.GetAll(), "BloqueId", "Nombre", asignacionBloque.BloqueId);
            ViewBag.DeporteId = new SelectList(deportista.Deportes, "DeporteId", "Nombre", asignacionBloque.DeporteId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AsignacionBloqueId,BloqueId,DeporteId,DeportistaId")]UpdateAsignacionBloquesViewModel model)
        {
            if (_unitOfWork.AsignacionesBloques.IsAsigancionGrupos(model.DeporteId, model.DeportistaId, model.BloqueId))
                ModelState.AddModelError("ExistDeporteDeportista", "La asignacion seleccionada ya existe");

            if (ModelState.IsValid)
            {
                var asignacionBloque = _unitOfWork.AsignacionesBloques.Get(model.AsignacionBloqueId);
                asignacionBloque.DeporteId = model.DeporteId;
                asignacionBloque.DeportistaId = model.DeportistaId;
                asignacionBloque.BloqueId = model.BloqueId;
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            var deportista = _unitOfWork.Deportistas.GetWithDeportes(model.DeportistaId);

            ViewBag.DeportistaNombre = deportista.ToString();
            ViewBag.BloqueId = new SelectList(_unitOfWork.Bloques.GetAll(), "BloqueId", "Nombre", model.BloqueId);
            ViewBag.DeportistaId = new SelectList(_unitOfWork.Deportistas.GetAll(), "DeportistaId", "Nombre", model.DeportistaId);
            ViewBag.DeporteId = new SelectList(deportista.Deportes, "DeporteId", "Nombre", model.DeporteId);
            return View(model);
        }

        // GET: /Bloques/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var asignacionBloque = _unitOfWork.AsignacionesBloques.Get(id.Value);
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
            _unitOfWork.AsignacionesBloques.Remove(id);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }

        public TableAsignacionBloques GridAsignaciones()
        {
            var groupDeportistas = _unitOfWork.AsignacionesBloques.GetAsignacionBloquesByMatriucla();

            var tableAsignaciones = new TableAsignacionBloques {Deportistas = new List<RowDeportista>()};

            foreach (var deportista in groupDeportistas)
            {
                var key = deportista.Key;
                var rowDeportista = deportista
                    .Where(x => x.Deportista.Matricula == key)
                    .Select(x => new RowDeportista
                    {
                        Matricula = x.Deportista.Matricula,
                        Nombre = x.Deportista.Nombre,
                        DeportistaId = x.DeportistaId
                    }).First();

                rowDeportista.Deportes = new List<RowDeporte>();

                var groupDeporte = deportista.Where(x => x.Deportista.Matricula == key).GroupBy(x => x.DeporteId).ToList();

                var listIds = new List<int>();
                foreach (var deporte in groupDeporte)
                {
                    var key2 = deporte.Key;
                    var rowDeporte = deporte
                        .Where(x => x.DeporteId == key2)
                        .Select(x => new RowDeporte
                        {
                            DeporteId = x.DeporteId,
                            Nombre = x.Deporte.Nombre
                        }).First();

                    var listRowBloque = deporte
                        .Where(dd => dd.DeporteId == key2)
                        .Select(dd =>
                        {
                            if (dd.Bloque != null)
                            {
                                return new RowBloque
                                {
                                    Nombre = dd.Bloque.Nombre,
                                    // ReSharper disable once PossibleInvalidOperationException
                                    BloqueId = dd.BloqueId.Value,
                                    DeporteDeportistaId = dd.AsignacionBloqueId
                                };
                            }
                            else
                            {
                                return new RowBloque
                                {
                                    Nombre = "Sin bloque",
                                    DeporteDeportistaId = dd.AsignacionBloqueId
                                };
                            }
                        }).ToList();

                    rowDeporte.Bloques = listRowBloque;
                    rowDeporte.RowSpan = listRowBloque.Select(lrb => lrb.DeporteDeportistaId).ToArray();
                    listIds.AddRange(listRowBloque.Select(lrb => lrb.DeporteDeportistaId).ToArray());
                    rowDeportista.Deportes.Add(rowDeporte);
                }
                rowDeportista.RowSpan = listIds.ToArray();
                tableAsignaciones.Deportistas.Add(rowDeportista);
            }

            return tableAsignaciones;

        }

        public class TableAsignacionBloques
        {
            public List<RowDeportista> Deportistas { get; set; }
        }

        public class RowDeportista
        {
            public int[] RowSpan { get; set; }
            public int DeportistaId { get; set; }
            public string Matricula { get; set; }
            public string Nombre { get; set; }
            public List<RowDeporte> Deportes { get; set; }
        }

        public class RowDeporte
        {
            public int[] RowSpan { get; set; }
            public int DeporteId { get; set; }
            public string Nombre { get; set; }
            public List<RowBloque> Bloques { get; set; }
        }

        public class RowBloque
        {
            public int BloqueId { get; set; }
            public int DeporteDeportistaId { get; set; }
            public string Nombre { get; set; }
        }
    }
}
