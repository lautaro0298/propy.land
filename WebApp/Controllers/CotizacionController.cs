using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Experto;
using LibreriaExperto.Comunicaciones_Externas;
using LibreriaClases.DTOJSon;

namespace WebApp.Controllers
{
    public class CotizacionController : Controller
    {
        // GET: Cotizacion
        public JsonResult CrearCotizacion()
        {
            var obj = APICotizacionToday.GetCotizacionAsync();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public void ActualizarCotizacion()
        {
          //  ExpertoCotizacion.ActualizarCotizacion();
        }
    }
}