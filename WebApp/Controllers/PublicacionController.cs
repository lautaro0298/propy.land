using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.ViewModels;
using WebApp.Servicios;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using System.Web.Helpers;
using System.Threading.Tasks;
using WebApp.DTO;
using WebApp.Experto;

using WebApp.DTOJSon;
using System.Text.RegularExpressions;
using System.IO;
using WebApp.Enumerables;
using LibreriaExperto.Publicaciones;
using LibreriaExperto.Seguridad;
using LibreriaClases;
using LibreriaClases.DTO;
using System.Drawing;
using MercadoPago.DataStructures.Customer;
using LibreriaExperto.Usuarios;
using LibreriaClases.Transferencia;
using Habanero.Util;

namespace WebApp.Controllers
{
    public class PublicacionController : Controller
    {
        private PublicacionServicios servicios;
        private PropiedadExtrasServicios serviciosPropiedadExtras;
        private PropiedadServicios serviciosPropiedad;
        private PropiedadTipoAmbienteServicios serviciosPropiedadTipoAmbiente;
        private PublicacionEstadoServicios serviciosPublicacionEstado;
        private CorreoServicios serviciosCorreo;
        private PublicacionServicios serviciosPublicacion;
        
        
        private ExpertoGestionarPublicacion gestionarPublicacionExperto;
        
