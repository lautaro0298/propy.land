using LibreriaClases;
using LibreriaClases.DTO;
using LibreriaClases.Transferencia;
using LibreriaExperto.Comunicaciones_Externas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using LibreriaExperto.Utils;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace LibreriaExperto.Busquedas
{
    public static class ExpertoBusquedas
    {
        public static (ErrorPropy, DTOContenedorResultadosBusqueda) generico()
        {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            var tareaObtenerPublicaciones = clienteHttp.GetAsync("api/Busqueda/obtenerPropiedadesParaEvaluarBusqueda");
            tareaObtenerPublicaciones.Wait();
            if (!tareaObtenerPublicaciones.Result.IsSuccessStatusCode)
            {
                throw new Exception(tareaObtenerPublicaciones.Result.StatusCode.ToString());
            }
            List<TransferenciaPublicacion> publicaciones = tareaObtenerPublicaciones.Result.Content.ReadAsAsync<List<TransferenciaPublicacion>>().Result;
            DTOContenedorResultadosBusqueda contenedorResultadosBusqueda = new DTOContenedorResultadosBusqueda();
            List<DTOPropiedad> propiedades = new List<DTOPropiedad>();
            foreach (var publicacion in publicaciones)
            {
                DTOPropiedad propiedad = new DTOPropiedad();
                propiedad.publicacionId = publicacion.publicacionId;
                propiedad.fechaInicioPublicacion = publicacion.fechaInicioPublicacion.ToShortDateString();
                propiedad.tipoMoneda = publicacion.Propiedad.TipoMoneda.denominacionMoneda;
                propiedad.precioPropiedad = String.Format("{0:n}", publicacion.Propiedad.precioPropiedad);
                propiedad.ubicación = publicacion.Propiedad.ubicacion;
                int f = 0;
                foreach (var e in publicacion.Propiedad.TipoPropiedad)
                {
                    propiedad.tipoPropiedad.Add(publicacion.Propiedad.TipoPropiedad.ElementAt(f).nombreTipoPropiedad);
                    f++;
                }
                propiedad.tipoPublicante = publicacion.Propiedad.TipoPublicante.nombreTipoPublicante;
                propiedad.imagen = publicacion.Propiedad.ImagenPropiedad.FirstOrDefault().rutaImagenPropiedad;
                propiedad.tipoPublicacion = publicacion.TipoPublicacion.nombreTipoPublicacion;
                contenedorResultadosBusqueda.propiedades.Add(propiedad);
                DTOPincheGoogleMaps pincheGoogleMaps = new DTOPincheGoogleMaps();
                pincheGoogleMaps.publicacionId = publicacion.publicacionId;
                pincheGoogleMaps.latitud = publicacion.Propiedad.latitud;
                pincheGoogleMaps.longitud = publicacion.Propiedad.longitud;
                pincheGoogleMaps.precioPropiedad = String.Format("{0:n}", publicacion.Propiedad.precioPropiedad);
                pincheGoogleMaps.tipoMoneda = publicacion.Propiedad.TipoMoneda.denominacionMoneda;
                f = 0;
                foreach (var d in publicacion.Propiedad.TipoPropiedad)
                {
                    pincheGoogleMaps.tipoPropiedad.Add( publicacion.Propiedad.TipoPropiedad.ElementAt(f).nombreTipoPropiedad);
                    f++;
                }
                pincheGoogleMaps.ubicacion = publicacion.Propiedad.ubicacion;
                contenedorResultadosBusqueda.pinchesGoogleMaps.Add(pincheGoogleMaps);
            }

            return (error, contenedorResultadosBusqueda);
         

        }
            public static (ErrorPropy, DTOContenedorResultadosBusqueda) Buscar(DTOFiltrosBusqueda parametrosBusqueda) {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            var tareaObtenerPublicaciones = clienteHttp.GetAsync("api/Busqueda/obtenerPropiedadesParaEvaluarBusqueda");
            tareaObtenerPublicaciones.Wait();
            if (!tareaObtenerPublicaciones.Result.IsSuccessStatusCode) {
                throw new Exception(tareaObtenerPublicaciones.Result.StatusCode.ToString());
            }
            string pais = String.Empty;
            string areaAdmNivel1 = String.Empty;
            string areaAdmNivel2 = String.Empty;
            string sufijoProvincia = String.Empty;
            string sufijoDepartamento = String.Empty;
           
            var publicacionn =  tareaObtenerPublicaciones.Result.Content.ReadAsStringAsync();
            publicacionn.Wait();
            var publicaciones = JsonConvert.DeserializeObject<List<TransferenciaPublicacion>>(publicacionn.Result);
            if (!String.IsNullOrEmpty(parametrosBusqueda.ubicacion)) {
                (ErrorPropy error, DTOUbicacionGoogle google) respuesta = ExpertoComunicacionExterna.ObtenerParametrosUbicacionGoogleMaps(parametrosBusqueda.ubicacion, clienteHttp, true);
                if (respuesta.error.codigoError != 0)
                {
                    error = respuesta.error;
                    return (error,null);
                }
                
                if (respuesta.google.areaAdministrativaNivel1.Contains("Province")) { sufijoProvincia = " Province"; }
                if (respuesta.google.areaAdministrativaNivel2.Contains("Department")) { sufijoDepartamento = " Department"; }
                pais = respuesta.google.pais;
                areaAdmNivel1 = respuesta.google.areaAdministrativaNivel1;
                areaAdmNivel2 = respuesta.google.areaAdministrativaNivel2;
            }
            List<TransferenciaPublicacion> publicacionesFiltradasPaso1 = new List<TransferenciaPublicacion>();
            #region Paso 1. Filtrar por ubicación.
            if (parametrosBusqueda.radioBusqueda != -1)
            {
                DTOPosicion origen = new DTOPosicion(parametrosBusqueda.latitud, parametrosBusqueda.longitud); ;
                foreach (var publicacion in publicaciones)
                {
                    DTOPosicion destino = new DTOPosicion(publicacion.Propiedad.latitud, publicacion.Propiedad.longitud);
                    double distancia = DistanciaEntrePuntosGeometricos.CalculoFormulaHaversine(origen, destino);
                    int distanciaEntero = Convert.ToInt32(distancia * 1000);
                    if (distanciaEntero < parametrosBusqueda.radioBusqueda * 1000)
                    {
                        publicacionesFiltradasPaso1.Add(publicacion);
                    }
                }
            }
            else {
                if (!String.IsNullOrEmpty(areaAdmNivel2) && !String.IsNullOrEmpty(areaAdmNivel1) && !String.IsNullOrEmpty(pais))
                {
                    foreach (var publicacion in publicaciones)
                    {
                        if (publicacion.Propiedad.AreaAdmNivel2 + sufijoDepartamento == areaAdmNivel2 && publicacion.Propiedad.AreaAdmNivel1 + sufijoProvincia == areaAdmNivel1 && publicacion.Propiedad.pais == pais)
                        {
                            publicacionesFiltradasPaso1.Add(publicacion);
                        }
                    }
                }
                if (String.IsNullOrEmpty(areaAdmNivel2) && String.IsNullOrEmpty(areaAdmNivel1) && String.IsNullOrEmpty(pais))
                {
                    publicacionesFiltradasPaso1 = publicaciones;
                }
                if (!String.IsNullOrEmpty(areaAdmNivel2) && String.IsNullOrEmpty(areaAdmNivel1) && String.IsNullOrEmpty(pais))
                {
                    foreach (var publicacion in publicaciones)
                    {
                        if (publicacion.Propiedad.AreaAdmNivel2 + sufijoDepartamento == areaAdmNivel2) { publicacionesFiltradasPaso1.Add(publicacion); }
                    }
                }
                if (String.IsNullOrEmpty(areaAdmNivel2) && !String.IsNullOrEmpty(areaAdmNivel1) && String.IsNullOrEmpty(pais))
                {
                    foreach (var publicacion in publicaciones)
                    {
                        if (publicacion.Propiedad.AreaAdmNivel1 + sufijoProvincia == areaAdmNivel1) { publicacionesFiltradasPaso1.Add(publicacion); }
                    }
                }
                if (String.IsNullOrEmpty(areaAdmNivel2) && String.IsNullOrEmpty(areaAdmNivel1) && !String.IsNullOrEmpty(pais))
                {
                    foreach (var publicacion in publicaciones)
                    {
                        if (publicacion.Propiedad.pais == pais) { publicacionesFiltradasPaso1.Add(publicacion); }
                    }
                }
                if (!String.IsNullOrEmpty(areaAdmNivel2) && !String.IsNullOrEmpty(areaAdmNivel1) && String.IsNullOrEmpty(pais))
                {
                    foreach (var publicacion in publicaciones)
                    {
                        if (publicacion.Propiedad.AreaAdmNivel2 + sufijoDepartamento == areaAdmNivel2 && publicacion.Propiedad.AreaAdmNivel1 + sufijoProvincia == areaAdmNivel1) { publicacionesFiltradasPaso1.Add(publicacion); }
                    }
                }
                if (String.IsNullOrEmpty(areaAdmNivel2) && !String.IsNullOrEmpty(areaAdmNivel1) && !String.IsNullOrEmpty(pais))
                {
                    foreach (var publicacion in publicaciones)
                    {
                        if (publicacion.Propiedad.AreaAdmNivel1 + sufijoProvincia == areaAdmNivel1 && publicacion.Propiedad.pais == pais) { publicacionesFiltradasPaso1.Add(publicacion); }
                    }
                }
                if (!String.IsNullOrEmpty(areaAdmNivel2) && String.IsNullOrEmpty(areaAdmNivel1) && !String.IsNullOrEmpty(pais))
                {
                    foreach (var publicacion in publicaciones)
                    {
                        if (publicacion.Propiedad.AreaAdmNivel2 + sufijoDepartamento == areaAdmNivel2 && publicacion.Propiedad.pais == pais) { publicacionesFiltradasPaso1.Add(publicacion); }
                    }
                }
            }
            

            #endregion
            List<TransferenciaPublicacion> publicacionesFiltradasPaso2 = new List<TransferenciaPublicacion>();
            #region Paso 2. Filtrar por tipo de publicación.
            if (parametrosBusqueda.tipoPublicacion != "Indistinto")
            {
                foreach (var propiedad in publicacionesFiltradasPaso1)
                {
                    if (propiedad.tipoPublicacionId == parametrosBusqueda.tipoPublicacion)
                    {
                        publicacionesFiltradasPaso2.Add(propiedad);
                    }
                }
            }
            else {
                publicacionesFiltradasPaso2 = publicacionesFiltradasPaso1;
            }
            #endregion
            List<TransferenciaPublicacion> publicacionesFiltradasPaso3 = new List<TransferenciaPublicacion>();
            #region Filtrar por tipo de propiedad
            foreach (var propiedad in publicacionesFiltradasPaso2) {
                if (parametrosBusqueda.tipoPropiedad != "Indistinto")
                {
                    if (propiedad.Propiedad.tipoPropiedadId == parametrosBusqueda.tipoPropiedad)
                    {
                        publicacionesFiltradasPaso3.Add(propiedad);
                    }
                }
                else {
                    publicacionesFiltradasPaso3 = publicacionesFiltradasPaso2;
                }
            }
            #endregion
            List<TransferenciaPublicacion> publicacionesFiltradasPaso4 = new List<TransferenciaPublicacion>();
            #region Filtrar por tipo de construcción
            foreach (var propiedad in publicacionesFiltradasPaso3) {
                if (parametrosBusqueda.tipoConstruccion != "Indistinto")
                {
                    if (propiedad.Propiedad.tipoConstruccionId == parametrosBusqueda.tipoConstruccion)
                    {
                        publicacionesFiltradasPaso4.Add(propiedad);
                    }
                }
                else {
                    publicacionesFiltradasPaso4 = publicacionesFiltradasPaso3;
                }
            }
            #endregion
            List<TransferenciaPublicacion> publicacionesFiltradasPaso5 = new List<TransferenciaPublicacion>();
            #region Filtrar por tipo de publicante
            foreach (var propiedad in publicacionesFiltradasPaso4) {
                if (parametrosBusqueda.tipoPublicante != "Indistinto")
                {
                    if (propiedad.Propiedad.tipoPublicanteId == parametrosBusqueda.tipoPublicante)
                    {
                        publicacionesFiltradasPaso5.Add(propiedad);
                    }
                }
                else {
                    publicacionesFiltradasPaso5 = publicacionesFiltradasPaso4;
                }
            }
            #endregion
            List<TransferenciaPublicacion> publicacionesFiltradasPaso6 = new List<TransferenciaPublicacion>();
            #region Filtrar por precios

            //var cotizacion = APICotizacionToday.GetCotizacion();

            //for(int cont =0; cont < cotizacion.Count(); cont++)
            //{
            //    if (cotizacion.ElementAt(cont).result.source == parametrosBusqueda.denominacionMoneda) cotizacion.Remove(cotizacion.ElementAt(cont));
            //}

            if (parametrosBusqueda.precioDesde == 0 && parametrosBusqueda.precioHasta == 0)
            {
                publicacionesFiltradasPaso6 = publicacionesFiltradasPaso5;
            }
            else
            {
                //foreach (var x in cotizacion)
                //{
                //    foreach (var propiedad in publicacionesFiltradasPaso5)
                //    {
                //        if (parametrosBusqueda.precioDesde != 0 && parametrosBusqueda.precioHasta == 0)
                //        {
                //            if ((propiedad.Propiedad.TipoMoneda.denominacionMoneda == parametrosBusqueda.denominacionMoneda && propiedad.Propiedad.precioPropiedad >= parametrosBusqueda.precioDesde) ||
                //                (propiedad.Propiedad.TipoMoneda.denominacionMoneda == x.result.source && (propiedad.Propiedad.precioPropiedad * Convert.ToDecimal(x.result.value)) >=  parametrosBusqueda.precioDesde))
                //            {
                //                publicacionesFiltradasPaso6.Add(propiedad);
                //            }
                //        }
                //        else
                //        {
                //            if(parametrosBusqueda.precioDesde == 0 && parametrosBusqueda.precioHasta != 0)
                //            {
                //                if ((propiedad.Propiedad.TipoMoneda.denominacionMoneda == parametrosBusqueda.denominacionMoneda && propiedad.Propiedad.precioPropiedad <= parametrosBusqueda.precioHasta) || 
                //                        (propiedad.Propiedad.TipoMoneda.denominacionMoneda == x.result.source && (propiedad.Propiedad.precioPropiedad * Convert.ToDecimal(x.result.value)) <=  parametrosBusqueda.precioHasta))
                //                {
                //                    publicacionesFiltradasPaso6.Add(propiedad);
                //                }
                //            }
                //            else
                //            {
                //                if(parametrosBusqueda.precioDesde != 0 && parametrosBusqueda.precioHasta != 0)
                //                {
                //                    if((propiedad.Propiedad.TipoMoneda.denominacionMoneda == parametrosBusqueda.denominacionMoneda && propiedad.Propiedad.precioPropiedad >= parametrosBusqueda.precioDesde && 
                //                        propiedad.Propiedad.precioPropiedad <= parametrosBusqueda.precioHasta) || 
                //                        (propiedad.Propiedad.TipoMoneda.denominacionMoneda == x.result.source && (propiedad.Propiedad.precioPropiedad * Convert.ToDecimal(x.result.value)) >=  parametrosBusqueda.precioDesde) &&
                //                        (propiedad.Propiedad.TipoMoneda.denominacionMoneda == x.result.source && (propiedad.Propiedad.precioPropiedad * Convert.ToDecimal(x.result.value)) <= parametrosBusqueda.precioHasta))
                //                    {
                //                        publicacionesFiltradasPaso6.Add(propiedad);
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}
            }
            
           
           /* if (parametrosBusqueda.precioDesde == 0 && parametrosBusqueda.precioHasta == 0) {
                publicacionesFiltradasPaso6 = publicacionesFiltradasPaso5;
            } else if (parametrosBusqueda.precioDesde != 0 && parametrosBusqueda.precioHasta == 0) {
                foreach (var propiedad in publicacionesFiltradasPaso5) {
                    if (propiedad.Propiedad.precioPropiedad >= parametrosBusqueda.precioDesde  ) {
                        publicacionesFiltradasPaso6.Add(propiedad);
                    }
                }
            } else if (parametrosBusqueda.precioDesde == 0 && parametrosBusqueda.precioHasta != 0) {
                foreach (var propiedad in publicacionesFiltradasPaso5) {
                    if (propiedad.Propiedad.precioPropiedad <= parametrosBusqueda.precioHasta) {
                        publicacionesFiltradasPaso6.Add(propiedad);
                    }
                }
            } else if (parametrosBusqueda.precioDesde!=0 && parametrosBusqueda.precioHasta!=0) {
                foreach (var propiedad in publicacionesFiltradasPaso5) {
                    if (propiedad.Propiedad.precioPropiedad>=parametrosBusqueda.precioDesde && propiedad.Propiedad.precioPropiedad<=parametrosBusqueda.precioHasta) {
                        publicacionesFiltradasPaso6.Add(propiedad);
                    }
                }
            }*/
            #endregion
            List<TransferenciaPublicacion> publicacionesFiltradasPaso7 = new List<TransferenciaPublicacion>();
            #region Filtrar por características específicas
            int cantAmbientes = 0;
            int cantDormitorios = 0;
            int cantBaños = 0;
            int cantCocheras = 0;
            if (parametrosBusqueda.característicasEspecificasHabilitadas == true)
            {
                foreach (var propiedad in publicacionesFiltradasPaso6)
                {
                    foreach (var tipoAmbiente in propiedad.Propiedad.PropiedadTipoAmbiente)
                    {
                        if (tipoAmbiente.activo == true)
                        {
                            switch (tipoAmbiente.TipoAmbiente.nombreTipoAmbiente)
                            {
                                case "Baños":
                                    if (tipoAmbiente.cantidad == parametrosBusqueda.cantidadBaños)
                                    {
                                        cantBaños=tipoAmbiente.cantidad;
                                    }
                                    break;
                                case "Cocheras":
                                    if (tipoAmbiente.cantidad == parametrosBusqueda.cantidadCocheras)
                                    {
                                        cantCocheras=tipoAmbiente.cantidad;
                                    }
                                    break;
                                case "Dormitorios":
                                    if (tipoAmbiente.cantidad == parametrosBusqueda.cantidadDormitorios)
                                    {
                                        cantDormitorios=tipoAmbiente.cantidad;
                                    }
                                    break;
                                case "Ambientes":
                                    if (tipoAmbiente.cantidad == parametrosBusqueda.cantidadAmbientes)
                                    {
                                        cantAmbientes=tipoAmbiente.cantidad;
                                    }
                                    break;
                            }
                        }
                    }
                    if (cantBaños == parametrosBusqueda.cantidadBaños && cantDormitorios == parametrosBusqueda.cantidadDormitorios && cantCocheras == parametrosBusqueda.cantidadCocheras && parametrosBusqueda.cantidadAmbientes == cantAmbientes)
                    {
                        publicacionesFiltradasPaso7.Add(propiedad);
                    }
                    cantAmbientes = 0;
                    cantBaños = 0;
                    cantCocheras = 0;
                    cantDormitorios = 0;
                }
            }
            else {
                publicacionesFiltradasPaso7 = publicacionesFiltradasPaso6;
            }
            #endregion Filtrar por características específicas
            List<TransferenciaPublicacion> publicacionesFiltradasPaso8 = new List<TransferenciaPublicacion>();
            #region Filtrar por extras
            //if (parametrosBusqueda.extras.Count > 0)
            //{
            //    foreach (var propiedad in publicacionesFiltradasPaso7)
            //    {
                   
            //        foreach (var extra in propiedad.Propiedad.PropiedadCaracteristica)
            //        {
            //            if (extra.activo==true && parametrosBusqueda.extras.Contains(extra.tipoPropiedadCaracteristicaID))
            //            {
            //                publicacionesFiltradasPaso8.Add(propiedad);
            //                break;
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    publicacionesFiltradasPaso8 = publicacionesFiltradasPaso7;
            //}
            #endregion



            #region Datos
            DTOContenedorResultadosBusqueda contenedorResultadosBusqueda = new DTOContenedorResultadosBusqueda();
            List<DTOPropiedad> propiedades = new List<DTOPropiedad>();
            foreach (var publicacion in publicacionesFiltradasPaso8) {
                DTOPropiedad propiedad = new DTOPropiedad();
                propiedad.publicacionId = publicacion.publicacionId;
                propiedad.fechaInicioPublicacion = publicacion.fechaInicioPublicacion.ToShortDateString();
                propiedad.tipoMoneda = publicacion.Propiedad.TipoMoneda.denominacionMoneda;
                propiedad.precioPropiedad = String.Format("{0:n}", publicacion.Propiedad.precioPropiedad);
                propiedad.ubicación = publicacion.Propiedad.ubicacion;
                //propiedad.tipoPropiedad = publicacion.Propiedad.TipoPropiedad.nombreTipoPropiedad;
                propiedad.tipoPublicante = publicacion.Propiedad.TipoPublicante.nombreTipoPublicante;
                propiedad.imagen = publicacion.Propiedad.ImagenPropiedad.FirstOrDefault().rutaImagenPropiedad;
                propiedad.tipoPublicacion = publicacion.TipoPublicacion.nombreTipoPublicacion;
                contenedorResultadosBusqueda.propiedades.Add(propiedad);
                DTOPincheGoogleMaps pincheGoogleMaps = new DTOPincheGoogleMaps();
                pincheGoogleMaps.publicacionId = publicacion.publicacionId;
                pincheGoogleMaps.latitud = publicacion.Propiedad.latitud;
                pincheGoogleMaps.longitud = publicacion.Propiedad.longitud;
                pincheGoogleMaps.precioPropiedad = String.Format("{0:n}", publicacion.Propiedad.precioPropiedad);
                pincheGoogleMaps.tipoMoneda = publicacion.Propiedad.TipoMoneda.denominacionMoneda;
                //pincheGoogleMaps.tipoPropiedad = publicacion.Propiedad.TipoPropiedad.nombreTipoPropiedad;
                pincheGoogleMaps.ubicacion = publicacion.Propiedad.ubicacion;
                contenedorResultadosBusqueda.pinchesGoogleMaps.Add(pincheGoogleMaps);
            }
            #endregion
            return (error,contenedorResultadosBusqueda);
        }
        public static (ErrorPropy,DTOParametrosBusqueda) ObtenerParametrosBusqueda() {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            DTOParametrosBusqueda parametrosBusqueda = new DTOParametrosBusqueda();
            #region Obtener características disponibles
            var tareaObtenerCaracteristicas = clienteHttp.GetAsync("api/Caracteristica/obtenerCaracteristicas");
            tareaObtenerCaracteristicas.Wait();
            if (!tareaObtenerCaracteristicas.Result.IsSuccessStatusCode) {
                throw new Exception(tareaObtenerCaracteristicas.Result.StatusCode.ToString());
            }
            List<TransferenciaCaracteristica> caracteristicas = tareaObtenerCaracteristicas.Result.Content.ReadAsAsync<List<TransferenciaCaracteristica>>().Result;
            foreach (var caracteristica in caracteristicas) {
                DTOCaracteristica objCaracteristica = new DTOCaracteristica();
                objCaracteristica.caracteristicaId = caracteristica.caracteristicaId;
                objCaracteristica.nombreCaracteristica = caracteristica.nombreCaracteristica;
                parametrosBusqueda.caracteristicas.Add(objCaracteristica);
            }
            #endregion
            #region Obtener tipos de propiedades disponibles
            var tareaObtenerTiposPropiedades = clienteHttp.GetAsync("api/TipoPropiedad/obtenerTiposPropiedades");
            tareaObtenerCaracteristicas.Wait();
            if (!tareaObtenerTiposPropiedades.Result.IsSuccessStatusCode) {
                throw new Exception(tareaObtenerCaracteristicas.Result.StatusCode.ToString());
            }
            List<DTOTipoPropiedad> tiposPropiedades = tareaObtenerTiposPropiedades.Result.Content.ReadAsAsync<List<DTOTipoPropiedad>>().Result;
            foreach (var tipoPropiedad in tiposPropiedades) {
                DTOTipoPropiedad objTipoPropiedad = new DTOTipoPropiedad();

                objTipoPropiedad.tipoPropiedadId = tipoPropiedad.tipoPropiedadId;
                objTipoPropiedad.nombreTipoPropiedad = tipoPropiedad.nombreTipoPropiedad;
                parametrosBusqueda.tiposPropiedades.Add(objTipoPropiedad);
            }
            #endregion
            #region Obtener tipos de ambientes
            var tareaObtenerTiposAmbientes = clienteHttp.GetAsync("api/TipoAmbiente/obtenerTiposAmbientes");
            tareaObtenerTiposAmbientes.Wait();
            if (!tareaObtenerTiposAmbientes.Result.IsSuccessStatusCode) {
                throw new Exception(tareaObtenerTiposAmbientes.Result.StatusCode.ToString());
            }
            List<TipoAmbiente> tiposAmbientes = tareaObtenerTiposAmbientes.Result.Content.ReadAsAsync<List<TipoAmbiente>>().Result;
            foreach (var tipoAmbiente in tiposAmbientes) {
                DTOTipoAmbiente objTipoAmbiente = new DTOTipoAmbiente();
                objTipoAmbiente.tipoAmbienteId = tipoAmbiente.tipoAmbienteId;
                objTipoAmbiente.nombreTipoAmbiente = tipoAmbiente.nombreTipoAmbiente;
                parametrosBusqueda.tiposAmbientes.Add(objTipoAmbiente);
            }
            #endregion
            #region Obtener tipos de publicantes
            var tareaObtenerTiposPublicantes = clienteHttp.GetAsync("api/TipoPublicante/obtenerTiposPublicantes");
            tareaObtenerTiposPublicantes.Wait();
            if (!tareaObtenerTiposPublicantes.Result.IsSuccessStatusCode) {
                throw new Exception(tareaObtenerTiposPublicantes.Result.StatusCode.ToString());
            }
            List<TransferenciaTipoPublicante> tiposPublicantes = tareaObtenerTiposPublicantes.Result.Content.ReadAsAsync<List<TransferenciaTipoPublicante>>().Result;
            foreach (var tipoPublicante in tiposPublicantes) {
                DTOTipoPublicante objTipoPublicante = new DTOTipoPublicante();
                objTipoPublicante.tipoPublicanteId = tipoPublicante.tipoPublicanteId;
                objTipoPublicante.nombreTipoPublicante = tipoPublicante.nombreTipoPublicante;
                parametrosBusqueda.tiposPublicantes.Add(objTipoPublicante);
            }
            #endregion
            #region Obtener tipos de publicaciones
            var tareaObtenerTiposPublicaciones = clienteHttp.GetAsync("api/TipoPublicacion/obtenerTiposPublicaciones");
            tareaObtenerTiposPublicaciones.Wait();
            if (!tareaObtenerTiposPublicaciones.Result.IsSuccessStatusCode) {
                throw new Exception(tareaObtenerTiposPublicaciones.Result.StatusCode.ToString());
            }
            List<TransferenciaTipoPublicacion> tiposPublicaciones = tareaObtenerTiposPublicaciones.Result.Content.ReadAsAsync<List<TransferenciaTipoPublicacion>>().Result;
            foreach (var tipoPublicacion in tiposPublicaciones) {
                DTOTipoPublicacion objTipoPublicacion = new DTOTipoPublicacion();
                objTipoPublicacion.tipoPublicacionId = tipoPublicacion.tipoPublicacionId;
                objTipoPublicacion.nombreTipoPublicacion = tipoPublicacion.nombreTipoPublicacion;
                parametrosBusqueda.tiposPublicaciones.Add(objTipoPublicacion);
            }
            #endregion
            #region Obtener tipos de construcciones
            var tareaObtenerTiposConstrucciones = clienteHttp.GetAsync("api/TipoConstruccion/obtenerTiposConstrucciones");
            tareaObtenerTiposConstrucciones.Wait();
            if (!tareaObtenerTiposConstrucciones.Result.IsSuccessStatusCode) {
                throw new Exception(tareaObtenerTiposConstrucciones.Result.StatusCode.ToString());
            }
            List<TransferenciaTipoConstruccion> tiposConstrucciones = tareaObtenerTiposConstrucciones.Result.Content.ReadAsAsync<List<TransferenciaTipoConstruccion>>().Result;
            foreach (var tipoConstruccion in tiposConstrucciones) {
                DTOTipoConstruccion objTipoConstruccion = new DTOTipoConstruccion();
                objTipoConstruccion.tipoConstruccionId = tipoConstruccion.tipoConstruccionId;
                objTipoConstruccion.nombreTipoConstruccion = tipoConstruccion.nombreTipoConstruccion;
                parametrosBusqueda.tiposConstrucciones.Add(objTipoConstruccion);
            }
            #endregion
            #region Obtener tipos de monedas
            var tareaObtenerTiposMonedas = clienteHttp.GetAsync("api/TipoMoneda/obtenerTiposMonedas");
            if (!tareaObtenerTiposMonedas.Result.IsSuccessStatusCode) {
                throw new Exception(tareaObtenerTiposMonedas.Result.StatusCode.ToString());
            }
            List<TransferenciaTipoMoneda> tiposMonedas = tareaObtenerTiposMonedas.Result.Content.ReadAsAsync<List<TransferenciaTipoMoneda>>().Result;
            foreach (var tipoMoneda in tiposMonedas) {
                DTOTipoMoneda objTipoMoneda = new DTOTipoMoneda();
                objTipoMoneda.tipoMonedaId = tipoMoneda.tipoMonedaId;
                objTipoMoneda.denominacionMoneda = tipoMoneda.denominacionMoneda;
                parametrosBusqueda.tiposMonedas.Add(objTipoMoneda);
            }
            #endregion



            return (error,parametrosBusqueda);
        }
    }
}
