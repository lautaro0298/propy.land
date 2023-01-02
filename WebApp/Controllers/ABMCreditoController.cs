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
    public class ABMCreditoController : Controller
    {
        [HttpGet]
        public ActionResult CrearPaqueteCredito()
        {
            (ErrorPropy errorPropy, List<DTOTipoMoneda> dTOTipoMoneda) resultado = ABMTipoMoneda.traerTipoMoneda();
            try
            {
                if (resultado.errorPropy.codigoError != 0) throw new Exception(resultado.errorPropy.descripcionError);
            }
            catch (Exception error)
            {
                ViewBag.Error = error.Message;
                return View("Error");
            }

            return View(resultado.dTOTipoMoneda);
        }
        [HttpPost]
        public ActionResult CrearPaqueteCredito(string nombredelpack, int cantidad, decimal Precio, string TipoMoneda)
        {
          var resultado= ABMPaquete.CrearPaquete(nombredelpack, cantidad, TipoMoneda, Precio);

            (ErrorPropy errorPropy, List<DTOTipoMoneda> dTOTipoMonedas) resultado1 = ABMTipoMoneda.traerTipoMoneda();
            try
            {
                if (resultado1.errorPropy.codigoError != 0) throw new Exception(resultado1.errorPropy.descripcionError);
            }
            catch (Exception error)
            {
                ViewBag.Error = error.Message;
                return View("Error");
            }

            return View(resultado1.dTOTipoMonedas);
        }
        [HttpGet]
        public ActionResult EditarPaqueteCredito()
        {
            (ErrorPropy errorPropy, List<DTOCredito> dTOCreditos) resultado = ABMPaquete.traerCredito();
            

            try
            {
                if (resultado.errorPropy.codigoError != 0) throw new Exception(resultado.errorPropy.descripcionError);
            }
            catch (Exception error)
            {
                ViewBag.Error = error.Message;
                return View("Error");
            }

            return View(resultado.dTOCreditos);

        }
        [HttpPost]
        public ActionResult EditarPaqueteCredito(string id , string nombredelpack, int cantidad, decimal Precio, string TipoMoneda)
        {
            DTOCredito dTOCredito = new DTOCredito();
            dTOCredito.PaqueteID = id;
            dTOCredito.Precio = Precio;
            dTOCredito.CantidadCreditos = cantidad;
            dTOCredito.TipoMonedaID = TipoMoneda;
            dTOCredito.NombrePack = nombredelpack;
            
            ABMPaquete.EditarCredito(dTOCredito);

            (ErrorPropy errorPropy, List<DTOCredito> dTOCreditos) resultado = ABMPaquete.traerCredito();


            try
            {
                if (resultado.errorPropy.codigoError != 0) throw new Exception(resultado.errorPropy.descripcionError);
            }
            catch (Exception error)
            {
                ViewBag.Error = error.Message;
                return View("Error");
            }

            return View(resultado.dTOCreditos);


        }
        [HttpGet]

        public ActionResult EliminarCredito()
        {
            var tarea = ABMPaquete.traerCredito();
            List<DTOCredito> lista = tarea.Item2;
            return View(lista);
        }


        [HttpPost]
        public ActionResult EliminarCredito(String id)
        {
            ABMPaquete.EliminarPaquete(id);
            var tarea = ABMPaquete.traerCredito();
            List<DTOCredito> lista = tarea.Item2;
            return View(lista);
        }
    }
    
}
