using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibreriaClases;
using LibreriaClases.DTO;
using LibreriaExperto.Desarrollo;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ABMTipoPropiedadTipoAmbienteController : Controller
    {
        // GET: ABMTipoPropiedadTipoAmbiente
        //[Authorize]
        [HttpGet]
        public ActionResult AsignarTipoPropiedadTipoAmbiente()
        {
            (ErrorPropy errorPropy, List<DTOTipoAmbiente> ListaDTOTipoAmbiente) respuesta = ABMTipoAmbiente.TraerTipoAmbiente();
            (ErrorPropy errorPropy, List<DTOTipoPropiedad> ListaDTOTIpoPropiedad) respuesta2 = ABM_TipoPropiedad.TraerTipoPropiedad();

            try
            {
                if(respuesta.errorPropy.codigoError != 0)
                {
                    throw new Exception(respuesta.errorPropy.descripcionError);
                }
                else
                {
                    if(respuesta2.errorPropy.codigoError != 0)
                    {
                        throw new Exception(respuesta2.errorPropy.descripcionError);
                    }
                }
            }
            catch (Exception error)
            {
                ViewBag.Error = error.Message;
                return View("Error");
            }

            DTO_TP_Y_TA dTO_TP_Y_TA = new DTO_TP_Y_TA();
            dTO_TP_Y_TA.dTOTipoAmbientes = respuesta.ListaDTOTipoAmbiente;
            dTO_TP_Y_TA.dTOTipoPropiedades = respuesta2.ListaDTOTIpoPropiedad;

            return View(dTO_TP_Y_TA);
        }

        [HttpPost]
        public JsonResult AsignarTipoPropiedadTipoAmbiente(string TipoPropiedad,string [] TipoAmbiente)
        {

            ErrorPropy errorPropy = ABMTipoPropiedadTipoAmbiente.Crear_y_Asignar_Ambiente_A_Propiedad(TipoPropiedad, TipoAmbiente);

            string result = "";

            try
            {
                if (errorPropy.codigoError != 0)
                {
                    result = "Error";
                    throw new Exception(errorPropy.descripcionError);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            result = "OK";

            return Json(result,JsonRequestBehavior.AllowGet); 
        }

        [HttpGet]
        public ActionResult EliminarTipoPropiedadTipoAmbiente() {

            (ErrorPropy errorPropy,List<DTOTipoPropiedadTipoAmbiente> DTOTipoPropiedadTipoAmbiente) resultado = ABMTipoPropiedadTipoAmbiente.TraerTipoPropiedadTipoAmbiente();

            try
            {
                if(resultado.errorPropy.codigoError != 0)
                {
                    throw new Exception(resultado.errorPropy.descripcionError);
                }
            }
            catch (Exception error)
            {
                ViewBag.Error = error.Message;
                return View("Error");
            }

            return View(resultado.DTOTipoPropiedadTipoAmbiente);
        }
    }
}