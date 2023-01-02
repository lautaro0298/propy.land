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
    public class ABMPlanController : Controller
    {
        // GET: ABMPlan
        [HttpGet]
        public ActionResult CrearPlan()
        {
            (ErrorPropy errorPropy, List<DTOTipoMoneda> dTOTipoMonedas) resultado = ABMTipoMoneda.traerTipoMoneda();
            try
            {
                if (resultado.errorPropy.codigoError != 0) throw new Exception(resultado.errorPropy.descripcionError);
            }
            catch (Exception error)
            {
                ViewBag.Error = error.Message;
                return View("Error");
            }

            return View(resultado.dTOTipoMonedas);
        }

        [HttpPost]
        public JsonResult CrearPlan(string NombrePlan, string TipoMoneda, bool PermitirVideo, bool PermitirEstadisticas, int Precio, int CantCreditos , int CantImage )
        {
            DTOPlan dTOPlan = new DTOPlan();

            dTOPlan.nombrePlan = NombrePlan;
            dTOPlan.precioPlan = Precio;
            dTOPlan.tipoMonedaId = TipoMoneda;
            dTOPlan.cantidadCreditosIniciales = CantCreditos;
            dTOPlan.cantidadMaxImagenesPermitidasPorPublicacion = CantImage;
            dTOPlan.permiteVideo = PermitirVideo;
            dTOPlan.accesoEstadisticasAvanzadas = PermitirEstadisticas;

            ErrorPropy errorPropy = ABMPlan.CrearPlan(dTOPlan);

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
        public ActionResult EditarPlan()
        {
            (ErrorPropy errorPropy, List<DTOTipoMoneda> dTOTipoMonedas) resultado = ABMTipoMoneda.traerTipoMoneda();
            (ErrorPropy error1, List<DTOPlan> dTOPlans) resultado2 = ABMPlan.traerPlan();

            try
            {
                if (resultado.errorPropy.codigoError != 0) throw new Exception(resultado.errorPropy.descripcionError);
            }
            catch (Exception error)
            {
                ViewBag.Error = error.Message;
                return View("Error");
            }

            return View( resultado2.dTOPlans) ;
        
        }
        [HttpPost]
        public JsonResult EditarPlan(string NombrePlan,string planId, string TipoMoneda, int Precio , int CantCreditos , int CantImage, bool PermitirVideo = false, bool PermitirEstadisticas=false)
        {
            DTOPlan dTOPlan = new DTOPlan();
            dTOPlan.planId = planId;
            dTOPlan.nombrePlan = NombrePlan;
            dTOPlan.precioPlan = Precio;
            dTOPlan.tipoMonedaId = TipoMoneda;
            dTOPlan.cantidadCreditosIniciales = CantCreditos;
            dTOPlan.cantidadMaxImagenesPermitidasPorPublicacion = CantImage;
            dTOPlan.permiteVideo = PermitirVideo;
            dTOPlan.accesoEstadisticasAvanzadas = PermitirEstadisticas;

            ErrorPropy errorPropy = ABMPlan.EditarPlan(dTOPlan);

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
        public ActionResult EliminarPlan()
        {
            var tarea = ABMPlan.traerPlan();
            List<DTOPlan> lista = tarea.Item2;
            return View(lista);
        }
        [HttpPost]
        public ActionResult EliminarPlan(String id)
        {
            ABMPlan.EliminarPlan(id);
            
            var tarea = ABMPlan.traerPlan();
            List <DTOPlan> lista = tarea.Item2;
            return View(lista);
        }
    }
}