using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.Servicios;

namespace WebApp.Controllers
{
    public class TipoPublicacionController : Controller
    {
        private TipoPublicacionServicios servicios;
        public TipoPublicacionController() {
            servicios = new TipoPublicacionServicios();
        }
        // GET: TipoPublicacion
        public ActionResult Index()
        {
            var data=servicios.ListarTipoPublicacionesSimple();
           
            return Json(data, JsonRequestBehavior.AllowGet);

        }
    }
}