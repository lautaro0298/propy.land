using LibreriaClases.DTO;
using LibreriaExperto.Desarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class ABMCaracteristicaController : Controller
    {

        // GET: ABMCaracteristica/Create
        public ActionResult Crear()
        {
            return View();
        }

        // POST: ABMCaracteristica/Create
        [HttpPost]
        public ActionResult Crear(String nombre)
        {
            try
            {
                ABMCaracteristica.CrearCaracteristica(nombre);

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: ABMCaracteristica/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ABMCaracteristica/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult Eliminar()
        {
           var tarea= ABMCaracteristica.TraerCaracteristicas();
            List<DTOCaracteristica> lista = tarea.Item2;
            return View(lista);
        }

        // POST: ABMCaracteristica/Delete/5
        [HttpPost]
        public ActionResult Eliminar(String id)
        {
            ABMCaracteristica.EliminarCaracteristica(id);
            var tarea = ABMCaracteristica.TraerCaracteristicas();
            List<DTOCaracteristica> lista = tarea.Item2;
            return View(lista);
        }
    }
}
