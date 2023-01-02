using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebApp.Servicios;
using WebApp.Experto;

namespace WebApp.Controllers
{
    public class SolicitarContactoPublicanteController : Controller
    {
        
        ClickPublicacionServicios serviciosClickPublicacion = new ClickPublicacionServicios();
        CorreoServicios serviciosCorreo = new CorreoServicios();
        ExpertoSolicitarDatosPublicante solicitarContactoPublicanteExperto = new ExpertoSolicitarDatosPublicante();
        // GET: SolicitarContactoPublicante
        public async Task<ActionResult> Index(Guid publicacionId)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    
                    //serviciosClickPublicacion.Crear(User.Identity.GetUserId(), publicacionId);
                    await serviciosCorreo.EnviarCorreoAvisoVisualización(User.Identity.GetUserId(), publicacionId);
                    return View(solicitarContactoPublicanteExperto.SolicitarDatosPublicante(User.Identity.GetUserId(), publicacionId));
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                
                
            }
            else {
                return RedirectToAction("Login","Account");
            }
            
        }
    }
}