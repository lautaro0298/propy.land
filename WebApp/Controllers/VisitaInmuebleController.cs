using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LibreriaClases;
using LibreriaClases.DTO;
using LibreriaExperto.Seguridad;
using LibreriaExperto.Visitas;

namespace WebApp.Controllers
{
    public class VisitaInmuebleController : Controller
    {
        public async Task<ActionResult> ObtenerDatosVisitante(string publicacionId,string usuarioId) {
            try
            {
                if (!ControlAcceso.Autorizacion(Session["IDUsuario"])) { return RedirectToAction("Login", "Usuario", null); }
                (ErrorPropy error, DTOContactoVisitante datosVisitante) respuestaObtenerContactoVisitante = await ExpertoVisitas.ObtenerContactoVisitante(usuarioId, Session["IDUsuario"].ToString(),publicacionId);
                if (respuestaObtenerContactoVisitante.error.codigoError==-1) {
                    ViewBag.Error = respuestaObtenerContactoVisitante.error.descripcionError;
                    
                    return View(respuestaObtenerContactoVisitante.datosVisitante);
                }
                if (respuestaObtenerContactoVisitante.error.codigoError!=-1 && respuestaObtenerContactoVisitante.error.codigoError!=0) {
                    throw new Exception(respuestaObtenerContactoVisitante.error.descripcionError);
                }
                return View(respuestaObtenerContactoVisitante.datosVisitante);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
                
            }
        }
        public async Task<ActionResult> ObtenerDatosPublicante(string publicacionId) {
            
            try
            {
                if (!ControlAcceso.Autorizacion(Session["IDUsuario"])) { return RedirectToAction("Login", "Usuario", null); }
                (ErrorPropy error, DTOContactoPublicante datosPublicante) respuestaObtenerContactoPublicante =await ExpertoVisitas.ObtenerContactoPublicanteAsync(publicacionId, Session["IDUsuario"].ToString(),1);
                if (respuestaObtenerContactoPublicante.error.codigoError!=0) {
                    throw new Exception(respuestaObtenerContactoPublicante.error.descripcionError);
                }
                return View(respuestaObtenerContactoPublicante.datosPublicante);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
                throw;
            }
        }
        // GET: VisitaInmueble
        public  ActionResult VisitarPublicacion(string publicacionId)
        {
            try
            {
                //if (!ControlAcceso.Autorizacion(Session["IDUsuario"])) { return RedirectToAction("Login", "Usuario", null); }
                (ErrorPropy error, DTOVisitaInmueble datosInmueble) respuestaObtenerInmueble =  ExpertoVisitas.VisitarInmueble(publicacionId);
                //if (respuestaObtenerInmueble.error.codigoError!=0) {
                //    throw new Exception(respuestaObtenerInmueble.error.descripcionError);
                //}
                
                return View(respuestaObtenerInmueble.datosInmueble);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
                
            }
            

           
        }
    }
}