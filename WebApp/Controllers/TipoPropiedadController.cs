
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.Servicios;

namespace WebApp.Controllers
{
    public class TipoPropiedadController : Controller
    {
        private TipoPropiedadServicios servicios;
        public TipoPropiedadController() {
            servicios = new TipoPropiedadServicios();
        }
        // GET: TipoPropiedad
        public ActionResult Index()
        {
            var data = servicios.ListarTipoPropiedades();
            return PartialView(data);
                
        }
        public ActionResult Id( string id)
        {
            var data = servicios.ListarTipoPropiedadesPorId(id);
            return PartialView(data);

        }
    }
}