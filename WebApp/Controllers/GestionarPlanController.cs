using System.Web.Mvc;
using LibreriaMercadoPago.Plan;
using LibreriaExperto.Seguridad;
using System;
using LibreriaClases.DTO;
using MercadoPago.Resources;

namespace WebApp.Controllers
{
    public class GestionarPlanController : Controller
    {
        // GET: GestionarPlan
        public ActionResult ConsultarPlan()
        {
            if (!ControlAcceso.Autorizacion(Session["IDUsuario"])) { return RedirectToAction("Login", "Usuario", null); }

            var user = Session["IDUsuario"].ToString();

            DTOPlan Plan_Del_User = new DTOPlan();

            try
            {
                try
                {
                    ExpertoPlan expertoPlan = new ExpertoPlan();
                    var ConsultarPlan = expertoPlan.ConsultarPlan(user);

                    if (ConsultarPlan.Item1.codigoError != 0)
                    {
                        throw new Exception(ConsultarPlan.Item1.descripcionError);
                    }
                    else
                    {
                        Plan_Del_User = ConsultarPlan.Item2;
                    }
                }
                catch (NullReferenceException)
                {
                    Plan_Del_User.planId = null;
                    return View(Plan_Del_User);
                }
            }catch(Exception error)
            {
                ViewBag.Error = error.Message;
                return View("Error");
            }
            
            return View(Plan_Del_User);
        }
    }
}