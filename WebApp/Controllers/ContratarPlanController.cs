using System;
using System.Web.Mvc;
using LibreriaExperto.Seguridad;
using LibreriaClases;
using LibreriaClases.DTO;
using System.Net.Http;
using MercadoPago;
using LibreriaMercadoPago.Comunicacion_MercadoPago;
using LibreriaMercadoPago.Interface_Compra;
using LibreriaMercadoPago.Plan;
using System.Collections.Generic;
using MercadoPago.Resources;

namespace WebApp.Controllers
{
    public class ContratarPlanController : Controller
    {
        [AllowAnonymous]
        [Authorize]
        public ActionResult Index()
        {
            try
            {
                if (!ControlAcceso.Autorizacion(Session["IDUsuario"])) { return RedirectToAction("Usuario", "Login", null); }

                var user = Session["IDUsuario"].ToString();

                Compra<DTOPlan> compra = new ExpertoContratarPlan<DTOPlan>();

                ExpertoPlan expertoPlan = new ExpertoPlan();

                var PlanExistente = expertoPlan.YaTienePlan(user);

                var Result = compra.ObtenerPlanes_o_Credito();

                if (PlanExistente == false)
                {
                    var Compras = compra.ArmarDTOCompra(user, Result.Item2);

                    return View(Compras.Item2);
                }
                else
                {
                    return RedirectToAction("ConsultarPlan", "GestionarPlan");
                }
            }
            catch (Exception error)
            {
                ViewBag.Error = error.Message;
                return View("Error");
            }
        }
        [GETEndpoint("ContratarPlan/Notification")]
        public ActionResult Notification(string payment_id, string preference_id)//por este metodo entra la IPN de mercadopago y se reciben los QueryString
        {
            try
            {
                ExpertoNotification Notification = new ExpertoNotification();
                (DTONotification dTONotification, Preference preference, var Pago)  = Notification.ProcesarEstado(payment_id, preference_id);

                if (dTONotification.Aprobado == true)
                {
                    ExpertoPlan expertoPlan = new ExpertoPlan();
                    expertoPlan.AsignarPlan(preference,Pago);
                }
                return View("~/Views/PaymentNotification/Notification.cshtml", dTONotification);
            }
            catch (Exception error)
            {
                ViewBag.Error = error.Message;
                ViewBag.detalleError = error.StackTrace;
                return View("Error");
            }
        }
    }
}