using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Experto;
using WebApp.DTO;
using Microsoft.AspNet.Identity;

namespace WebApp.Controllers
{
    public class ABMUsuarioController : Controller
    {
        // GET: ABMUsuario
        public ActionResult Index()
        {
            ExpertoABMUsuario expertoABMUsuario = new ExpertoABMUsuario();
            DTOUsuario datos = expertoABMUsuario.PreparDatosParaEdicion(User.Identity.GetUserId());
            return View(datos);
        }
    }
}