using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Experto;
using WebApp.DTO;
using Microsoft.AspNet.Identity;

namespace WebApp.Controllers
{
    [Authorize]
    public class SolicitarDatosInmuebleController : Controller
    {
        // GET: SolicitarDatosInmueble
        public ActionResult Index(Guid publicacionId)
        {
            ExpertoSolicitarDatosInmueble experto = new ExpertoSolicitarDatosInmueble();
            string usuarioId = User.Identity.GetUserId();
            DTOEspecificacionInmueble datos = experto.SolicitarDatosInmueble(publicacionId,usuarioId);
            return View(datos);
        }
    }
}