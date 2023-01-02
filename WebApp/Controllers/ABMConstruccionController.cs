using LibreriaClases.DTO;
using LibreriaExperto.Desarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class ABMConstruccionController : Controller
    {
        // GET: ABMTipoMoneda/Create
        public ActionResult CrearConstruccion()
        {
            return View();
        }

        // POST: ABMTipoMoneda/Create
        [HttpPost]
        public ActionResult CrearConstruccion(String nombre)
        {
            try
            {
                ABMConstruccion.CrearMoneda(nombre);

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: ABMTipoMoneda/Edit/5



        public ActionResult EliminarConstruccion()
        {
            var tarea = ABMConstruccion.traerTipoConstruccion();
            List<DTOTipoConstruccion> lista = tarea.Item2;
            return View(lista);
        }

        // POST: ABMTipoMoneda/Delete/5
        [HttpPost]
        public ActionResult EliminarConstruccion(String id)
        {
            ABMConstruccion.EliminarTipoConstruccion(id);
            var tarea = ABMConstruccion.traerTipoConstruccion();
            List<DTOTipoConstruccion> lista = tarea.Item2;
            return View(lista);
        }
    }
}