using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.Servicios;
namespace WebApp.Controllers
{
    public class TipoUserController : Controller
    {
        private TipoUserServicios servicios;
        public TipoUserController() {
            servicios = new TipoUserServicios();
        }
        // GET: TipoUser
        public ActionResult Index()
        {
            var data = servicios.ListarTipoUsers();
            return PartialView(data);
            
        }
    }
}