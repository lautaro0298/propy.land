using LibreriaClases;
using LibreriaClases.DTO;
using LibreriaExperto.Seguridad;
using LibreriaMercadoPago.Comunicacion_MercadoPago;
using LibreriaMercadoPago.Credito;
using LibreriaMercadoPago.Interface_Compra;
using MercadoPago;
using MercadoPago.Resources;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Servicios;

namespace WebApp.Controllers
{
    public class BuyCreditsController : Controller
    {
        // GET: ComprarCredito
        [HttpGet]
        public ActionResult BuyCredits()
        {
            try
            {
                if (!ControlAcceso.Autorizacion(Session["IDUsuario"])) { return RedirectToAction("Index", "Home", null); }

                var user = Session["IDUsuario"].ToString();

                Compra<DTOCredito> compra = new ExpertoComprarCreditos<DTOCredito>();
                var Result = compra.ObtenerPlanes_o_Credito();

                var Compras = compra.ArmarDTOCompra(user, Result.Item2);

                return View(Compras.Item2);

            }catch(Exception error)
            {
                ViewBag.Error = error.Message;
                ViewBag.detalleError = error.StackTrace;
                return View("Error");
            }
        }

        [GETEndpoint("BuyCredits/Notification")]
        public ActionResult Notification(string payment_id, string preference_id)//por este metodo entra la IPN de mercadopago y se reciben los QueryString
        {
            try
            {
                ExpertoNotification Notification = new ExpertoNotification();
                (DTONotification dTONotification, Preference preference, var Pago) = Notification.ProcesarEstado(payment_id, preference_id);

                if (dTONotification.Aprobado == true)
                {
                    ExpertoCredito expertoCredito = new ExpertoCredito();
                    expertoCredito.Reload_credits(preference,Pago);
                }
                return RedirectToAction("Notification", "PaymentNotification", dTONotification);
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
