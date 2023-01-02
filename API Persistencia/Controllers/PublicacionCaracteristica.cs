using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionCaracteristica : Controller
    {
        private ConexionDB con;
        public PublicacionCaracteristica(ConexionDB conexion) {
            con = conexion;
        }
        // GET: PublicacionCaracteristica
        public ActionResult Index()
        {
            return View();
        }

        // GET: PublicacionCaracteristica/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PublicacionCaracteristica/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PublicacionCaracteristica/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PublicacionCaracteristica/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PublicacionCaracteristica/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PublicacionCaracteristica/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PublicacionCaracteristica/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
