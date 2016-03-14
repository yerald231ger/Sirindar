using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Sirindar.Core;
using Sirindar.Core.Repositories;
using Sirindar.Core.UnitOfWork;
using Sirindar.Entity;
using SirindarApi.Models.Remotos;

namespace SirindarApi.Controllers
{
    [Authorize]
    public class DeportistaController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private SirindarDbContext db = new SirindarDbContext();

        public DeportistaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/Deportista/5
        [ResponseType(typeof(Deportista))]
        public IHttpActionResult GetDeportista(int id)
        {
            var deportista = _unitOfWork.Deportistas.GetByMatricula(id.ToString());

            if (deportista == null)
            {
                return NotFound();
            }

            var model = new CafeteriaDeportistaModel
            {
                DeportistaId = deportista.DeportistaId,
                Nombre = deportista.Nombre + " " + deportista.Apellidos,
                Dependencia = new CafeteriaDepenenciaModel
                {
                    Nombre = deportista.Dependencia.Nombre
                }
            };

            return Ok(model);
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