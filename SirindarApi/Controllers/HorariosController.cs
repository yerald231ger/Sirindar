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
    public class HorariosController : ApiController
    {
        private SirindarDbContext db = new SirindarDbContext();

        // GET api/Horarios
        public IQueryable<Horario> GetHorarios()
        {
            return db.Horarios;
        }

        // GET api/Horarios/5
        [ResponseType(typeof(Horario))]
        public async Task<IHttpActionResult> GetHorario(int id)
        {
            Horario horario = await db.Horarios.FindAsync(id);
            if (horario == null)
            {
                return NotFound();
            }

            return Ok(horario);
        }

        // PUT api/Horarios/5
        public async Task<IHttpActionResult> PutHorario(int id, Horario horario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != horario.HorarioId)
            {
                return BadRequest();
            }

            db.Entry(horario).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HorarioExists(id))
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

        // POST api/Horarios
        [ResponseType(typeof(Horario))]
        public async Task<IHttpActionResult> PostHorario(Horario horario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Horarios.Add(horario);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = horario.HorarioId }, horario);
        }

        // DELETE api/Horarios/5
        [ResponseType(typeof(Horario))]
        public async Task<IHttpActionResult> DeleteHorario(int id)
        {
            Horario horario = await db.Horarios.FindAsync(id);
            if (horario == null)
            {
                return NotFound();
            }

            db.Horarios.Remove(horario);
            await db.SaveChangesAsync();

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

        private bool HorarioExists(int id)
        {
            return db.Horarios.Count(e => e.HorarioId == id) > 0;
        }
    }
}