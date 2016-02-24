using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CNSirindar.Repositories;
using CNSirindar.Models;
using CNSirindar;

namespace SirindarApi.Controllers
{
    public class DeportistaController : ApiController
    {

        private IRepository<Deportista, int> deportistaR;
        private SirindarDbContext db = new SirindarDbContext();

        public DeportistaController(IRepository<Deportista,int> deportistaR) 
        {
            this.deportistaR = deportistaR;
        }

        // GET api/Deportista
        public IEnumerable<Deportista> GetDeportistas()
        {
            return deportistaR.List();
        }

        // GET api/Deportista/5
        [ResponseType(typeof(Deportista))]
        public IHttpActionResult GetDeportista(int id)
        {
            var deportista = deportistaR.Read(id);
            if (deportista == null)
            {
                return NotFound();
            }

            return Ok(deportista);
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