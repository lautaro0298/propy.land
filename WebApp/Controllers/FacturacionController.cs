using LibreriaClases.DTO;
using LibreriaExperto.Seguridad;
using LibreriaMercadoPago.Factura;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class FacturacionController : Controller
    {
        // GET: Facturacion
        public ActionResult ConsultarFacturas()
        {
            DTO_PU_Y_User dTO_PU_Y_User = new DTO_PU_Y_User();
            try
            {
                if (ControlAcceso.Autorizacion(Session["IDUsuario"]))
                {
                    var User = Session["IDUsuario"].ToString();

                    ExpertoFactura expertoFactura = new ExpertoFactura();


                    dTO_PU_Y_User = expertoFactura.ConsultarFacturacion(User);
                }
                else
                {
                    dTO_PU_Y_User.User = null;
                }
            }
            catch (Exception)
            {
                dTO_PU_Y_User.dTOs = null;
                dTO_PU_Y_User.User = null;
            }
            return View(dTO_PU_Y_User);
        }
    }
}