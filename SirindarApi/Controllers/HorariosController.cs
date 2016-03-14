using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Sirindar.Core;
using Sirindar.Core.Repositories;
using Sirindar.Core.UnitOfWork;
using Sirindar.Entity;

namespace SirindarApi.Controllers
{
    [Authorize]
    public class HorariosController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private SirindarDbContext db = new SirindarDbContext();

        public HorariosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/Horarios
        public IEnumerable<Horario> GetHorarios()
        {
            return _unitOfWork.Horarios.GetAll();
        }

        // GET api/Horarios/5
        [ResponseType(typeof(Horario))]
        public IHttpActionResult GetHorario(int id)
        {
            var horario = _unitOfWork.Horarios.Get(id);
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