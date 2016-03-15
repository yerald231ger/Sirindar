using System.Web.Mvc;
using System.Web.UI;
using Sirindar.Core.UnitOfWork;


namespace Sirindar.Controllers
{
    public class ValidationsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValidationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public JsonResult MatriculaExist(string matricula)
        {
            return Json(!_unitOfWork.Deportistas.IsMatricula(matricula), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsDeporte(int deporteid)
        {
            return Json(!_unitOfWork.Deportes.IsDeporte(deporteid), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsDependencia(int dependenciaid)
        {
            return Json(!_unitOfWork.Dependencias.IsDependencia(dependenciaid), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsDeportista(int deportistaid)
        {
            return Json(!_unitOfWork.Deportistas.IsDeportista(deportistaid), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsBloque(int bloqueid)
        {
            return Json(!_unitOfWork.Bloques.IsBloque(bloqueid), JsonRequestBehavior.AllowGet);
        }
    }
}