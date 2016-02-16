using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Sirindar.Helpers;
using CNSirindar.Models;
using CNSirindar.Repositories;

namespace Sirindar.Controllers
{
    [Authorize(Roles="Admin")]
    public class HorariosController : Controller
    {
        private IRepository<Horario,int> _horario;

        public HorariosController(
            IRepository<Horario, int> horario
            ) 
        {
            this._horario = horario;
        }

        // GET: /Horarios/
        public ActionResult Index()
        {
            ViewBag.json = new HtmlString(JsonConvert.SerializeObject(_horario.List(), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            return View();
        }

        // GET: /Horarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var horario = _horario.Read(id);
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
        public ActionResult Edit([Bind(Include="HorarioId,Inicia,Finaliza,FechaAlta,Nombre")] Horario horario)
        {
            if (ModelState.IsValid)
            {
                _horario.Update(horario);
                return RedirectToAction("Index");
            }
            ViewBag.Nombre = new SelectList(SirindarControls.EnumAsList<ComidasDia>(), "Value", "Text", horario.HorarioId);
            return View(horario);
        }       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _horario = null;
            }
            base.Dispose(disposing);
        }
    }
}
