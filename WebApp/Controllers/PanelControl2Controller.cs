using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibreriaExperto.Seguridad;

namespace WebApp.Controllers
{
    public class PanelControl2Controller : Controller
    {
        // GET: PenlControl2
        public ActionResult Index()
        {
            if (!ControlAcceso.Autorizacion(Session["IDUsuario"])) {
                return RedirectToAction("Index","Home",null);
            }
            return View();
        }
    }
}