using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Servicios;

namespace WebApp.Controllers
{
    public class TipoConstruccionController : Controller
    {
        private TipoConstruccionServicios servicios;
        public TipoConstruccionController() {
            servicios = new TipoConstruccionServicios();
        }
        // GET: TipoConstruccion
        public ActionResult ListarTipoConstrucciones()
        {
            var data = servicios.ListarTipoConstrucciones();
            return PartialView(data);
        }
    }
}