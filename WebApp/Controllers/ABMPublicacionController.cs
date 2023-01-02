using LibreriaClases.DTO;
using LibreriaExperto.Desarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class ABMPublicacionController : Controller
    {
        // GET: ABMPublicacion
        public ActionResult CrearPublicacion()
        {
            return View();
        }

        // POST: ABMTipoMoneda/Create
        [HttpPost]
        public ActionResult CrearPublicacion(String nombre)
        {
            ABMTipoPublicacion.CrearTipoPublicacion(nombre);
            try
            {


                return View();
            }
            catch
            {
                return View();
            }
        }



        [HttpGet]

        public ActionResult EliminarPublicacion()
        {
            var tarea = ABMTipoPublicacion.traerTipoPublicacion();
            List<DTOTipoPublicacion> lista = tarea.Item2;
            return View(lista);
        }


        [HttpPost]
        public ActionResult EliminarPublicacion(String id)
        {
            ABMTipoPublicacion.EliminarTipoPublicacion(id);
            var tarea = ABMTipoPublicacion.traerTipoPublicacion();
            List<DTOTipoPublicacion> lista = tarea.Item2;
            return View(lista);
        }
    }
}