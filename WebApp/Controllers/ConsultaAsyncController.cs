
using LibreriaClases.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibreriaExperto.ConsultaAsincrona;
using System;
using LibreriaExperto.Seguridad;

namespace WebApp.Controllers
{
    public class ConsultaAsyncController : Controller
    {
        [HttpGet]
        public JsonResult ConsultarCreditoLabel()
        {
            if (ControlAcceso.Autorizacion(Session["IDUsuario"])){
                try
                {
                    var User = Session["IDUsuario"].ToString();

                    DTOCredito dTOCredito = new DTOCredito();


                    ExpertoConsultaAsincrona expertoConsultaAsincrona = new ExpertoConsultaAsincrona();
                    dTOCredito.CreditosActuales = expertoConsultaAsincrona.Consulta_Creditos_Async(User);

                    return Json(dTOCredito, JsonRequestBehavior.AllowGet);
                }
                catch (NullReferenceException)
                {
                    DTOCredito dTOCredito = new DTOCredito();
                    dTOCredito.CreditosActuales = 0;
                    return Json(dTOCredito, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                DTOCredito dTOCredito = new DTOCredito();
                dTOCredito.CreditosActuales = 0;
                return Json(dTOCredito, JsonRequestBehavior.AllowGet);
            }
        }
    }
}