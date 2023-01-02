using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Servicios;
using WebApp.DTO;
using WebApp.Experto;
namespace WebApp.Controllers
{
    public class SolicitarEstadisticaPublicacionController : Controller
    {
        ExpertoSolicitarEstadisticasInmueble solicitarEstadisticasInmuebleExperto = new ExpertoSolicitarEstadisticasInmueble();
        //EstadisticasServicios serviciosEstadistica = new EstadisticasServicios();
        // GET: SolicitarEstadisticaPublicacion
        public ActionResult Index(Guid publicacionId)
        {
            //DTOListadoEstadisticas dto = serviciosEstadistica.ConsultarEstadisticas(publicacionId);
            DTOListadoEstadisticas dto = solicitarEstadisticasInmuebleExperto.SolicitarEstadisticasInmueble(publicacionId);
            if (dto.listadoDTOEstadistica.Count==0) {
                ViewBag.Error = "No se han encontrado estadísticas para esta publicación";
            }
            return View(dto);
        }
    }
}