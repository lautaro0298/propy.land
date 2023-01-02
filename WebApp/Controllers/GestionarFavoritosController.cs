using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Experto;
using WebApp.DTO;
using LibreriaClases;
using LibreriaExperto.Usuarios;
using LibreriaExperto.Seguridad;

namespace WebApp.Controllers
{
    public class GestionarFavoritosController : Controller
    {
        
        // GET: GestionarFavoritos
        [HttpGet]
        public ActionResult Index()
        {
            string usuarioId = User.Identity.GetUserId();
            List<DTOFavorito>listadoFavoritos=GestionarFavoritosExperto.ListarFavoritos(usuarioId);
            return View(listadoFavoritos);
        }
        public ActionResult QuitarFavorito(Guid publicacionId) {
            try
            {
                string userId = User.Identity.GetUserId();

                DTOError error = GestionarFavoritosExperto.QuitarFavorito(userId,publicacionId);
                switch (error.codigoError) {
                    case 0:
                        return Json("OK");
                    default:
                        return Json("NOOK");

                }
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index", "Error", new { codError = ex.HResult, descripcionError = ex.Message });
            }
        }
        [HttpPost]
        public ActionResult AgregarFavorito(Guid publicacionId) {
            try
            {
                string userId = User.Identity.GetUserId();
                
                
                DTOError error = GestionarFavoritosExperto.AgregarFavorito(userId, publicacionId);
                switch (error.codigoError) {
                    case 0:
                        return Json("OK");
                        
                    case 1:
                        return Json("YAEXISTE");
                    default:
                        return Json("NOOK");
                        

                }
                
                
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index", "Error", new { codError = ex.HResult, descripcionError = ex.Message });
            }
            
        }
    }
}