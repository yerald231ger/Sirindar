using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Sirindar.Core;
using Sirindar.Core.UnitOfWork;
using Sirindar.Helpers;

namespace Sirindar.Controllers
{
    [Authorize(Roles="Admin")]
    public class HorariosController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HorariosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: /Horarios/
        public ActionResult Index()
        {
            ViewBag.json = new HtmlString(JsonConvert.SerializeObject(_unitOfWork.Horarios.GetAll(), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            return View();
        }

        // GET: /Horarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var horario = _unitOfWork.Horarios.Get(id.Value);
            if (horario == null)
                return HttpNotFound();

            ViewBag.Nombre = new SelectList(SirindarControls.EnumAsList<ComidasDia>(), "Value", "Text", (int)horario.Nombre);
            return PartialView(horario);
        }

        // POST: /Horarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="HorarioId,Inicia,Finaliza,FechaAlta,Nombre")] Horario model)
        {
            if (ModelState.IsValid)
            {
                var horario = _unitOfWork.Horarios.Get(model.HorarioId);
                horario.Inicia = model.Inicia;
                horario.Finaliza = model.Finaliza;
                horario.Nombre = model.Nombre;
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            ViewBag.Nombre = new SelectList(SirindarControls.EnumAsList<ComidasDia>(), "Value", "Text", model.HorarioId);
            return View(model);
        }       

      
    }
}
