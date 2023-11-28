using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LibreriaClases;
using LibreriaClases.Transferencia;
using LibreriaExperto.Seguridad;
using LibreriaExperto.Usuarios;
using LibreriaExperto.Mensajeria;
using MercadoPago.DataStructures.Customer;
using LibreriaClases.DTO;
using Microsoft.AspNetCore.Authentication;

using System.Xml;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Security;
using System.IO;
using System.Text;
using WebApp.AFIP.WSAA;
using System.Net;
using System.Net.Sockets;
using System.Xml.Linq;
using System.Web.Http.Cors;
using Microsoft.AspNet.Identity;
using System.Web.Http.Results;
using WebApp.Models;

namespace WebApp.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsuarioController : Controller
    {
        public static string token;
        public static string sign;
        public static DateTime generationTime;
        public static DateTime expirationTime;
        [EnableCors(origins: "*", headers: "*", methods: "*")]

        public ActionResult ConsultarActividad()
        {
            try
            {
                if (!ControlAcceso.Autorizacion(Session["IDUsuario"])) { return RedirectToAction("Login", "Usuario", null); }
                (ErrorPropy error, List<DTOConsultaActividadUsuario> listadoActividades) respuestaConsultarActividad = ExpertoUsuarios.ConsultarActividad(Session["IDUsuario"].ToString());
                return View(respuestaConsultarActividad.listadoActividades);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ErrorDetalle = ex.StackTrace;
                return View("Error");

            }


        }
        [HttpGet]
        public bool ConsultarIsFavorito(string publicacionId)
        {
            try
            {

                if (!ControlAcceso.Autorizacion(Session["IDUsuario"])) { return false; }
                bool respuesta = ExpertoUsuarios.ConsultarIsFavorito(Session["IDUsuario"].ToString(), publicacionId);

                return respuesta;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ErrorDetalle = ex.StackTrace;
                return false;
                throw;
            }
        }
        [HttpGet]
        public JsonResult ConsultarFavoritos()
        {
            try
            {

                if (!ControlAcceso.Autorizacion(Session["IDUsuario"])) { return (null); }
                List<Publicacion> publicacions = new List<Publicacion>();
                List<TransferenciaPublicacion> respuesta = ExpertoUsuarios.ConsultarListaFavoritosPublicacion(Session["IDUsuario"].ToString());

                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ErrorDetalle = ex.StackTrace;
                return null;
                throw;
            }
        }
        [HttpGet]
        public ActionResult ConsultarListaFavoritos()
        {
            try
            {

                if (!ControlAcceso.Autorizacion(Session["IDUsuario"])) { return RedirectToAction("Login", "Usuario", null); }
                (ErrorPropy error, List<DTOFavorito> favoritos) respuesta = ExpertoUsuarios.ConsultarListaFavoritos(Session["IDUsuario"].ToString());

                return View(respuesta.favoritos);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ErrorDetalle = ex.StackTrace;
                return View("Error");
                throw;
            }
        }
        [HttpPost]
        public ActionResult AddFavorito(string publicacionId)
        {
            try
            {
                if (!ControlAcceso.Autorizacion(Session["IDUsuario"])) { return RedirectToAction("Login", "Usuario", null); }
                ErrorPropy error = new ErrorPropy();
                error = ExpertoUsuarios.AgregarFavorito(Session["IDUsuario"].ToString(), publicacionId);
                switch (error.codigoError)
                {
                    case 0:
                        return Json("OK");

                    case -1:
                        return Json("YAEXISTE");
                    default:
                        throw new Exception(error.descripcionError);

                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ErrorDetalle = ex.StackTrace;
                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult EliminarFavorito(string publicacionId)
        {
            try
            {
                if (!ControlAcceso.Autorizacion(Session["IDUsuario"])) { return RedirectToAction("Login", "Usuario", null); }
                ErrorPropy error = ExpertoUsuarios.QuitarFavorito(publicacionId, Session["IDUsuario"].ToString());

                return Json(new { resultado = "OK" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ErrorDetalle = ex.StackTrace;
                return Json(new { resultado = "Error", mensaje = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult DeleteFavorito(string publicacionId)
        {
            try
            {
                if (!ControlAcceso.Autorizacion(Session["IDUsuario"])) { return RedirectToAction("Login", "Usuario", null); }
                ErrorPropy error = ExpertoUsuarios.QuitarFavorito(publicacionId, Session["IDUsuario"].ToString());

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ErrorDetalle = ex.StackTrace;
                return View("Error");
            }
            return RedirectToAction("ConsultarListaFavoritos");
        }
        public ActionResult ReenvioMailVerificacion()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ReenvioMailVerificacion(string email)
        {
            ErrorPropy error = new ErrorPropy();
            string rutaConfirmarCuenta = Url.Action("ConfirmarCuenta", "Usuario", new { email = email });
            try
            {
                error = ValidacionParametros.ValidacionRecuperarContraseñaPaso1(email);
                if (error.codigoError != 0)
                {
                    ViewBag.Error = "Error";
                    ModelState.AddModelError("", error.descripcionError);
                    return View();
                }
                error = await ExpertoUsuarios.ReenvioMailVerificacion(email, rutaConfirmarCuenta);
                if (error.codigoError == -1)
                {
                    ViewBag.Error = "Error";
                    ModelState.AddModelError("", error.descripcionError);
                    return View();

                }

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ErrorDetalle = ex.StackTrace;
                return View("Error");

            }
            return RedirectToAction("Login", "Usuario", null);
        }
        [HttpGet]
        public ActionResult RecuperarContraseñaPaso1()
        {
            return View();
        }
        public async Task<ActionResult> RecuperarContraseñaPaso1(string email)
        {
            ErrorPropy error = new ErrorPropy();
            try
            {
                error = ValidacionParametros.ValidacionRecuperarContraseñaPaso1(email);
                if (error.codigoError != 0)
                {
                    ViewBag.Error = "Error";
                    ModelState.AddModelError("", error.descripcionError);
                    return View();
                }
                string rutaRestablecerContraseña = Url.Action("RecuperarContraseñaPaso2", "Usuario");
                error = await ExpertoUsuarios.RecuperarContraseñaPaso1(email, rutaRestablecerContraseña);
                if (error.codigoError == -1)
                {
                    ViewBag.Error = "Error";
                    ModelState.AddModelError("", error.descripcionError);
                    return View();
                }

                ViewBag.Mensaje = "OK";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ErrorDetalle = ex.StackTrace;
                return View("Error");

            }

            return View();
        }
        [HttpGet]
        public ActionResult RecuperarContraseñaPaso2()
        {
            return View();
        }
        public ActionResult RecuperarContraseñaPaso2(string email, string clave, string claveRepetida)
        {
            ErrorPropy error = new ErrorPropy();
            try
            {
                error = ValidacionParametros.ValidacionRecuperarContraseñaPaso2(email, clave, claveRepetida);
                if (error.codigoError != 0)
                {
                    ViewBag.Error = "Error";
                    ModelState.AddModelError("", error.descripcionError);
                    return View();
                }
                error = ExpertoUsuarios.RecuperarContraseña(email, clave, claveRepetida);
                if (error.codigoError == -1)
                {
                    ViewBag.Error = "Error";
                    ModelState.AddModelError("", error.descripcionError);
                    return View();
                }

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ErrorDetalle = ex.StackTrace;
                return View("Error");

            }

            return RedirectToAction("Login", "Usuario", null);
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public ActionResult Login()
        {
            // Almacena la URL actual en returnUrl
            string returnUrl = Request.UrlReferrer?.AbsoluteUri;
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string clave)
        {
            ErrorPropy error = new ErrorPropy();
            try
            {
                error = ValidacionParametros.ValidacionLogin(email, clave);
                if (error.codigoError != 0)
                {
                    ViewBag.Error = "Error";
                    ModelState.AddModelError("", error.descripcionError);
                    return View();
                }

                (ErrorPropy error, TransferenciaUsuario usuario) respuesta = ExpertoUsuarios.Login(email, clave);
                error = respuesta.error;
                if (error.codigoError == -1)
                {
                    ViewBag.Error = "Error";
                    ModelState.AddModelError("", error.descripcionError);
                    return View();
                }
                if (error.codigoError == -2)
                {
                    ViewBag.Error = "Error";
                    ModelState.AddModelError("", error.descripcionError);
                    ViewBag.EmailNoVerificado = "true";
                    return View();
                }

                // Almacena la URL actual en returnUrl
                string returnUrl = ViewBag.ReturnUrl as string;

                // Establece la sesión del usuario
                Session["IDUsuario"] = respuesta.usuario.usuarioId;

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    // Redirige a la URL almacenada después del inicio de sesión
                    return Redirect(returnUrl);
                }
                else
                {
                    // Si no hay returnUrl, redirige a la acción predeterminada
                    return RedirectToAction("Index", "PanelControl2");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ErrorDetalle = ex.StackTrace;
                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult ConfirmarCuenta(string email)
        {
            ErrorPropy error = new ErrorPropy();
            try
            {
                error = ExpertoUsuarios.ConfirmarCuenta(email);

                return RedirectToAction("Index", "Home", null);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ErrorDetalle = ex.StackTrace;
                return View("Error");

            }



        }
        [HttpPost]
        public ActionResult CrearCuenta(string nombreUsuario, string apellidoUsuario, string telefono1, string telefono2, string email, string clave, string claveRepetida, bool permitirSerContactadoPorPublicante, bool permitirSerNotificado)
        {

            ErrorPropy error = new ErrorPropy();

            try
            {
                error = ValidacionParametros.ValidacionParametrosCrearCuenta(nombreUsuario, apellidoUsuario, telefono1, telefono2, email, clave, claveRepetida);
                if (error.codigoError != 0)
                {
                    ViewBag.Error = "Error";
                    ModelState.AddModelError("", error.descripcionError);
                    return View();
                }






            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ErrorDetalle = ex.StackTrace;
                return View("Error");
                throw;
            }


            return RedirectToAction("Index", "PanelControl2", null);
        }




        [HttpGet]
        public ActionResult CrearCuenta()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        public ActionResult loginGoogleAsync([System.Web.Http.FromBody] string id_token, string returnUrl)
        {
            var tokenDeId = id_token;

            ErrorPropy error = new ErrorPropy();
            try
            {
                (ErrorPropy error, TransferenciaUsuario usuario) respuesta = ((ErrorPropy error, TransferenciaUsuario usuario))ExpertoUsuarios.LoginGoogle(tokenDeId);
                error = respuesta.error;
                if (error.codigoError == -1)
                {
                    ViewBag.Error = "Error";
                    ModelState.AddModelError("", error.descripcionError);
                    return View();
                }
                if (error.codigoError == -2)
                {
                    ViewBag.Error = "Error";
                    ModelState.AddModelError("", error.descripcionError);
                    ViewBag.EmailNoVerificado = "true";
                    return View();
                }

                Session["IDUsuario"] = respuesta.usuario.usuarioId;

                if (!string.IsNullOrEmpty(returnUrl) && !returnUrl.Contains("Login"))
                {
                    // Verifica si la returnUrl no contiene "Login" (evitar bucle de redirección)
                    return Redirect(returnUrl);
                }
                else
                {
                    // Si no es una returnUrl válida, redirige a la acción predeterminada
                    return RedirectToAction("Index", "PanelControl2");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ErrorDetalle = ex.StackTrace;
                return View("Error");
            }
        }



    }

}



