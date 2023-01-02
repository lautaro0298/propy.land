using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.DTO;
using WebApp.Experto;

namespace WebApp.Controllers
{
    public class GestionarCreditoController : Controller
    {
        // GET: GestionarCredito
        [HttpGet]
        public ActionResult ConsultarCredito()
        {
            DTOCredito dTOCredito = new DTOCredito();
            var user = User.Identity.GetUserId();
            if (user != null)
            {
                ExpertoConsulta expertoConsulta = new ExpertoConsulta();
                dTOCredito = expertoConsulta.ConsultarCredito(user);
            }
            else
            {
                dTOCredito.CreditoActual = 0;
            }
            return View(dTOCredito);
        }
    }
}