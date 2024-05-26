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
            var resultado = ABMTipoMoneda.traerTipoMoneda();
            if (!resultado.errorPropy.EsExito)
            {
                ViewBag.Error = resultado.errorPropy.Descripcion;
                return View("Error");
            }

            return View(resultado.dTOTipoMonedas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearPlan(DTOPlan plan)
        {
            if (!ModelState.IsValid)
            {
                return View(plan);
            }

            var errorPropy = ABMPlan.CrearPlan(plan);
            if (!errorPropy.EsExito)
            {
                ModelState.AddModelError("", errorPropy.Descripcion);
                return View(plan);
            }

            return RedirectToAction("EditarPlan");
        }

        [HttpGet]
        public ActionResult EditarPlan()
        {
            var resultado = ABMTipoMoneda.traerTipoMoneda();
            if (!resultado.errorPropy.EsExito)
            {
                ViewBag.Error = resultado.errorPropy.Descripcion;
                return View("Error");
            }

            var resultado2 = ABMPlan.traerPlan();
            if (!resultado2.errorPropy.EsExito)
            {
                ViewBag.Error = resultado2.errorPropy.Descripcion;
                return View("Error");
            }

            return View(resultado2.dTOPlans);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPlan(DTOPlan plan)
        {
            if (!ModelState.IsValid)
            {
                return View(plan);
            }

            var errorPropy = ABMPlan.EditarPlan(plan);
            if (!errorPropy.EsExito)
            {
                ModelState.AddModelError("", errorPropy.Descripcion);
                return View(plan);
            }

            return RedirectToAction("EditarPlan");
        }

        [HttpGet]
        public ActionResult EliminarPlan()
        {
            var tarea = ABMPlan.traerPlan();
            if (!tarea.errorPropy.EsExito)
            {
                ViewBag.Error = tarea.errorPropy.Descripcion;
                return View("Error");
            }

            return View(tarea.dTOPlans);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarPlan(string id)
        {
            var errorPropy = ABMPlan.EliminarPlan(id);
            if (!errorPropy.EsExito)
            {
                ViewBag.Error = errorPropy.Descripcion;
                return View("Error");
            }

            var tarea = ABMPlan.traerPlan();
            if (!tarea.errorPropy.EsExito)
            {
                ViewBag.Error = tarea.errorPropy.Descripcion;
                return View("Error");
            }

            return View(tarea.dTOPlans);
        }
    }
}
