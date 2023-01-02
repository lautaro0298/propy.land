using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.DTO;

namespace WebApp.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(int codError,string descripcionError)
        {
            DTOError error = new DTOError();
            error.codigoError = codError;
            error.descripcionError.Add(descripcionError);
            return View(error);
        }
    }
}