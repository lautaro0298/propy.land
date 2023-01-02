using LibreriaClases.DTO;
using LibreriaExperto.Desarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class ABMTipoMonedaController : Controller
    {

        // GET: ABMTipoMoneda/Create
        public ActionResult CrearMoneda()
        {
            return View();
        }

        // POST: ABMTipoMoneda/Create
        [HttpPost]
        public ActionResult CrearMoneda(String nombre)
        {
            try
            {
                ABMTipoMoneda.CrearMoneda(nombre);

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: ABMTipoMoneda/Edit/5
     


        public ActionResult EliminarMoneda()
        {
            var tarea = ABMTipoMoneda.traerTipoMoneda();
            List<DTOTipoMoneda> lista = tarea.Item2;
            return View(lista);
        }

        // POST: ABMTipoMoneda/Delete/5
        [HttpPost]
        public ActionResult EliminarMoneda(String id)
        {
            ABMTipoMoneda.EliminarTipoMoneda(id);
            var tarea = ABMTipoMoneda.traerTipoMoneda();
            List<DTOTipoMoneda> lista = tarea.Item2;
            return View(lista);
        }
    }
}
