using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Servicios;

using WebApp.Experto;

using WebApp.DTOJSon;
using System.Text.RegularExpressions;
using System.Globalization;
using LibreriaClases;
using MercadoPago.DataStructures.Customer;
using LibreriaExperto.Busquedas;
using LibreriaExperto.Seguridad;
using LibreriaClases.DTO;
using NUnit.Framework.Constraints;
using WebApp.DTO;

using LibreriaExperto.ConsultaAsincrona;

namespace WebApp.Controllers
{
    public class BusquedaController : Controller
    {
        ExpertoRealizarBusqueda realizarBusquedaExperto = new ExpertoRealizarBusqueda();
        BusquedaServicios bs = new BusquedaServicios();
        [HttpGet]
        public ActionResult Nindex()
        {
            var respuesta = ExpertoBusquedas.generico();

            
            return View(respuesta.Item2);
        }

        [HttpGet]
        public ActionResult Buscar() {
            try
            {
                
                (ErrorPropy error, DTOParametrosBusqueda parametrosBusqueda) respuesta = ExpertoBusquedas.ObtenerParametrosBusqueda();
                
                return View(respuesta.parametrosBusqueda);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ErrorDetalle = ex.StackTrace;
                return View("Error");
               
            }
            
        }
        [HttpGet]
        [ActionName("Getcaracteristicas")]
        public JsonResult Getcaracteristicas(string id)
        {
            var consulta =new ExpertoConsultaAsincrona() ;
            var publicaciones = consulta.ConsultaCaracteristica(id);
            List<SelectListItem> items = publicaciones.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.caracteristicas.nombreCaracteristica.ToString(),
                    Value = d.caracteristicas.caracteristicaId.ToString(),
                    Selected = false
                };
            });
            ViewBag.items = items;
            return Json(publicaciones, JsonRequestBehavior.AllowGet);
           // return new JsonResult() { Data = publicaciones };
        }
        [HttpPost]
        public ActionResult Buscar(string clientAddress,string latitud,string longitud, string tipoOperacion,string tipoPropiedad,string tipoConstruccion,string moneda, string tipoPublicante,string precioDesde,string precioHasta,bool característicasEspecificas,string cantidadDormitorios, string cantidadAmbientes, string cantidadBaños, string cantidadCocheras,int radio,List<string> extras) {
            try
            {
                #region Conversiones
                double lat = Double.Parse(latitud, CultureInfo.InvariantCulture);
                double lng = Double.Parse(longitud, CultureInfo.InvariantCulture);
                int cantBaños = 0;
                int cantDormitorios = 0;
                int cantAmbientes = 0;
                int cantCocheras = 0;
                decimal precioD = 0;
                decimal precioH = 0;
                if (extras is null) { extras = new List<string>(); }
                if (!String.IsNullOrEmpty(precioDesde)) { precioD = Convert.ToDecimal(precioDesde); }
                if (!String.IsNullOrEmpty(precioHasta)) { precioH = Convert.ToDecimal(precioHasta); }
                if (!String.IsNullOrEmpty(cantidadAmbientes)) { cantAmbientes = Convert.ToInt32(cantidadAmbientes); }
                if (!String.IsNullOrEmpty(cantidadBaños)) { cantBaños = Convert.ToInt32(cantidadBaños); }
                if (!String.IsNullOrEmpty(cantidadDormitorios)) { cantDormitorios = Convert.ToInt32(cantidadDormitorios); }
                if (!String.IsNullOrEmpty(cantidadCocheras)) { cantCocheras = Convert.ToInt32(cantidadCocheras); }
                
                #endregion
                ErrorPropy error = new ErrorPropy();
                error = ValidacionParametros.ValidacionBusqueda(precioD,precioH);
                if (error.codigoError!=0) {
                    ViewBag.Error = error.descripcionError;
                    ModelState.AddModelError("", error.descripcionError);
                    (ErrorPropy error, DTOParametrosBusqueda parametrosBusqueda) res = ExpertoBusquedas.ObtenerParametrosBusqueda();
                    
                    return View(res.parametrosBusqueda);
                }
                LibreriaClases.DTO.DTOFiltrosBusqueda parametrosBusqueda = new LibreriaClases.DTO.DTOFiltrosBusqueda();
                parametrosBusqueda.ubicacion = clientAddress;
                parametrosBusqueda.tipoPublicacion = tipoOperacion;
                parametrosBusqueda.tipoPropiedad = tipoPropiedad;
                parametrosBusqueda.tipoConstruccion = tipoConstruccion;
                parametrosBusqueda.tipoPublicante = tipoPublicante;
                parametrosBusqueda.precioDesde = precioD;
                parametrosBusqueda.precioHasta = precioH;
                parametrosBusqueda.característicasEspecificasHabilitadas = característicasEspecificas;
                parametrosBusqueda.cantidadCocheras = cantCocheras;
                parametrosBusqueda.cantidadBaños = cantBaños;
                parametrosBusqueda.cantidadDormitorios = cantDormitorios;
                parametrosBusqueda.cantidadAmbientes = cantAmbientes;
                parametrosBusqueda.latitud = lat;
                parametrosBusqueda.longitud = lng;
                parametrosBusqueda.radioBusqueda = radio;
                parametrosBusqueda.extras = extras;
                parametrosBusqueda.denominacionMoneda = moneda;
                

                (ErrorPropy error, LibreriaClases.DTO.DTOContenedorResultadosBusqueda resultadosBusqueda) respuesta = ExpertoBusquedas.Buscar(parametrosBusqueda);
                if (respuesta.error.codigoError==-1) {
                    ViewBag.Error = respuesta.error.descripcionError;
                    ModelState.AddModelError("",respuesta.error.descripcionError);
                    (ErrorPropy error, DTOParametrosBusqueda parametrosBusqueda) respuesta2 = ExpertoBusquedas.ObtenerParametrosBusqueda();
                    
                    return View(respuesta2.parametrosBusqueda);
                }
                
                return View("ResultadosBusqueda",respuesta.resultadosBusqueda);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ErrorDetalle = ex.StackTrace;
                return View("Error");
               
            }
        }
        
        // GET: Busqueda
        public ActionResult Index()
        {
            try
            {
                return View(/*realizarBusquedaExperto.PrepararFiltrosParaBusqueda()*/);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { codError = ex.HResult, descripcionError = ex.Message });
                
            }
           

            
            
        }
        [HttpPost]
        public ActionResult Index(string clientAddress, string tipoPropiedad, string tipoOperacion, string tipoConstruccion, List<string> extras, String precioDesde, String precioHasta,
            string tipoMoneda, int radio, string latitud, string longitud, string SupTMax, string SupTMin, int cantidadBaños = -1, 
            int cantidadDormitorios = -1, int cantidadCocheras = -1, int antiguedad = -1, int cantidadPlantas = -1, int cantidadAmbientes =- 1) 
        {
            try
            {

                ValidacionErroresServicios.PrevenirErroresNull(ref precioDesde, ref precioHasta, ref SupTMax, ref SupTMin, ref clientAddress, ref radio);
                
                //Convierto latitud y longitud en float
                double lat = Double.Parse(latitud, CultureInfo.InvariantCulture);
                double lng = Double.Parse(longitud, CultureInfo.InvariantCulture);

                //saco el formato numérico al precioPropiedad
                string patron = @"[^\w]";
                Regex regex = new Regex(patron);
                string PRECIOdesde = regex.Replace(precioDesde, "");
                string PRECIOhasta = regex.Replace(precioHasta, "");
                string SupTerrenoMax = regex.Replace(SupTMax, "");
                string SupTerrenoMin = regex.Replace(SupTMin, "");
                string rutaRelativa = Server.MapPath("~");



                DTOValidarParametrosFiltrosBusqueda dtoValidacion = new DTOValidarParametrosFiltrosBusqueda();
                dtoValidacion.precioDesde = Convert.ToDecimal(PRECIOdesde);
                dtoValidacion.precioHasta = Convert.ToDecimal(PRECIOhasta);
                dtoValidacion.SuperficieTerrenoMax = Convert.ToInt32(SupTerrenoMax);
                dtoValidacion.SuperficieTerrenoMin = Convert.ToInt32(SupTerrenoMin);
                DTOError errores = ValidacionErroresServicios.ValidarEnvioParametrosFiltroBusqueda(dtoValidacion);

                if (errores.codigoError != 0)
                {

                    foreach (var error in errores.descripcionError)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return View(realizarBusquedaExperto.PrepararFiltrosParaBusqueda());
                }
                else
                {
                    DTOBusqueda dto = new DTOBusqueda();
                    if (clientAddress == "")
                    {
                        dto.areaAdministrativaNivel1 = "-1";
                        dto.areaAdministrativaNivel2 = "-1";
                        dto.pais = "-1";
                    }
                    else
                    {
                        string query = "https://maps.googleapis.com/maps/api/geocode/json?address=" + clientAddress + "&key=AIzaSyAXxPwQsLSfF4gC7VtJdl9GIIcyAzVdmhk";
                        DTORootObjectAddress rootObjAddress = APIGoogleMapsServicios.SolicitarUbicacionCompleta(query);
                        if (rootObjAddress.status == "OK")
                        {
                            for (int i = 0; i < rootObjAddress.results[0].address_components.Count; i++)
                            {
                                switch (rootObjAddress.results[0].address_components[i].types[0])
                                {
                                    case "administrative_area_level_2":
                                        dto.areaAdministrativaNivel2 = rootObjAddress.results[0].address_components[i].long_name;
                                        break;
                                    case "administrative_area_level_1":
                                        dto.areaAdministrativaNivel1 = rootObjAddress.results[0].address_components[i].long_name;
                                        break;
                                    case "country":
                                        dto.pais = rootObjAddress.results[0].address_components[i].long_name;
                                        break;
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", NotificacionesServicios.errorApiGoogleMapsIdentificador);
                            return View(realizarBusquedaExperto.PrepararFiltrosParaBusqueda());
                            
                        }
                    }
                    if (String.IsNullOrEmpty(dto.areaAdministrativaNivel2))
                    {
                        dto.areaAdministrativaNivel2 = "-1";
                    }
                    if (String.IsNullOrEmpty(dto.areaAdministrativaNivel1))
                    {
                        dto.areaAdministrativaNivel1 = "-1";
                    }
                    dto.tipoPropiedad = tipoPropiedad;
                    dto.tipoOperacion = tipoOperacion;
                    dto.tipoConstruccion = tipoConstruccion;
                    dto.cantidadBaños = cantidadBaños;
                    dto.cantidadDormitorios = cantidadDormitorios;
                    dto.cantidadCocheras = cantidadCocheras;
                    dto.antiguedad = antiguedad;
                    dto.cantidadPlantas = cantidadPlantas;
                    dto.extras = extras;
                    dto.precio = -1; //eliminar cuando se implemente precio rapido
                    dto.precioDesde = Convert.ToDecimal(PRECIOdesde);
                    dto.precioHasta = Convert.ToDecimal(PRECIOhasta);
                    dto.superficieTerrenoMax = Convert.ToInt32(SupTerrenoMax);
                    dto.superficieTerrenoMin = Convert.ToInt32(SupTerrenoMin);
                    dto.rutaRelativa = rutaRelativa;
                    dto.latitudOrigen = lat;
                    dto.longitudOrigen = lng;
                    dto.radioElegido = radio;
                    dto.cantidadAmbientes = cantidadAmbientes;

                    DTOTodosResultadosBusqueda data = realizarBusquedaExperto.RealizarBusqueda(dto, tipoMoneda);
                    if (data.ListaDtoResultadoPublicacion.Count == 0)
                    {
                        ViewBag.Mensaje = "Lo sentimos. No se encontraron resultados";
                    }
                    System.Web.HttpContext.Current.Session["PublicacionesFiltradas"] = data;
                    TempData["PublicacionesFiltradas"] = data;
                    return RedirectToAction("RealizarBusqueda", "Busqueda");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { codError = ex.HResult, descripcionError = ex.Message });
                
            }
                   
            
        }


        [HttpGet]
        public ActionResult RealizarBusqueda() {
            try
            {
                DTOTodosResultadosBusqueda publicacionesFiltradas = (DTOTodosResultadosBusqueda)System.Web.HttpContext.Current.Session["PublicacionesFiltradas"];
                //TempData["PublicacionesAordenar"] = publicacionesFiltradas;
                return View(publicacionesFiltradas);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error", new { codError = ex.HResult, descripcionError = ex.Message });
                
            }
            
        }
        [HttpPost]
        public ActionResult RealizarBusqueda(string condicionOrdenamiento) {
            try
            {
                DTOTodosResultadosBusqueda publicacionesFiltradas = (DTOTodosResultadosBusqueda)System.Web.HttpContext.Current.Session["PublicacionesFiltradas"];

                switch (condicionOrdenamiento)
                {
                    case "SoloPrecio":
                        publicacionesFiltradas.ListaDtoResultadoPublicacion = publicacionesFiltradas.ListaDtoResultadoPublicacion.OrderBy(x => x.precioPropiedad).ToList();
                        break;
                    case "SoloFecha":
                        publicacionesFiltradas.ListaDtoResultadoPublicacion = publicacionesFiltradas.ListaDtoResultadoPublicacion.OrderByDescending(x => x.fechaInicioPublicacion).ToList();
                        break;
                    case "PrecioFecha":
                        publicacionesFiltradas.ListaDtoResultadoPublicacion = publicacionesFiltradas.ListaDtoResultadoPublicacion.OrderBy(x => x.precioPropiedad).ThenByDescending(y => y.fechaInicioPublicacion).ToList();
                        break;
                }
                return View(publicacionesFiltradas);
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index", "Error", new { codError = ex.HResult, descripcionError = ex.Message });
            }
            
        }
        
    }
}