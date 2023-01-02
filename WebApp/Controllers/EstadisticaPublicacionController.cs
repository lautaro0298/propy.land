using LibreriaClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using LibreriaClases.DTO;
using LibreriaExperto;
using LibreriaExperto.Estadisticas;

namespace WebApp.Controllers
{
    public class EstadisticaPublicacionController : Controller
    {
        // GET: EstadisticaPublicacion
        public ActionResult ConsultarEstadisticasPublicacion(string publicacionId)
        {
            try
            {
                (ErrorPropy error, DTOConsultaEstadisticaPublicacion estadisticas) respuestaObtenerEstadisticas = ExpertoEstadisticas.ConsultarEstadisticasPublicacion(publicacionId);
                
                return View(respuestaObtenerEstadisticas.estadisticas);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ErrorDetalle = ex.Message;
                return View("Error");
                throw;
            }
           
        }
    }
}