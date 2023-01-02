using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.DTO
{
    public class DTONotification
    {
        public string StatusPayment { get; set; }
        public string NombrePlan { get; set; }
        public HttpStatusCodeResult httpStatusCodeResult { get; set; }
    }
}