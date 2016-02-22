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
using CNSirindar.Models;
using CNSirindar;

namespace SirindarApi.Controllers
{
    public class DeportistaController : ApiController
    {
        private SirindarDbContext db = new SirindarDbContext();

        // GET api/Deportista
        public IQueryable<Deportista> GetDeportistas()
        {
            return db.Deportistas;
        }

        // GET api/Deportista/5
        [ResponseType(typeof(Deportista))]
        public async Task<IHttpActionResult> GetDeportista(int id)
        {
            Deportista deportista = await db.Deportistas.FindAsync(id);
            if (deportista == null)
            {
                return NotFound();
            }

            return Ok(deportista);
        }

        // PUT api/Deportista/5
        public async Task<IHttpActionResult> PutDeportista(int id, Deportista deportista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deportista.DeportistaId)
            {
                return BadRequest();
            }

            db.Entry(deportista).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeportistaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Deportista
        [ResponseType(typeof(Deportista))]
        public async Task<IHttpActionResult> PostDeportista(Deportista deportista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Deportistas.Add(deportista);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = deportista.DeportistaId }, deportista);
        }

        // DELETE api/Deportista/5
        [ResponseType(typeof(Deportista))]
        public async Task<IHttpActionResult> DeleteDeportista(int id)
        {
            Deportista deportista = await db.Deportistas.FindAsync(id);
            if (deportista == null)
            {
                return NotFound();
            }

            db.Deportistas.Remove(deportista);
            await db.SaveChangesAsync();

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

        private bool DeportistaExists(int id)
        {
            return db.Deportistas.Count(e => e.DeportistaId == id) > 0;
        }
    }
}