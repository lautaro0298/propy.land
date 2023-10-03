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
    public class ABTipoPropiedadTipoCaracteristicaController : Controller
    {
        [HttpGet]
        public ActionResult AsignarTipoPropiedadTipoCaracteristica()
        {
            (ErrorPropy errorPropy, List<DTOCaracteristica> ListaDTOCaracteristica) respuesta = ABMCaracteristica.TraerCaracteristicas();
            (ErrorPropy errorPropy, List<DTOTipoPropiedad> ListaDTOTIpoPropiedad) respuesta2 = ABM_TipoPropiedad.TraerTipoPropiedad();

            try
            {
                if (respuesta.errorPropy.codigoError != 0)
                {
                    throw new Exception(respuesta.errorPropy.descripcionError);
                }
                else
                {
                    if (respuesta2.errorPropy.codigoError != 0)
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

            (List<DTOTipoPropiedad>, List<DTOCaracteristica>) dTO_TP_Y_C;
            dTO_TP_Y_C.Item2 = respuesta.ListaDTOCaracteristica;
            dTO_TP_Y_C.Item1 = respuesta2.ListaDTOTIpoPropiedad;

            return View(dTO_TP_Y_C);
        }
        [HttpPost]
        public ActionResult AsignarTipoPropiedadTipoCaracteristica(string[] TipoPropiedad, string[] caracteristica)
        {
            // Verifica si TipoPropiedad y caracteristica son null o vacíos y maneja los errores si es necesario.

            try
            {
                
                        ABMTipoPropiedadcaracteristica.Crear_y_Asignar_Caracteristicas_A_Propiedades(TipoPropiedad, caracteristica);
                  
                

                // Tu código para obtener las listas de TipoPropiedad y Caracteristica aquí...

                return (AsignarTipoPropiedadTipoCaracteristica());
            }
            catch (Exception error)
            {
                ViewBag.Error = error.Message;
                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult EliminarTipoPropiedadTipoCaracteristica()
        {
            var respuesta2 = ABMTipoPropiedadcaracteristica.TraerTipoPropiedadcaracteristica();
            return View(respuesta2.Item2);
        }
        [HttpPost]
        public ActionResult EliminarTipoPropiedadTipoCaracteristica(string id)
        {
            var respuesta2 = ABMTipoPropiedadcaracteristica.EliminarTipoPropiedadCaracteristica(id);
            return View(respuesta2.Item2);
        }

    }
}