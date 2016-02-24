using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CNSirindar;
using CNSirindar.Models;
using CNSirindar.Repositories;

namespace SirindarApi.Controllers
{
    public class HorariosController : ApiController
    {
        private IRepository<Horario, int> horarioR;
        private SirindarDbContext db = new SirindarDbContext();

        public HorariosController(IRepository<Horario, int> horarioR)
        {
            this.horarioR = horarioR;
        }

        // GET api/Horarios
        public IEnumerable<Horario> GetHorarios()
        {
            return horarioR.List();
        }

        // GET api/Horarios/5
        [ResponseType(typeof(Horario))]
        public IHttpActionResult GetHorario(int id)
        {
            var horario = horarioR.Read(id);
            if (horario == null)
            {
                return NotFound();
            }

            return Ok(horario);
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
    }
}