        public PublicacionController() {
            serviciosPropiedadTipoAmbiente = new PropiedadTipoAmbienteServicios();
            servicios = new PublicacionServicios();
            serviciosPropiedadExtras = new PropiedadExtrasServicios();
            serviciosPropiedad = new PropiedadServicios();
            serviciosPublicacionEstado = new PublicacionEstadoServicios();
            serviciosCorreo = new CorreoServicios();
            serviciosPublicacion = new PublicacionServicios();
            
            gestionarPublicacionExperto = new ExpertoGestionarPublicacion();
            
        }
        [HttpGet]
        public ActionResult ListarPublicacionesPorUsuario() {
            try
            {
                if (!ControlAcceso.Autorizacion(Session["IDUsuario"])) { return RedirectToAction("Login", "Usuario", null); }
                ErrorPropy error = new ErrorPropy();

                (ErrorPropy error, DTOPublicaciones publicaciones) respuesta = ExpertoPublicaciones.ListarPublicaciones(Session["IDUsuario"].ToString());
                
                return View(respuesta.publicaciones);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ErrorDetalle = ex.Message;
                return View("Error");
                
            }
            

        }
        public ActionResult BuscarPorId(string id) {
            DTOPublicaciones respuesta = ExpertoPublicaciones.ListarPublicacionesPorId(id);

            return View(respuesta.publicaciones);
        }
        [HttpGet]
        public ActionResult CrearPublicacion() {
            if (!ControlAcceso.Autorizacion(Session["IDUsuario"])) { return RedirectToAction("Login", "Usuario", null); }
            ErrorPropy error = new ErrorPropy();
            
            try
            {
                (ErrorPropy error,LibreriaClases.DTO.DTOCrearPublicacion datosCrearPublicacion) respuesta = ExpertoPublicaciones.ObtenerDatosCrearPublicacion(Session["IDUsuario"].ToString());
                if (respuesta.error.codigoError!=0) {
                    ViewBag.OperacionBloqueada = respuesta.error.descripcionError;
                    return View(respuesta.datosCrearPublicacion);
                }
                return View(respuesta.datosCrearPublicacion);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ErrorDetalle = ex.StackTrace;
                return View("Error");
                
            }
            
        }
        [HttpPost]
        public ActionResult CrearPublicacion(string ubicacion,string tipoPublicacion,string tipoPublicante,string tipoPropiedad,string tipoConstruccion,List<string>extras,string Baños,string Ambientes,string Cocheras, string Dormitorios,string antiguedad,string supCubierta,string supTerreno,bool amueblado,string expensasUltimoMes,string precioPropiedad,string pisos,string moneda,string reseña) {
            if (!ControlAcceso.Autorizacion(Session["IDUsuario"])) { return RedirectToAction("Login", "Usuario", null); }
            ErrorPropy error = new ErrorPropy();

            
            try
            {
                #region Manejo de imágenes
                HttpPostedFileBase imagenSubida = Request.Files["imagen-1"];
                HttpPostedFileBase imagenSubida2 = Request.Files["imagen-2"];
                HttpPostedFileBase imagenSubida3 = Request.Files["imagen-3"];
                HttpPostedFileBase imagenSubida4 = Request.Files["imagen-4"];
                HttpPostedFileBase imagenSubida5 = Request.Files["imagen-5"];
                HttpPostedFileBase imagenSubida6 = Request.Files["imagen-6"];
                HttpPostedFileBase imagenSubida7 = Request.Files["imagen-7"];
                HttpPostedFileBase imagenSubida8 = Request.Files["imagen-8"];
                List<HttpPostedFileBase> imagenesRecibidas = new List<HttpPostedFileBase>();
                imagenesRecibidas.Add(imagenSubida);
                imagenesRecibidas.Add(imagenSubida2);
                imagenesRecibidas.Add(imagenSubida3);
                imagenesRecibidas.Add(imagenSubida4);
                imagenesRecibidas.Add(imagenSubida5);
                imagenesRecibidas.Add(imagenSubida6);
                imagenesRecibidas.Add(imagenSubida7);
                imagenesRecibidas.Add(imagenSubida8);
                List<HttpPostedFileBase> imagenesEnviadas = new List<HttpPostedFileBase>();
                //Si no sube imagen la quito de la lista
                foreach (var imagen in imagenesRecibidas)
                {
                    if (imagen!=null)
                    {
                        if (imagen.ContentLength != 0)
                        {
                            imagenesEnviadas.Add(imagen);
                        }
                    }
                }
                if(imagenesRecibidas.IsNull()|| imagenesRecibidas.Count==0)
                //foreach (var imagen in imagenesEnviadas)
                //{
                //    if (!ImagenServicios.ValidarImagen(imagen))
                {
                    ModelState.AddModelError("", "No se subio ninguna imagen");
                    (ErrorPropy error, LibreriaClases.DTO.DTOCrearPublicacion datosCrearPublicacion) respuesta = ExpertoPublicaciones.ObtenerDatosCrearPublicacion(Session["IDUsuario"].ToString());

                    return View(respuesta.datosCrearPublicacion);
                }
            

                    string ruta = HttpContext.Server.MapPath("~/Temp/");
                List<string> rutasImagenes = ImagenServicios.SubirArchivo(ruta, imagenesEnviadas);
                #endregion

                #region Conversiones
                int nroPisos = 0;
                decimal precioProp = 0;
                int superficieCubierta = 0;
                int superficieTerreno = 0;
                int añosAntiguedad = 0;
                int cantidadBaños = Convert.ToInt32(Baños);
                int cantidadAmbientes = Convert.ToInt32(Ambientes);
                int cantidadDormitorios = Convert.ToInt32(Dormitorios);
                int cantidadCocheras = Convert.ToInt32(Cocheras);
                decimal importeExpensasUltimoMes = 0;
                if (extras == null) { extras = new List<string>(); }
                if (!String.IsNullOrEmpty(precioPropiedad)) { precioProp = Convert.ToDecimal(precioPropiedad); }
                if (!String.IsNullOrEmpty(supCubierta)) { superficieCubierta = Convert.ToInt32(supCubierta); }
                if (!String.IsNullOrEmpty(supTerreno)) { superficieTerreno = Convert.ToInt32(supTerreno); }
                if (!String.IsNullOrEmpty(antiguedad)) { añosAntiguedad = Convert.ToInt32(antiguedad); }
                if (!String.IsNullOrEmpty(expensasUltimoMes)) { importeExpensasUltimoMes = Convert.ToDecimal(expensasUltimoMes); }
                if (!String.IsNullOrEmpty(pisos)) { nroPisos = Convert.ToInt32(pisos); }
                #endregion

                error = ValidacionParametros.ValidacionCrearPublicacion(ubicacion,tipoPropiedad,tipoPublicacion,tipoPublicante,tipoConstruccion,moneda,superficieTerreno,superficieCubierta,precioProp,añosAntiguedad,nroPisos,rutasImagenes);
                if (error.codigoError!=0) {
                    ViewBag.Error = "Error";
                    ModelState.AddModelError("",error.descripcionError);
                    (ErrorPropy error,LibreriaClases.DTO.DTOCrearPublicacion datosCrearPublicacion) respuesta = ExpertoPublicaciones.ObtenerDatosCrearPublicacion(Session["IDUsuario"].ToString());
                    
                    return View(respuesta.datosCrearPublicacion);
                }
                

                error = ExpertoPublicaciones.CrearPublicacion(ubicacion,moneda,tipoPropiedad,tipoPublicacion,tipoPublicante,tipoConstruccion,superficieCubierta,superficieTerreno,cantidadDormitorios,cantidadBaños,cantidadAmbientes,cantidadCocheras,rutasImagenes,extras,amueblado, importeExpensasUltimoMes,precioProp, Session["IDUsuario"].ToString(),añosAntiguedad,nroPisos,reseña);
                if (error.codigoError==-5) {
                    return RedirectToAction("Index","ContratarPlan");
                }
                if (error.codigoError==-1) {
                    ViewBag.Error = "Error";
                    ModelState.AddModelError("", error.descripcionError);
                    (ErrorPropy error, LibreriaClases.DTO.DTOCrearPublicacion datosCrearPublicacion) respuesta = ExpertoPublicaciones.ObtenerDatosCrearPublicacion(Session["IDUsuario"].ToString());
                    
                    return View(respuesta.datosCrearPublicacion);
                }
                
                ViewBag.Mensaje = "¡Usted ha creado una publicación! Consulte su panel de control";
                return View("SuccessProcess");
                
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ErrorDetalle = ex.StackTrace;
                return View("Error");
                throw;
            }
        }
        [HttpGet]
        public ActionResult EditarPublicacionNew(string publicacionId) {
            if (!ControlAcceso.Autorizacion(Session["IDUsuario"])) { return RedirectToAction("Login", "Usuario", null); }
            try
            {
                ErrorPropy error = new ErrorPropy();
                (ErrorPropy error, LibreriaClases.DTO.DTOEditarPublicacion datosEditarPublicacion) respuesta = ExpertoPublicaciones.ObtenerDatosEditarPublicacion(publicacionId, Session["IDUsuario"].ToString());
                
                return View(respuesta.datosEditarPublicacion);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ErrorDetalle = ex.StackTrace;
                return View("Error");
                
            }
            
        }
        [HttpPost]
        public ActionResult EditarPublicacionNew(string publicacionId,string ubicacion, string tipoPublicacion, string tipoPropiedad, string tipoConstruccion, List<string> extras, string Baños, string Ambientes, string Cocheras, string Dormitorios, string antiguedad, string supCubierta, string supTerreno, bool amueblado, string expensasUltimoMes, string precioPropiedad, string pisos, string moneda,List<string> imagenesDescartadas,string reseña) {
            try
            {
                if (!ControlAcceso.Autorizacion(Session["IDUsuario"])) { return RedirectToAction("Login", "Usuario", null); }
                ErrorPropy error = new ErrorPropy();
                #region Conversiones
                int nroPisos = 0;
                decimal precioProp = 0;
                int superficieCubierta = 0;
                int superficieTerreno = 0;
                int añosAntiguedad = 0;
                int cantidadBaños = Convert.ToInt32(Baños);
                int cantidadAmbientes = Convert.ToInt32(Ambientes);
                int cantidadDormitorios = Convert.ToInt32(Dormitorios);
                int cantidadCocheras = Convert.ToInt32(Cocheras);
                decimal importeExpensasUltimoMes = 0;
                if (!String.IsNullOrEmpty(precioPropiedad)) { precioProp = Convert.ToDecimal(precioPropiedad); }
                if (!String.IsNullOrEmpty(supCubierta)) { superficieCubierta = Convert.ToInt32(supCubierta); }
                if (!String.IsNullOrEmpty(supTerreno)) { superficieTerreno = Convert.ToInt32(supTerreno); }
                if (!String.IsNullOrEmpty(antiguedad)) { añosAntiguedad = Convert.ToInt32(antiguedad); }
                if (!String.IsNullOrEmpty(expensasUltimoMes)) { importeExpensasUltimoMes = Convert.ToDecimal(expensasUltimoMes); }
                if (!String.IsNullOrEmpty(pisos)) { nroPisos = Convert.ToInt32(pisos); }
                if (imagenesDescartadas is null) { imagenesDescartadas = new List<string>(); }
                #endregion
                #region Manejo de imágenes
                HttpPostedFileBase imagenSubida = Request.Files["imagen1"];
                HttpPostedFileBase imagenSubida2 = Request.Files["imagen2"];
                HttpPostedFileBase imagenSubida3 = Request.Files["imagen3"];
                HttpPostedFileBase imagenSubida4 = Request.Files["imagen4"];
                List<HttpPostedFileBase> imagenesRecibidas = new List<HttpPostedFileBase>();
                imagenesRecibidas.Add(imagenSubida);
                imagenesRecibidas.Add(imagenSubida2);
                imagenesRecibidas.Add(imagenSubida3);
                imagenesRecibidas.Add(imagenSubida4);
                List<HttpPostedFileBase> imagenesEnviadas = new List<HttpPostedFileBase>();
                //Si no sube imagen la quito de la lista
                foreach (var imagen in imagenesRecibidas)
                {
                    if (imagen.FileName != "")
                    {
                        imagenesEnviadas.Add(imagen);
                    }
                }
                foreach (var imagen in imagenesEnviadas)
                {
                    if (!ImagenServicios.ValidarImagen(imagen))
                    {
                        ViewBag.Error = "OK";
                        ModelState.AddModelError("", "Formato de imagen incorrecto");
                        (ErrorPropy error, LibreriaClases.DTO.DTOCrearPublicacion datosCrearPublicacion) respuesta = ExpertoPublicaciones.ObtenerDatosCrearPublicacion(Session["IDUsuario"].ToString());
                        
                        return View(respuesta.datosCrearPublicacion);
                    }
                }

                string ruta = HttpContext.Server.MapPath("~/Temp/");
                List<string> rutasNuevasImagenes = ImagenServicios.SubirArchivo(ruta, imagenesEnviadas);
                #endregion

                error = ValidacionParametros.ValidacionEditarPublicacion(tipoPropiedad,tipoPublicacion,tipoConstruccion,moneda,superficieTerreno,superficieCubierta,precioProp,añosAntiguedad,nroPisos, rutasNuevasImagenes);
                if (error.codigoError!=0) {
                    ViewBag.Error = "OK";
                    ModelState.AddModelError("", error.descripcionError);
                    (ErrorPropy error, LibreriaClases.DTO.DTOEditarPublicacion datosCrearPublicacion) respuesta = ExpertoPublicaciones.ObtenerDatosEditarPublicacion(publicacionId, Session["IDUsuario"].ToString());
                    
                    return View(respuesta.datosCrearPublicacion);
                }
                error = ExpertoPublicaciones.EditarPublicacion(publicacionId, ubicacion, tipoPropiedad, tipoPublicacion, tipoConstruccion, superficieTerreno, superficieCubierta, cantidadDormitorios, cantidadCocheras, cantidadBaños, cantidadAmbientes, añosAntiguedad, importeExpensasUltimoMes, amueblado, nroPisos, precioProp, moneda, extras, reseña, imagenesDescartadas,rutasNuevasImagenes);
                if (error.codigoError==-1) {
                    ViewBag.Error = "OK";
                    ModelState.AddModelError("", error.descripcionError);
                    (ErrorPropy error, LibreriaClases.DTO.DTOEditarPublicacion datosCrearPublicacion) respuesta = ExpertoPublicaciones.ObtenerDatosEditarPublicacion(publicacionId, Session["IDUsuario"].ToString());
                    
                    return View(respuesta.datosCrearPublicacion);
                }
                
                ViewBag.Mensaje = "Usted editó una propiedad con exito";
                return View("SuccessProcess");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ErrorDetalle = ex.StackTrace;
                return View("Error");
                
            }
            
        }
        [HttpGet]
        public ActionResult EliminarPublicacion(string publicacionId) {
            if (!ControlAcceso.Autorizacion(Session["IDUsuario"])) { return RedirectToAction("Login", "Usuario", null); }
            try
            {
                ErrorPropy error = new ErrorPropy();
                error = ExpertoPublicaciones.EliminarPublicacion(publicacionId);
               
                ViewBag.Mensaje = "Usted ha eliminado un inmueble con éxito.";
                return View("SuccessProcess");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ErrorDetalle = ex.Message;
                return View("Error");
                
            }
            
        }
        [HttpGet]
        public ActionResult Ordenar(string metodoOrdenamiento) {
            if (!ControlAcceso.Autorizacion(Session["IDUsuario"])) { return RedirectToAction("Login", "Usuario", null); }
            try
            {
                (ErrorPropy error, DTOPublicaciones pubs) respuesta = ExpertoPublicaciones.OrdenarPublicacionesUsuario(metodoOrdenamiento,Session["IDUsuario"].ToString());
                
                return View("ListarPublicacionesPorUsuario",respuesta.pubs);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ErrorDetalle = ex.Message;
                return View("Error");
                
            }
        }
        [Authorize]
        // GET: Publicacion
        public ActionResult Index()
        {
            try
            {
                var usuario = User.Identity.GetUserId();
                DTOListadoPublicacionesPorUsuario data = gestionarPublicacionExperto.ObtenerListadoPublicacionesPorUsuario(usuario);
                return PartialView(data);
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index","Error",new {codError=ex.HResult,descripcionError=ex.Message });
            }
            
        }
        /*public ActionResult VisualizarImagen(Guid publicacionId,int indice)
        {
            List<byte[]> cover = servicios.ObtenerImagenBD(publicacionId);
            if (cover[indice] != null)
            {
                FileContentResult img = File(cover[indice], "image/jpg");
                return img;
                //File(cover[0], "image/jpg");
            }
            else
            {
                return null;
            }
        }*/

        

