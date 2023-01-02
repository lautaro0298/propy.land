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


namespace WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        public static string token ;
        public static string sign ;
        public static DateTime generationTime;
        public static DateTime expirationTime;


        public ActionResult ConsultarActividad()
        {
            try
            {
                if (!ControlAcceso.Autorizacion(Session["IDUsuario"])) { return RedirectToAction("Index", "Home", null); }
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
        public ActionResult ConsultarListaFavoritos()
        {
            try
            {

                if (!ControlAcceso.Autorizacion(Session["IDUsuario"])) { return RedirectToAction("Index", "Home", null); }
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
                if (!ControlAcceso.Autorizacion(Session["IDUsuario"])) { return RedirectToAction("Index", "Home", null); }
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
        public ActionResult DeleteFavorito(string publicacionId)
        {
            try
            {
                if (!ControlAcceso.Autorizacion(Session["IDUsuario"])) { return RedirectToAction("Index", "Home", null); }
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
        public ActionResult Login()
        {
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

                Session["IDUsuario"] = respuesta.usuario.usuarioId;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ErrorDetalle = ex.StackTrace;
                return View("Error");
            }

            return RedirectToAction("Index", "PanelControl2");
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
        [HttpGet]
        public ActionResult CrearCuenta()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CrearCuenta(string nombreUsuario, string apellidoUsuario, string telefono1, string telefono2, string email, string clave, string claveRepetida, bool permitirSerContactadoPorPublicante, bool permitirSerNotificado)
        {
            //long CUIT = Int64.Parse(cuit);
            ErrorPropy error = new ErrorPropy();
            //string token = null;
            //string sign = null;
            
            //string strTicketRespuesta = null;
            //LoginTicket objTicketRespuesta = null;
            try
            {
                error = ValidacionParametros.ValidacionParametrosCrearCuenta(nombreUsuario, apellidoUsuario, telefono1, telefono2, email, clave, claveRepetida);
                if (error.codigoError != 0)
                {
                    ViewBag.Error = "Error";
                    ModelState.AddModelError("", error.descripcionError);
                    return View();
                }
                //try
                //{
                   
                //    AFIP.WSAA.LoginCMSService loginCMS = new AFIP.WSAA.LoginCMSService();
                //    objTicketRespuesta = new LoginTicket();
                //    strTicketRespuesta = objTicketRespuesta.Ticket();
                //}
                //catch (Exception excepcionAlObtenerTicket)
                //{
                //    Console.WriteLine("***EXCEPCION AL OBTENER TICKET: " + excepcionAlObtenerTicket.Message);

                //}
                //try {
          
                //    XmlDocument xmlDocument = new XmlDocument();
                //    xmlDocument.LoadXml(strTicketRespuesta);
                //    XmlNodeList listatoken = xmlDocument.GetElementsByTagName("token");
                //    XmlNodeList lostasing = xmlDocument.GetElementsByTagName("sign");
                //    XmlNodeList listadata = xmlDocument.GetElementsByTagName("generationTime");
                //    XmlNodeList listadata2 = xmlDocument.GetElementsByTagName("expirationTime");
                //    foreach (XmlNode listz in listadata)
                //    {

                //        var generacion = listz.ChildNodes[0].Value.ToString();
                //        generationTime = Convert.ToDateTime(generacion);


                //    }
                //    foreach (XmlNode listz in listadata2)
                //    {

                //        var expiracion = listz.ChildNodes[0].Value.ToString();
                //        expirationTime = Convert.ToDateTime(expiracion);
                //    }
                //    foreach (XmlNode listz in listatoken)
                //    {
                       
                //       token= listz.ChildNodes[0].Value.ToString();
                //    }
                //    foreach (XmlNode lis in lostasing) {
                //        sign = lis.ChildNodes[0].Value.ToString();
                //    }
                    
                //}catch(Exception ex)
                //{
                //    throw new Exception("Error verificar cuit en afip:"+ex.Message+" Intente mas tarde");
                //}
                //try
                //{
                //    long cuitRepresentada = 20128139185;
                //    CUIT = cuitRepresentada;
                   
                //    PersonaServiceA5Client personaServiceA5 = new PersonaServiceA5Client();
                //    var persona =personaServiceA5.getPersona(token, sign, cuitRepresentada, CUIT);


                //}
                //catch (Exception ex)
                //{
                //    throw new Exception("Error consulta afip" + ex.Message);
                //}


               
                
                ////analizo tiked
                


           

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

      
    }
}



