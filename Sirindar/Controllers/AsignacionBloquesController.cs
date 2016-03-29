using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Sirindar.Core.UnitOfWork;

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
            ViewBag.json =
                new HtmlString(JsonConvert.SerializeObject(GridAsignaciones().Deportistas,
                    new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public JsonResult GetDeportistas(int draw, int start, int length)
        {
            var value = HttpContext.Request.Params["search[value]"];
            var recordsTotal = _unitOfWork.Deportistas.Count();
            var data = _unitOfWork.Deportistas
                .GetAllByExpression(value)
                .Select(d => new
                {
                    DT_RowId = "row_" + d.DeportistaId,
                    DT_RowData = new
                    {
                        pkey = d.DeportistaId
                    },
                    d.Matricula,
                    d.Nombre,
                    d.Apellidos
                }).ToList();
            return Json(new
            {
                draw,
                recordsTotal,
                recordsFiltered = data.Count,
                data
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBloques()
        {
            return Json(_unitOfWork.Bloques.GetAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDeportes(string matricula)
        {
            return Json(_unitOfWork.Deportistas.GetDeportes(matricula), JsonRequestBehavior.AllowGet);
        }

        public TableAsignacionBloques GridAsignaciones()
        {
            var groupDeportistas = _unitOfWork.AsignacionesBloques.GetAsignacionBloquesByMatriucla();

            var tableAsignaciones = new TableAsignacionBloques { Deportistas = new List<RowDeportista>() };

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

        //public class DataTablesParams
        //{
        //    public int draw;
        //    public int start;
        //    public int length;
        //    public search search;
        //    public order[] order;
        //    public column[] columns;
        //}

        //public class order
        //{
        //    public int column;
        //    public string dir;
        //}

        //public class column
        //{
        //    public string data;
        //    public string name;
        //    public bool searchable;
        //    public bool orderable;
        //    public search search;
        //}

        //public class search
        //{
        //    public string value;
        //    public bool regex;
        //}

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
