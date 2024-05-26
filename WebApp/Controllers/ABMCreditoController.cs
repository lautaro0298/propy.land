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
        private ActionResult DisplayError(Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View("Error");
        }

        private ActionResult DisplayCreditos(List<DTOCredito> creditos)
        {
            if (creditos == null)
            {
                return DisplayError(new Exception("No creditos found."));
            }

            return View(creditos);
        }

        [HttpGet]
        public ActionResult CrearPaqueteCredito()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearPaqueteCredito(string nombredelpack, int cantidad, decimal Precio, string TipoMoneda)
        {
            if (Precio <= 0 || cantidad <= 0)
            {
                return DisplayError(new Exception("Precio and cantidad must be greater than zero."));
            }

            var resultado = ABMPaquete.CrearPaquete(nombredelpack, cantidad, TipoMoneda, Precio);

            return DisplayCreditos(resultado.dTOCreditos);
        }

        [HttpGet]
        public ActionResult EditarPaqueteCredito()
        {
            (ErrorPropy errorPropy, List<DTOCredito> dTOCreditos) resultado = ABMPaquete.traerCredito();

            return DisplayCreditos(resultado.dTOCreditos);
        }

        [HttpPost]
        public ActionResult EditarPaqueteCredito(string id, string nombredelpack, int cantidad, decimal Precio, string TipoMoneda)
        {
            if (string.IsNullOrEmpty(id))
            {
                return DisplayError(new Exception("Invalid id."));
            }

            DTOCredito dTOCredito = new DTOCredito
            {
                PaqueteID = id,
                Precio = Precio,
                CantidadCreditos = cantidad,
                TipoMonedaID = TipoMoneda,
                NombrePack = nombredelpack
            };

            ABMPaquete.EditarCredito(dTOCredito);

            (ErrorPropy errorPropy, List<DTOCredito> dTOCreditos) resultado = ABMPaquete.traerCredito();

            return DisplayCreditos(resultado.dTOCreditos);
        }

        [HttpGet]
        public ActionResult EliminarCredito()
        {
            var tarea = ABMPaquete.traerCredito();
            return DisplayCreditos(tarea.Item2);
        }

        [HttpPost]
        public ActionResult EliminarCredito(String id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return DisplayError(new Exception("Invalid id."));
            }

            ABMPaquete.EliminarPaquete(id);

            var tarea = ABMPaquete.traerCredito();
            return DisplayCreditos(tarea.Item2);
        }
    }
}