        [Authorize]
        [HttpGet]
        public ActionResult EditarPublicacion(Guid publicacionId) {
            DTOVistaEditarPublicacion data = gestionarPublicacionExperto.PrepararDatosEditarPublicacion(publicacionId);
            return View(data);
        }
        [Authorize]
        [HttpPost]
        public ActionResult EditarPublicacion(DTOVistaEditarPublicacion datos) {
            if (ModelState.IsValid)
            {
                DTOError error = gestionarPublicacionExperto.EditarPublicacion(datos);
                if (error.codigoError != 0)
                {
                    ViewBag.Error = error.descripcionError.FirstOrDefault();
                    return View();
                    //return RedirectToAction("EditarPublicacion");
                }
                else {
                    return RedirectToAction("Index", "Publicacion");
                }
                
            }
            else {
                return View(datos);
            }

            
        }

        [Authorize]
        [HttpGet]
        public ActionResult RealizarPublicacion() {
            DTOVistaNuevaPublicacion dtoNuevaPublicacion = new DTOVistaNuevaPublicacion();
            try
            {
                
                dtoNuevaPublicacion = gestionarPublicacionExperto.PrepararDatosNuevaPublicacion();
            }
            catch (Exception)
            {

                ViewBag.ErrorFatal = "Algo salió mal, vuelva a intentarlo más tarde";
            }
            

            return View(dtoNuevaPublicacion);
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> RealizarPublicacion(string comentarios,string tipoMoneda,string añosAntiguedad,string direccion,string tipoConstruccion,string tipoOperacion,string tipoPropiedad,string tipoUsuario,string superficieCubierta,string superficieTerreno,int nroPlantas,List<int> cantidadAmbientes,List<Guid> tipoAmbiente,List<Guid> extras,String precioPropiedad) {
            try
            {

                #region Preparación de parametros para su manejo
                //saco el formato numérico al precioPropiedad
                string patron = @"[^\w]";
                Regex regex = new Regex(patron);
                string PRECIO = regex.Replace(precioPropiedad, "");
                string años = regex.Replace(añosAntiguedad, "");
                string supterreno = regex.Replace(superficieTerreno, "");
                string supcubierta = regex.Replace(superficieCubierta, "");

                DTOValidarParametrosNuevaPublicacion dtoValidacion = new DTOValidarParametrosNuevaPublicacion();
                dtoValidacion.direccion = direccion;
                dtoValidacion.precio = Convert.ToDecimal(PRECIO);
                dtoValidacion.tipoConstruccion = tipoConstruccion;
                dtoValidacion.tipoPropiedad = tipoPropiedad;
                dtoValidacion.superficieCubierta = (float)Convert.ToDouble(supcubierta);
                dtoValidacion.superficieTerreno = (float)Convert.ToDouble(supterreno);
                HttpPostedFileBase imagenSubida = Request.Files["imagenSubida"];
                HttpPostedFileBase imagenSubida2 = Request.Files["imagenSubida2"];
                HttpPostedFileBase imagenSubida3 = Request.Files["imagenSubida3"];
                List<HttpPostedFileBase> imagenesRecibidas = new List<HttpPostedFileBase>();
                imagenesRecibidas.Add(imagenSubida);
                imagenesRecibidas.Add(imagenSubida2);
                imagenesRecibidas.Add(imagenSubida3);
                List<HttpPostedFileBase> imagenesEnviadas = new List<HttpPostedFileBase>();
                //Si no sube imagen la quito de la lista
                foreach (var imagen in imagenesRecibidas)
                {
                    if (imagen.FileName != "")
                    {
                        imagenesEnviadas.Add(imagen);
                    }
                }
                dtoValidacion.imagenes = imagenesEnviadas;
                #endregion

                #region Control de errores en los parámetros
                DTOError errorParametros = ValidacionErroresServicios.ValidarParámetrosNuevaPublicación(dtoValidacion);
                if (errorParametros.codigoError == 0)
                {
                    WebApp.DTO.DTOCrearPublicacion datos = new WebApp.DTO.DTOCrearPublicacion();
                    DTOError errorApiGoogleMaps = new DTOError();

                    #region Control de errores en la api de google
                    string query = "https://maps.googleapis.com/maps/api/geocode/json?address=" + direccion + "&key=AIzaSyAXxPwQsLSfF4gC7VtJdl9GIIcyAzVdmhk";
                    DTORootObjectAddress rootObjAddress = APIGoogleMapsServicios.SolicitarUbicacionCompleta(query);
                    if (rootObjAddress.status == "OK")
                    {
                        errorApiGoogleMaps.codigoError = 0;

                        for (int i = 0; i < rootObjAddress.results[0].address_components.Count; i++)
                        {
                            switch (rootObjAddress.results[0].address_components[i].types[0])
                            {
                                case "route":
                                    datos.calle = rootObjAddress.results[0].address_components[i].long_name;
                                    break;
                                case "administrative_area_level_2":
                                    datos.areaAdministrativaNivel2 = rootObjAddress.results[0].address_components[i].long_name;
                                    break;
                                case "administrative_area_level_1":
                                    datos.areaAdministrativaNivel1 = rootObjAddress.results[0].address_components[i].long_name;
                                    break;
                                case "country":
                                    datos.pais = rootObjAddress.results[0].address_components[i].long_name;
                                    break;
                                case "street_number":
                                    datos.nroCalle = Convert.ToInt32(rootObjAddress.results[0].address_components[i].long_name);
                                    break;

                            }
                        }

                        string ruta = HttpContext.Server.MapPath("~/Temp/");
                        List<string> rutasImg = ImagenServicios.SubirArchivo(ruta, imagenesEnviadas);

                        List<byte[]> imagenes = servicios.ConvertToBytes(imagenesEnviadas);
                        datos.latitud = rootObjAddress.results[0].geometry.location.lat;
                        datos.longitud = rootObjAddress.results[0].geometry.location.lng;
                        datos.direccionFormateada = rootObjAddress.results[0].formatted_address;
                        datos.identificadorUbicacionGoogle = rootObjAddress.results[0].place_id;
                        datos.antiguedad = Convert.ToInt32(años);
                        datos.cantidadAmbientes = cantidadAmbientes;
                        datos.tipoConstruccion = Guid.Parse(tipoConstruccion);
                        datos.tipoMoneda = Guid.Parse(tipoMoneda);
                        datos.tipoOperacion = Guid.Parse(tipoOperacion);
                        datos.tipoPropiedad = Guid.Parse(tipoPropiedad);
                        datos.tipoUsuario = Guid.Parse(tipoUsuario);
                        datos.superficieTerreno = (float)Convert.ToDouble(supterreno);
                        datos.superficieCubierta = (float)Convert.ToDouble(supcubierta);
                        datos.extras = extras;
                        datos.imagenes = imagenes;
                        datos.rutasImagenes = rutasImg;
                        datos.precioPropiedad = Convert.ToDecimal(PRECIO);
                        datos.tipoAmbiente = tipoAmbiente;
                        datos.observaciones = comentarios;
                        datos.usuarioId = User.Identity.GetUserId();

                        #region Control de errores en el Experto
                        string ip = Request.UserHostAddress;
                        //Llamo al experto
                        DTOError errorExperto = await gestionarPublicacionExperto.CrearPublicacion(datos, ip);
                        if (errorExperto.codigoError != 0)
                        {
                            ModelState.AddModelError("", errorExperto.descripcionError.FirstOrDefault());
                            return View(serviciosPublicacion.PrepararDatosPublicacion());
                        }
                        else
                        {

                            return RedirectToAction("Index");
                        }
                        #endregion
                    }
                    else
                    {
                        errorApiGoogleMaps.codigoError = (int)Enums.CodigosError.codErrorNuevaPublicacion;
                        errorApiGoogleMaps.descripcionError.Add(NotificacionesServicios.errorApiGoogleMapsIdentificador);
                        ModelState.AddModelError("", errorApiGoogleMaps.descripcionError.FirstOrDefault());
                        return View(serviciosPublicacion.PrepararDatosPublicacion());
                    }
                    #endregion
                }
                else
                {
                    foreach (var error in errorParametros.descripcionError)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return View(serviciosPublicacion.PrepararDatosPublicacion());
                }
                #endregion
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { codError = ex.HResult, descripcionError = ex.Message });
            }                       
        }                     
    }
}