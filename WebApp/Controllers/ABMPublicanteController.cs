using LibreriaClases.DTO;
using LibreriaExperto.Desarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class ABMPublicanteController : Controller
    {
        // GET: ABMPublicante
        // GET: ABMTipoMoneda/Create
        public ActionResult CrearPublicante()
        {
            return View();
        }

        // POST: ABMTipoMoneda/Create
        [HttpPost]
        public ActionResult CrearPublicante(String nombre)
        {
            try
            {

                ABMPublicante.CrearMoneda(nombre);
                return View();
            }
            catch
            {
                return View();
            }
        }

        

        [HttpGet]

        public ActionResult EliminarPublicante()
        {
            var tarea = ABMPublicante.traerPublicante() ;
            List<DTOTipoPublicante> lista = tarea.Item2;
            return View(lista);
        }

       
        [HttpPost]
        public ActionResult EliminarPublicante(String id)
        {
            ABMPublicante.EliminarPublicante(id);
            var tarea = ABMPublicante.traerPublicante();
            List<DTOTipoPublicante> lista = tarea.Item2;
            return View(lista);
        }
    }
}