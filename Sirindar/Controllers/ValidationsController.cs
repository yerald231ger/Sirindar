using CNSirindar.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;


namespace Sirindar.Controllers
{
    public class ValidationsController : Controller
    {
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public JsonResult MatriculaExist(string matricula)
        {
            return Json(!(GeneralRepository.IsMatricula(matricula)), JsonRequestBehavior.AllowGet);
        }

	}
}