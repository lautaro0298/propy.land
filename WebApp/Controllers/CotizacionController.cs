using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Experto;
using LibreriaExperto.Comunicaciones_Externas;
using LibreriaClases.DTOJSon;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class CotizacionController : Controller
    {
        // GET: Cotizacion
        public async Task<JsonResult>  CrearCotizacion()
        {
            var obj = await APICotizacionToday.GetCotizacionAsync();
            
            return Json(obj , JsonRequestBehavior.AllowGet);
        }

        public void ActualizarCotizacion()
        {
          //  ExpertoCotizacion.ActualizarCotizacion();
        }
    }
}