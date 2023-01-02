using LibreriaClases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class PaymentNotificationController : Controller
    {
        // GET: PaymentNotification
        public ActionResult Notification(DTONotification dTONotification)
        {
            return View(dTONotification);
        }
    }
}