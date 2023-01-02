using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.Servicios;

namespace WebApp.Controllers
{
    public class ExtrasController : Controller
    {
        private ExtrasServicios servicios;
        public ExtrasController() {
            servicios = new ExtrasServicios();
        }
        // GET: Extras
        public ActionResult ListarExtras()
        {
            var data = servicios.ListarExtras();
            return PartialView(data);
                
        }
    }
}