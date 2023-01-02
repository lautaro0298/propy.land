using LibreriaClases.DTO;
using LibreriaExperto.Desarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.Servicios;

namespace WebApp.Controllers
{
    public class TipoAmbienteController : Controller
    {
        private TipoAmbienteServicios servicios;
        public TipoAmbienteController() {
            servicios = new TipoAmbienteServicios();
        }
        // GET: TipoAmbiente
        public ActionResult ListarTiposAmbiente()
        {
            var data = servicios.ListarTipoAmbientes();
            return PartialView(data);
                
        }
        [HttpGet]
        public ActionResult AgregarAmbiente()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AgregarAmbiente(String nombre)
        {
            var tarea = ABMTipoAmbiente.CrearTipoAmbiente(nombre);
            return View();
        }
        [HttpGet]
        public ActionResult EliminarAmbiente()
        {
            var tarea = ABMTipoAmbiente.TraerTipoAmbiente();
            List<DTOTipoAmbiente> lista = tarea.Item2;
            return View(lista);
        }
        [HttpPost]
        public ActionResult EliminarAmbiente(String id)
        {
            ABMTipoAmbiente.EliminarTipoAmbiente(id);
            var tarea = ABMTipoAmbiente.TraerTipoAmbiente();
            List<DTOTipoAmbiente> lista = tarea.Item2;
            return View(lista);
        }
    }
}