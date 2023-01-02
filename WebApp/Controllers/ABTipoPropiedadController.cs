using LibreriaClases;
using LibreriaClases.DTO;
using LibreriaExperto.Desarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class ABTipoPropiedadController : Controller
    {
        // GET: ABTipoPropiedad
        [HttpGet]
        public ActionResult CrearTipoPropiedad()
        {
            (ErrorPropy errorPropy, List<DTOTipoAmbiente> dTOTipoAmbientes) resultado = ABMTipoAmbiente.TraerTipoAmbiente();
            (ErrorPropy errorPropy, List<DTOCaracteristica> dTOCaracteristicas) resultado2 = ABMCaracteristica.TraerCaracteristicas();

            try
            {
                if (resultado.errorPropy.codigoError != 0)
                {
                    throw new Exception(resultado.errorPropy.descripcionError);
                }
                else
                {
                    if (resultado2.errorPropy.codigoError != 0)
                    {
                        throw new Exception(resultado2.errorPropy.descripcionError);
                    }
                }
            }
            catch (Exception error)
            {
                ViewBag.Error = error.Message;
                return View("Error");
            }

            DTO_C_Y_A dTO_C_Y_A = new DTO_C_Y_A();
            dTO_C_Y_A.dTOCaracteristicas = resultado2.dTOCaracteristicas;
            dTO_C_Y_A.dTOTipoAmbientes = resultado.dTOTipoAmbientes;


            return View(dTO_C_Y_A);
        }
        [HttpPost]
        public JsonResult CrearTipoPropiedad(String NombreTipoPropiedad, String TipoAmbiente, DTO_C_Y_A data)
        {
            List<String> lista = new List<string>();
            for (int x = 0; x < data.dTOCaracteristicas.Count; x++) {
                if (data.dTOCaracteristicas.ElementAt(x).ischeck)
                {
                    lista.Add(data.dTOCaracteristicas.ElementAt(x).nombreCaracteristica);
                } }
            ErrorPropy errorPropy = ABM_TipoPropiedad.CrearTipoPropiedad(NombreTipoPropiedad, lista);
            string result = "";

            try
            {
                if (errorPropy.codigoError != 0)
                {
                    result = "Error";
                    throw new Exception(errorPropy.descripcionError);
                }
            }
            catch (Exception error)
            {
                ViewBag.Error = error.Message;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            result = "OK";

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult EliminarTipoPropiedad()
        {
            (ErrorPropy, List<DTOTipoPropiedad>) resultado = ABM_TipoPropiedad.TraerTipoPropiedad();
            List<DTOTipoPropiedad> resultado2 = resultado.Item2;
            return View(resultado2);
        }
        [HttpPost]
        public ActionResult EliminarTipoPropiedad(String id)
        {
            ErrorPropy errorPropy = ABM_TipoPropiedad.EliminarTipoPropiedad(id);
            string result = "";

            try
            {
                if (errorPropy.codigoError != 0)
                {
                    result = "Error";
                    throw new Exception(errorPropy.descripcionError);
                }
            }
            catch (Exception error)
            {
                ViewBag.Error = error.Message;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            result = "OK";
            (ErrorPropy, List<DTOTipoPropiedad>) resultado = ABM_TipoPropiedad.TraerTipoPropiedad();
            List<DTOTipoPropiedad> resultado2 = resultado.Item2;
            return View(resultado2);
        }

    }
    
    
}
