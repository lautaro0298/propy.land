using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using WebApp.Experto;
using LibreriaExperto.Comunicaciones_Externas;
using LibreriaClases.DTOJSon;

namespace WebApp.Controllers
{
    public class CotizacionController : Controller
    {
        public CotizacionController()
        {
            // Initialize any necessary components here
        }

        // GET: Cotizacion
        [HttpGet]
        public async Task<JsonResult> CrearCotizacion()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://api.cotizaciontoday.com/cotizaciones");
            var content = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<Cotizacion>(content);

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [Obsolete]
        public void ActualizarCotizacion()
        {
            // ExpertoCotizacion.ActualizarCotizacion();
        }
    }
}
