using LibreriaClases;
using LibreriaClases.Transferencia;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using LibreriaClases.DTO;
using LibreriaExperto.Comunicaciones_Externas;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using LibreriaExperto.Usuarios;
using LibreriaExperto.Seguridad;

namespace LibreriaExperto.Publicaciones
{
    public static class ExpertoPublicaciones
    {
        public static void HabilitarDeshabilitarPublicacionesUsuario(List<TransferenciaPublicacion> publicaciones,int estado,HttpClient clienteHttp) {
            ErrorPropy error = new ErrorPropy();
            foreach (var publicacionUsuario in publicaciones) {
                publicacionUsuario.estado = estado;
            }
            
            var tareaHabilitarDeshabilitarPublicaciones = clienteHttp.PostAsJsonAsync<List<TransferenciaPublicacion>>("api/Publicacion/habilitarDeshabilitarPublicaciones", publicaciones);
            tareaHabilitarDeshabilitarPublicaciones.Wait();
            if (!tareaHabilitarDeshabilitarPublicaciones.Result.IsSuccessStatusCode)
            {
                throw new Exception(tareaHabilitarDeshabilitarPublicaciones.Result.StatusCode.ToString());
            }
            
        }
        public static (ErrorPropy,DTOPublicaciones) OrdenarPublicacionesUsuario(string metodoOrdenamiento,string usuarioId) {
            ErrorPropy error = new ErrorPropy();
            (ErrorPropy error, DTOPublicaciones publicaciones) respuesta = ListarPublicaciones(usuarioId);
            if (respuesta.error.codigoError!=0) {
                error = respuesta.error;
                return (error, null);
            }
            List<DTOPublicacion> publicacionesOrdenadas = new List<DTOPublicacion>();
            switch (metodoOrdenamiento) {
                case "PrecioMayorMenor":
                    respuesta.publicaciones.publicaciones = respuesta.publicaciones.publicaciones.OrderByDescending(x => x.precioPropiedad).ToList();
                    return (error, respuesta.publicaciones);
                case "PrecioMenorMayor":
                    respuesta.publicaciones.publicaciones = respuesta.publicaciones.publicaciones.OrderBy(x => x.precioPropiedad).ToList();
                    return (error,respuesta.publicaciones);
                case "MasNuevas":
                    respuesta.publicaciones.publicaciones = respuesta.publicaciones.publicaciones.OrderByDescending(x => x.fechaInicioPublicacion).ToList();
                    return (error, respuesta.publicaciones);
                case "MasViejas":
                    respuesta.publicaciones.publicaciones = respuesta.publicaciones.publicaciones.OrderBy(x => x.fechaInicioPublicacion).ToList();
                    return (error, respuesta.publicaciones);
                        
            }
            return (error, respuesta.publicaciones);
        }
        public static (ErrorPropy,DTOPublicaciones) ListarPublicaciones(string usuarioId) {
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            ErrorPropy error = new ErrorPropy();
            DTOPublicaciones pubs = new DTOPublicaciones();
            (ErrorPropy error, TransferenciaUsuario usuario) respuestaObtenerUsuario = ExpertoUsuarios.ObtenerUsuarioPorID(usuarioId,clienteHttp);
            if (respuestaObtenerUsuario.error.codigoError!=0) {
                return (respuestaObtenerUsuario.error, null);
            }
            var tareaObtenerPublicacionesPorUsuario = clienteHttp.GetAsync("api/Publicacion/obtenerPublicacionesPorUsuario/"+usuarioId);
            tareaObtenerPublicacionesPorUsuario.Wait();
            if (!tareaObtenerPublicacionesPorUsuario.Result.IsSuccessStatusCode) {
                throw new Exception(tareaObtenerPublicacionesPorUsuario.Result.StatusCode.ToString());
            }
            TimeZoneInfo zonaHorariaArgentina = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
            List<TransferenciaPublicacion> publicaciones = tareaObtenerPublicacionesPorUsuario.Result.Content.ReadAsAsync<List<TransferenciaPublicacion>>().Result;
            if (respuestaObtenerUsuario.usuario.PlanUsuario.Count == 0)
            {
                pubs.cantidadCreditosActivos = 0;
            }
            else {
                pubs.cantidadCreditosActivos = respuestaObtenerUsuario.usuario.PlanUsuario.Where(x => x.activo == true).FirstOrDefault().cantidadCreditosActivos;
            }
            
            foreach (var publicacion in publicaciones) {
                DTOPublicacion objPublicacion = new DTOPublicacion();
                objPublicacion.publicacionId = publicacion.publicacionId;
                objPublicacion.fechaInicioPublicacion = TimeZoneInfo.ConvertTimeFromUtc(publicacion.fechaInicioPublicacion, zonaHorariaArgentina).ToShortDateString();
                objPublicacion.fechaFinPublicacion = TimeZoneInfo.ConvertTimeFromUtc(publicacion.fechaFinPublicacion, zonaHorariaArgentina).ToShortDateString();
                objPublicacion.estado = publicacion.estado;
                int cout = 0;
                //foreach (var c in publicacion.Propiedad.TipoPropiedad) {
                //    objPublicacion.tipoPropiedad[cout] = publicacion.Propiedad.TipoPropiedad.ElementAt(cout).nombreTipoPropiedad;
                //    cout++;
                //}
                objPublicacion.precioPropiedad = String.Format("{0:c}",publicacion.Propiedad.precioPropiedad);
                objPublicacion.ubicacionPropiedad = publicacion.Propiedad.ubicacion;
                objPublicacion.tipoMoneda = publicacion.Propiedad.TipoMoneda.denominacionMoneda;
                objPublicacion.pais = publicacion.Propiedad.pais;
                objPublicacion.provincia = publicacion.Propiedad.AreaAdmNivel1;
                objPublicacion.departamento = publicacion.Propiedad.AreaAdmNivel2;
                pubs.publicaciones.Add(objPublicacion);
            }
            return (error,pubs);

        }
        public static ErrorPropy CrearPublicacion(string ubicacion, string tipoMoneda, string tipoPropiedad, string tipoPublicacion, string tipoPublicante,string tipoConstruccion,int superficieCubierta,int superficieTerreno, int cantidadDormitorios, int cantidadBaños, int cantidadAmbientes, int cantidadCocheras,List<string>rutasImagenes,List<string> extras,bool amueblado, decimal importeExpensasUltimoMes,decimal precioPropiedad,string usuarioId,int añosAntiguedad,int nroPisos,string reseña) {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            
            var tareaObtenerDuplicado = clienteHttp.GetAsync("api/Publicacion/obtenerPropiedades");
            tareaObtenerDuplicado.Wait();
            if (!tareaObtenerDuplicado.Result.IsSuccessStatusCode) {
                throw new Exception(tareaObtenerDuplicado.Result.StatusCode.ToString());
            }
            List<TransferenciaPropiedad> propiedades = tareaObtenerDuplicado.Result.Content.ReadAsAsync<List<TransferenciaPropiedad>>().Result;
            

            (ErrorPropy error, DTOUbicacionGoogle google) respuestaGoogle = ExpertoComunicacionExterna.ObtenerParametrosUbicacionGoogleMaps(ubicacion,clienteHttp,false);
            if (respuestaGoogle.error.codigoError!=0) {
                error = respuestaGoogle.error;
                return error;
            }
            foreach (var prop in propiedades)
            {
                if (prop.ubicacion == ubicacion)
                {
                    error.codigoError = -1;
                    error.descripcionError = "Ya se encuentra registrado un inmueble con la Ubicación ingresada.";
                    return error;
                }
            }
            TransferenciaPropiedad propiedad = new TransferenciaPropiedad();
            propiedad.propiedadId = Guid.NewGuid().ToString();
            propiedad.fechaRegistro = DateTime.UtcNow;
            propiedad.tipoMonedaId = tipoMoneda;
            propiedad.tipoConstruccionId = tipoConstruccion;
            propiedad.tipoPropiedadId = tipoPropiedad;
            propiedad.tipoPublicanteId = tipoPublicante;
            propiedad.ImagenPropiedad = new List<TransferenciaImagenPropiedad>();
            //publicacion.Caracteristicas = new List<TransferenciaPropiedadCaracteristica>();
            // editado para la nueva modelacion
            //propiedad.PropiedadCaracteristica = new List<TransferenciaPropiedadCaracteristica>();

            propiedad.PropiedadTipoAmbiente = new List<TransferenciaPropiedadTipoAmbiente>();
            propiedad.superficieCubierta = superficieCubierta;
            propiedad.superficieTerreno = superficieTerreno;
            propiedad.ubicacion = ubicacion;
            propiedad.latitud = respuestaGoogle.google.latitud;
            propiedad.longitud = respuestaGoogle.google.longitud;
            propiedad.pais = respuestaGoogle.google.pais;
            propiedad.AreaAdmNivel1 = respuestaGoogle.google.areaAdministrativaNivel1;
            propiedad.AreaAdmNivel2 = respuestaGoogle.google.areaAdministrativaNivel2;
            propiedad.amueblado = amueblado;
            propiedad.importeExpensasUltimoMes = importeExpensasUltimoMes;
            propiedad.usuarioId = usuarioId;
            propiedad.precioPropiedad = precioPropiedad;
            propiedad.añosAntiguedad = añosAntiguedad;
            propiedad.nroPisos = nroPisos;
            propiedad.descripcionPropiedad = reseña;

            foreach (var imagen in rutasImagenes) {

                TransferenciaImagenPropiedad imagenPropiedad = new TransferenciaImagenPropiedad();
                imagenPropiedad.ImagenPropiedadId = Guid.NewGuid().ToString();
                imagenPropiedad.activo = true;
                imagenPropiedad.rutaImagenPropiedad = imagen;
                imagenPropiedad.propiedadId = propiedad.propiedadId;
                propiedad.ImagenPropiedad.Add(imagenPropiedad);
            }

            // editar para la nueva modelacion 
            List<TransferenciaPropiedadCaracteristica> caracteristicas=new  List<TransferenciaPropiedadCaracteristica>();  
            foreach (var extra in extras)
            {

                TransferenciaPropiedadCaracteristica propiedadCaracteristica = new TransferenciaPropiedadCaracteristica();
                var transferenciaPropiedadCaracteristica = clienteHttp.GetAsync("api/TipoPropiedadCaracteristica/ObtenerPorIDdePropiedadCaracteristica?id=" + extra+ "&idPropiedad="+tipoPropiedad);
                transferenciaPropiedadCaracteristica.Wait();
                if (!transferenciaPropiedadCaracteristica.Result.IsSuccessStatusCode)
                {
                    throw new Exception(transferenciaPropiedadCaracteristica.Result.StatusCode.ToString());
                }
                propiedadCaracteristica = transferenciaPropiedadCaracteristica.Result.Content.ReadAsAsync<TransferenciaPropiedadCaracteristica>().Result;
                
                caracteristicas.Add(propiedadCaracteristica);
            }

            //foreach (var extra in extras) {
            //    TransferenciaPropiedadCaracteristica propiedadCaracteristica = new TransferenciaPropiedadCaracteristica();
            //    propiedadCaracteristica.tipoPropiedadCaracteristicaID = Guid.NewGuid().ToString();
            //  //  propiedadCaracteristica.activo = true;
            //    propiedadCaracteristica.TipopropiedadId = propiedad.propiedadId;
            //    //propiedadCaracteristica.caracteristicas.Add( );
            //    propiedad.PropiedadCaracteristica.Add(propiedadCaracteristica);
            //}

            var tareaObtenerTiposAmbientes = clienteHttp.GetAsync("api/TipoAmbiente/obtenerTiposAmbientes");
            tareaObtenerTiposAmbientes.Wait();
            if (!tareaObtenerTiposAmbientes.Result.IsSuccessStatusCode) {
                throw new Exception(tareaObtenerDuplicado.Result.StatusCode.ToString());
            }
            List<TipoAmbiente> tiposAmbientes = tareaObtenerTiposAmbientes.Result.Content.ReadAsAsync<List<TipoAmbiente>>().Result;
            foreach (var tipoAmbiente in tiposAmbientes) {
                TransferenciaPropiedadTipoAmbiente propiedadTipoAmbiente = new TransferenciaPropiedadTipoAmbiente();
                switch (tipoAmbiente.nombreTipoAmbiente) {
                    case "Dormitorios":
                        
                        propiedadTipoAmbiente.propiedadTipoAmbienteId = Guid.NewGuid().ToString();
                        propiedadTipoAmbiente.activo = true;
                        propiedadTipoAmbiente.cantidad = cantidadDormitorios;
                        propiedadTipoAmbiente.propiedadId = propiedad.propiedadId;
                        propiedadTipoAmbiente.tipoAmbienteId = tipoAmbiente.tipoAmbienteId;
                        propiedad.PropiedadTipoAmbiente.Add(propiedadTipoAmbiente);
                        break;
                    case "Baños":
                        
                        propiedadTipoAmbiente.propiedadTipoAmbienteId = Guid.NewGuid().ToString();
                        propiedadTipoAmbiente.activo = true;
                        propiedadTipoAmbiente.cantidad = cantidadBaños;
                        propiedadTipoAmbiente.propiedadId = propiedad.propiedadId;
                        propiedadTipoAmbiente.tipoAmbienteId = tipoAmbiente.tipoAmbienteId;
                        propiedad.PropiedadTipoAmbiente.Add(propiedadTipoAmbiente);
                        break;
                    case "Cocheras":
                        propiedadTipoAmbiente.propiedadTipoAmbienteId = Guid.NewGuid().ToString();
                        propiedadTipoAmbiente.activo = true;
                        propiedadTipoAmbiente.cantidad = cantidadCocheras;
                        propiedadTipoAmbiente.propiedadId = propiedad.propiedadId;
                        propiedadTipoAmbiente.tipoAmbienteId = tipoAmbiente.tipoAmbienteId;
                        propiedad.PropiedadTipoAmbiente.Add(propiedadTipoAmbiente);
                        break;
                    case "Ambientes":
                        propiedadTipoAmbiente.propiedadTipoAmbienteId = Guid.NewGuid().ToString();
                        propiedadTipoAmbiente.activo = true;
                        propiedadTipoAmbiente.cantidad = cantidadAmbientes;
                        propiedadTipoAmbiente.propiedadId = propiedad.propiedadId;
                        propiedadTipoAmbiente.tipoAmbienteId = tipoAmbiente.tipoAmbienteId;
                        propiedad.PropiedadTipoAmbiente.Add(propiedadTipoAmbiente);
                        break;
                }
            }

            TransferenciaPublicacion publicacion = new TransferenciaPublicacion();
            publicacion.publicacionId = Guid.NewGuid().ToString();
            publicacion.estado = (int)CodigosEstados.Estados.activa;
            publicacion.fechaInicioPublicacion = DateTime.UtcNow;
            publicacion.fechaFinPublicacion = publicacion.fechaInicioPublicacion.AddDays(14);
            publicacion.tipoPublicacionId = tipoPublicacion;
            publicacion.propiedadId = propiedad.propiedadId;
            publicacion.Propiedad = propiedad;
            publicacion.Caracteristicas = new List<TransferenciaPublicacionCaracteristica>();
            #region extras
            //publicacion.Caracteristicas = caracteristicas;
            foreach (var ex in extras)
            {
                TransferenciaPublicacionCaracteristica publicacioncaracteristica = new TransferenciaPublicacionCaracteristica();
                
                publicacioncaracteristica.PublicacionCaracteristicaId = Guid.NewGuid().ToString();
                publicacioncaracteristica.CaracteristicaId = ex;
               
                publicacioncaracteristica.PublicacionId = publicacion.publicacionId;
                 publicacioncaracteristica.Caracteristica= caracteristicas[extras.IndexOf(ex)].caracteristicas; 
                publicacion.Caracteristicas.Add(publicacioncaracteristica);
            }
            #endregion
            var tareaCrearPublicacion = clienteHttp.PostAsJsonAsync<TransferenciaPublicacion>("api/Publicacion/crearPublicacion", publicacion);
            tareaCrearPublicacion.Wait();
            if (!tareaCrearPublicacion.Result.IsSuccessStatusCode) {
                throw new Exception(tareaCrearPublicacion.Result.StatusCode.ToString());
            }
            ExpertoUsuarios.RegistrarActividad(usuarioId,"Creó una publicación con ubicación en: "+propiedad.ubicacion);

            return error;
        }
        public static (ErrorPropy,DTOEditarPublicacion) ObtenerDatosEditarPublicacion(string publicacionId,string usuarioId) {
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            ErrorPropy error = new ErrorPropy();

            (ErrorPropy error, TransferenciaPublicacion publicacion) respuesta = ObtenerPublicacion(publicacionId, clienteHttp);
            if (respuesta.error.codigoError != 0)
            {
                error = respuesta.error;
                return (error,null);
            }
            DTOEditarPublicacion datosPrevios = new DTOEditarPublicacion();
            datosPrevios.publicacionId = respuesta.publicacion.publicacionId;
            datosPrevios.tipoPublicacionElegidaAnteriormente = respuesta.publicacion.TipoPublicacion.nombreTipoPublicacion;
            // se quita por el nuevo modelado
            //datosPrevios.tipoPublicanteElegidoAnteriormente = respuesta.publicacion.Propiedad.TipoPublicante.nombreTipoPublicante;
            int cout = 0;
            //foreach (var tipo in respuesta.publicacion.Propiedad.TipoPropiedad) { 
            datosPrevios.tipoPropiedadElegidaAnteriormente = respuesta.publicacion.Propiedad.tipoPropiedadId;
            //    cout++;
            //}
            //datosPrevios.tipoConstruccionElegidaAnteriormente = respuesta.publicacion.Propiedad.TipoConstruccion.nombreTipoConstruccion;
            datosPrevios.precioPropiedadAnterior = Convert.ToInt64(respuesta.publicacion.Propiedad.precioPropiedad);
            datosPrevios.tipoMonedaElegidaAnteriormente = respuesta.publicacion.Propiedad.TipoMoneda.denominacionMoneda;
            datosPrevios.supCubiertaElegidaAnteriormente = respuesta.publicacion.Propiedad.superficieCubierta;
            datosPrevios.supTerrenoElegidaAnteriormente = respuesta.publicacion.Propiedad.superficieTerreno;
            datosPrevios.añosAntiguedadElegidosAnteriormente = respuesta.publicacion.Propiedad.añosAntiguedad;
            datosPrevios.nroPisosElegidosAnteriormente = respuesta.publicacion.Propiedad.nroPisos;
            datosPrevios.importeExpensasAnterior = respuesta.publicacion.Propiedad.importeExpensasUltimoMes;
            datosPrevios.ubicacionAnterior = respuesta.publicacion.Propiedad.ubicacion;
            datosPrevios.amueblado = respuesta.publicacion.Propiedad.amueblado;
            datosPrevios.reseñaAnterior = respuesta.publicacion.Propiedad.descripcionPropiedad;
            datosPrevios.imagenesAnteriores = new List<DTOImagenAnterior>();

            foreach (var imagenAnterior in respuesta.publicacion.Propiedad.ImagenPropiedad) {
                if (imagenAnterior.activo==true) {
                    DTOImagenAnterior objImagenAnterior = new DTOImagenAnterior();
                    objImagenAnterior.imagenAnteriorId = imagenAnterior.ImagenPropiedadId;
                    objImagenAnterior.rutaImagen = imagenAnterior.rutaImagenPropiedad;
                    datosPrevios.imagenesAnteriores.Add(objImagenAnterior);
                }
                
            }


            foreach (var tipoAmbiente in respuesta.publicacion.Propiedad.PropiedadTipoAmbiente) {
                if (tipoAmbiente.activo==true) {
                    switch (tipoAmbiente.TipoAmbiente.nombreTipoAmbiente)
                    {
                        case "Dormitorios":
                            datosPrevios.cantidadDormitorioesElegidosAnteriormente = tipoAmbiente.cantidad;
                            break;
                        case "Baños":
                            datosPrevios.cantidadBañosElegidosAnteriormente = tipoAmbiente.cantidad;
                            break;
                        case "Ambientes":
                            datosPrevios.cantidadAmbientesElegidosAnteriormente = tipoAmbiente.cantidad;
                            break;
                        case "Cocheras":
                            datosPrevios.cantidadCocherasElegidasAnteriormente = tipoAmbiente.cantidad;
                            break;
                    }
                }
                
            }
            //editar edicion de extras solo se elimina esta parte
            datosPrevios.extrasElegidosAnteriormente = new List<string>();
            cout = 0;
            ////foreach (var extra in respuesta.publicacion.Propiedad.PropiedadCaracteristica) {
            ////    if (extra.activo==true) {
            ////        datosPrevios.extrasElegidosAnteriormente.Add(extra.tipoPropiedadCaracteristicaID);
            ////        cout++;
            ////    }
                
            //}
            (ErrorPropy error, DTOCrearPublicacion datosEditarPublicacion) respuesta2 = ObtenerDatosCrearPublicacion(usuarioId);
            if (respuesta2.error.codigoError!=0) {
                error = respuesta2.error;
                return (error,null);
            }
            datosPrevios.listadoCaracteristicas = respuesta2.datosEditarPublicacion.listadoCaracteristicas;
            datosPrevios.listadoTiposAmbientes = respuesta2.datosEditarPublicacion.listadoTiposAmbientes;
            datosPrevios.listadoTiposConstruccion = respuesta2.datosEditarPublicacion.listadoTiposConstruccion;
            datosPrevios.listadoTiposMonedas = respuesta2.datosEditarPublicacion.listadoTiposMonedas;
            datosPrevios.listadoTiposPropiedades = respuesta2.datosEditarPublicacion.listadoTiposPropiedades;
            datosPrevios.listadoTiposPublicaciones = respuesta2.datosEditarPublicacion.listadoTiposPublicaciones;
            datosPrevios.listadoTiposPublicantes = respuesta2.datosEditarPublicacion.listadoTiposPublicantes;
            return (error,datosPrevios);
        }
        public static (ErrorPropy, TransferenciaPublicacion) ObtenerPublicacionReducida(string publicacionId, HttpClient clienteHttp)
        {
            ErrorPropy error = new ErrorPropy();
            var tareaObtenerPublicacion = clienteHttp.GetAsync("api/Publicacion/obtenerPublicacionPorIDReducida/" + publicacionId);
            tareaObtenerPublicacion.Wait();
            if (!tareaObtenerPublicacion.Result.IsSuccessStatusCode)
            {
                throw new Exception(tareaObtenerPublicacion.Result.StatusCode.ToString());
            }
            TransferenciaPublicacion publicacion = tareaObtenerPublicacion.Result.Content.ReadAsAsync<TransferenciaPublicacion>().Result;
            return (error, publicacion);
        }
        public static (ErrorPropy, TransferenciaPublicacion) ObtenerPublicacion(string publicacionId,HttpClient clienteHttp) {
            ErrorPropy error = new ErrorPropy();
            var tareaObtenerPublicacion = clienteHttp.GetAsync("api/Publicacion/obtenerPublicacionPorId/" + publicacionId);
            tareaObtenerPublicacion.Wait();
            if (!tareaObtenerPublicacion.Result.IsSuccessStatusCode)
            {
                throw new Exception(tareaObtenerPublicacion.Result.StatusCode.ToString());
            }
            TransferenciaPublicacion publicacion = tareaObtenerPublicacion.Result.Content.ReadAsAsync<TransferenciaPublicacion>().Result;
            return (error, publicacion);
        }
        public static ErrorPropy EditarPublicacion(string publicacionId,string ubicacion,string tipoPropiedad,string tipoPublicacion,string tipoConstruccion,int supTerreno,int supCubierta,int cantidadDormitorios,int cantidadCocheras,int cantidadBaños,int cantidadAmbientes,int añosAntiguedad,decimal importeExpensasUltimoMes,bool amueblado,int nroPisos,decimal precioPropiedad, string tipoMoneda,List<string> extras,string reseña,List<string> imagenesDescartadas,List<string> nuevasImagenes) {
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            ErrorPropy error = new ErrorPropy();
            
            (ErrorPropy error,TransferenciaPublicacion publicacion) respuesta = ObtenerPublicacion(publicacionId,clienteHttp);
            if (respuesta.error.codigoError!=0) {
                error = respuesta.error;
                return error;
            }
            if (!String.IsNullOrEmpty(ubicacion)) {
                (ErrorPropy error, DTOUbicacionGoogle google) respuestaGoogle = ExpertoComunicacionExterna.ObtenerParametrosUbicacionGoogleMaps(ubicacion, clienteHttp,false);
                if (respuestaGoogle.error.codigoError!=0) {
                    error = respuestaGoogle.error;
                    return error;
                }
                respuesta.publicacion.Propiedad.latitud = respuestaGoogle.google.latitud;
                respuesta.publicacion.Propiedad.longitud = respuestaGoogle.google.longitud;
                respuesta.publicacion.Propiedad.ubicacion = ubicacion;
                respuesta.publicacion.Propiedad.pais = respuestaGoogle.google.pais;
                respuesta.publicacion.Propiedad.AreaAdmNivel1 = respuestaGoogle.google.areaAdministrativaNivel1;
                respuesta.publicacion.Propiedad.AreaAdmNivel2 = respuestaGoogle.google.areaAdministrativaNivel2;
            }
            int cantidadImagenes = 0;
            foreach (var imagen in respuesta.publicacion.Propiedad.ImagenPropiedad) {
                if (imagen.activo==true) {
                    cantidadImagenes++;
                }
            }
            if (cantidadImagenes==imagenesDescartadas.Count && nuevasImagenes.Count==0) {
                error.codigoError = -1;
                error.descripcionError = "Debe agregar al menos una imagen de su inmueble";
                return error;
            }
            if (imagenesDescartadas.Count>0) {
                foreach (var imagenDescartada in imagenesDescartadas)
                {
                    foreach (var imagen in respuesta.publicacion.Propiedad.ImagenPropiedad)
                    {
                        if (imagen.ImagenPropiedadId == imagenDescartada)
                        {
                            imagen.activo = false;
                        }
                    }
                }
            }
            foreach (var imagenNueva in nuevasImagenes) {
                TransferenciaImagenPropiedad imagen = new TransferenciaImagenPropiedad();
                imagen.ImagenPropiedadId = Guid.NewGuid().ToString();
                imagen.rutaImagenPropiedad = imagenNueva;
                imagen.propiedadId = respuesta.publicacion.propiedadId;
                imagen.activo = true;
                respuesta.publicacion.Propiedad.ImagenPropiedad.Add(imagen);
            }
            

            respuesta.publicacion.tipoPublicacionId = tipoPublicacion;
            respuesta.publicacion.Propiedad.tipoPropiedadId = tipoPropiedad;
            respuesta.publicacion.Propiedad.tipoConstruccionId = tipoConstruccion;
            //respuesta.publicacion.Propiedad.tipoPublicanteId = tipoPublicante;
            respuesta.publicacion.Propiedad.precioPropiedad = precioPropiedad;
            respuesta.publicacion.Propiedad.tipoMonedaId = tipoMoneda;
            respuesta.publicacion.Propiedad.añosAntiguedad = añosAntiguedad;
            respuesta.publicacion.Propiedad.importeExpensasUltimoMes = importeExpensasUltimoMes;
            respuesta.publicacion.Propiedad.amueblado = amueblado;
            respuesta.publicacion.Propiedad.superficieTerreno = supTerreno;
            respuesta.publicacion.Propiedad.superficieCubierta = supCubierta;
            respuesta.publicacion.Propiedad.nroPisos = nroPisos;
            respuesta.publicacion.Propiedad.descripcionPropiedad = reseña;

            //foreach (var caracteristiaAnterior in respuesta.publicacion.Propiedad.PropiedadCaracteristica) {
            //    caracteristiaAnterior.activo = false;
            //}

            foreach (var caracteristicaAgregada in extras) {
                TransferenciaPropiedadCaracteristica propiedadCaracteristica = new TransferenciaPropiedadCaracteristica();
                propiedadCaracteristica.tipoPropiedadCaracteristicaID = Guid.NewGuid().ToString();
               // propiedadCaracteristica.caracteristicaId.Add( caracteristicaAgregada);
                propiedadCaracteristica.TipopropiedadId = respuesta.publicacion.propiedadId;
                propiedadCaracteristica.activo = true;
                
                
                //comprobar si no estan de lo contrario


                //respuesta.publicacion.Propiedad.PropiedadCaracteristica.Add(propiedadCaracteristica);
                
                
            }
            
            foreach (var tipoAmbienteAnterior in respuesta.publicacion.Propiedad.PropiedadTipoAmbiente) {
                tipoAmbienteAnterior.activo = false;
            }
            var tareaObtenerTiposAmbientes = clienteHttp.GetAsync("api/TipoAmbiente/obtenerTiposAmbientes");
            tareaObtenerTiposAmbientes.Wait();
            if (!tareaObtenerTiposAmbientes.Result.IsSuccessStatusCode)
            {
                throw new Exception(tareaObtenerTiposAmbientes.Result.StatusCode.ToString());
            }
            List<TipoAmbiente> tiposAmbientes = tareaObtenerTiposAmbientes.Result.Content.ReadAsAsync<List<TipoAmbiente>>().Result;
            foreach (var tipoAmbiente in tiposAmbientes)
            {
                TransferenciaPropiedadTipoAmbiente propiedadTipoAmbiente = new TransferenciaPropiedadTipoAmbiente();
                switch (tipoAmbiente.nombreTipoAmbiente)
                {
                    case "Dormitorios":

                        propiedadTipoAmbiente.propiedadTipoAmbienteId = Guid.NewGuid().ToString();
                        propiedadTipoAmbiente.activo = true;
                        propiedadTipoAmbiente.cantidad = cantidadDormitorios;
                        propiedadTipoAmbiente.propiedadId = respuesta.publicacion.propiedadId;
                        propiedadTipoAmbiente.tipoAmbienteId = tipoAmbiente.tipoAmbienteId;
                        respuesta.publicacion.Propiedad.PropiedadTipoAmbiente.Add(propiedadTipoAmbiente);
                        break;
                    case "Baños":

                        propiedadTipoAmbiente.propiedadTipoAmbienteId = Guid.NewGuid().ToString();
                        propiedadTipoAmbiente.activo = true;
                        propiedadTipoAmbiente.cantidad = cantidadBaños;
                        propiedadTipoAmbiente.propiedadId = respuesta.publicacion.propiedadId;
                        propiedadTipoAmbiente.tipoAmbienteId = tipoAmbiente.tipoAmbienteId;
                        respuesta.publicacion.Propiedad.PropiedadTipoAmbiente.Add(propiedadTipoAmbiente);
                        break;
                    case "Cocheras":
                        propiedadTipoAmbiente.propiedadTipoAmbienteId = Guid.NewGuid().ToString();
                        propiedadTipoAmbiente.activo = true;
                        propiedadTipoAmbiente.cantidad = cantidadCocheras;
                        propiedadTipoAmbiente.propiedadId = respuesta.publicacion.propiedadId;
                        propiedadTipoAmbiente.tipoAmbienteId = tipoAmbiente.tipoAmbienteId;
                        respuesta.publicacion.Propiedad.PropiedadTipoAmbiente.Add(propiedadTipoAmbiente);
                        break;
                    case "Ambientes":
                        propiedadTipoAmbiente.propiedadTipoAmbienteId = Guid.NewGuid().ToString();
                        propiedadTipoAmbiente.activo = true;
                        propiedadTipoAmbiente.cantidad = cantidadAmbientes;
                        propiedadTipoAmbiente.propiedadId = respuesta.publicacion.propiedadId;
                        propiedadTipoAmbiente.tipoAmbienteId = tipoAmbiente.tipoAmbienteId;
                        respuesta.publicacion.Propiedad.PropiedadTipoAmbiente.Add(propiedadTipoAmbiente);
                        break;
                }
            }


            var tareaEditarPublicacion = clienteHttp.PostAsJsonAsync<TransferenciaPublicacion>("api/Publicacion/editarPublicacion",respuesta.publicacion);
            tareaEditarPublicacion.Wait();
            if (!tareaEditarPublicacion.Result.IsSuccessStatusCode) {
                throw new Exception(tareaEditarPublicacion.Result.StatusCode.ToString());
            }
            ExpertoUsuarios.RegistrarActividad(respuesta.publicacion.Propiedad.usuarioId,"Editó un inmueble con ubicación en "+respuesta.publicacion.Propiedad.ubicacion);
            return error;
            
        }
        public static (ErrorPropy,DTOCrearPublicacion) ObtenerDatosCrearPublicacion(string usuarioId) {
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            ErrorPropy error = new ErrorPropy();
            DTOCrearPublicacion datosCrearPublicacion = new DTOCrearPublicacion();
            (ErrorPropy error,TransferenciaPlanUsuario plan) respuestaObtenerPlanUsuario = ExpertoUsuarios.ObtenerPlanActivoUsuario(usuarioId,clienteHttp);
            if (respuestaObtenerPlanUsuario.plan==null) {
                error.codigoError = -2;
                error.descripcionError = "Debe contratar un plan para poder realizar publiaciones.";
                return (error, null);
            }
            if (respuestaObtenerPlanUsuario.plan.cantidadCreditosActivos<=0) {
                error.codigoError = -2;
                error.descripcionError = "Usted no cuenta con créditos suficientes para realizar esta operación. Por favor coontrate más créditos a través de su panel de control.";
                return (error, null);
            }
            if (respuestaObtenerPlanUsuario.error.codigoError!=0) {
                return (respuestaObtenerPlanUsuario.error, null);
            }
            datosCrearPublicacion.cantidadImagenesDisponibles = respuestaObtenerPlanUsuario.plan.Plan.cantidadMaxImagenesPermitidasPorPublicacion;

            #region Obtener listado de tipos de publicantes disponibles en la bd
            var tareaObtenerTiposPublicantes = clienteHttp.GetAsync("api/TipoPublicante/obtenerTiposPublicantes");
            tareaObtenerTiposPublicantes.Wait();
            if (!tareaObtenerTiposPublicantes.Result.IsSuccessStatusCode)
            {
                throw new Exception(tareaObtenerTiposPublicantes.Result.StatusCode.ToString());
            }
            List<TransferenciaTipoPublicante> tiposPublicantes = tareaObtenerTiposPublicantes.Result.Content.ReadAsAsync<List<TransferenciaTipoPublicante>>().Result;
            foreach (var tipoPublicante in tiposPublicantes)
            {
                DTOTipoPublicante objTipoPublicante = new DTOTipoPublicante();
                objTipoPublicante.tipoPublicanteId = tipoPublicante.tipoPublicanteId;
                objTipoPublicante.nombreTipoPublicante = tipoPublicante.nombreTipoPublicante;
                datosCrearPublicacion.listadoTiposPublicantes.Add(objTipoPublicante);
            }
            #endregion

            #region Obtener listado de tipos de publicaciones disponibles en la bd
            var tareaObtenerTiposPublicaciones = clienteHttp.GetAsync("api/TipoPublicacion/obtenerTiposPublicaciones");
            tareaObtenerTiposPublicaciones.Wait();
            if (!tareaObtenerTiposPublicaciones.Result.IsSuccessStatusCode)
            {
                throw new Exception(tareaObtenerTiposPublicantes.Result.StatusCode.ToString());
            }
            List<TransferenciaTipoPublicacion> tiposPublicaciones = tareaObtenerTiposPublicaciones.Result.Content.ReadAsAsync<List<TransferenciaTipoPublicacion>>().Result;
            foreach (var tipoPublicacion in tiposPublicaciones) {
                DTOTipoPublicacion objTipoPublicacion = new DTOTipoPublicacion();
                objTipoPublicacion.tipoPublicacionId = tipoPublicacion.tipoPublicacionId;
                objTipoPublicacion.nombreTipoPublicacion = tipoPublicacion.nombreTipoPublicacion;
                datosCrearPublicacion.listadoTiposPublicaciones.Add(objTipoPublicacion);
            }
            #endregion

            #region Obtener listado de tipos de propiedades disponibles de la bd
            var tareaObtenerTiposPropiedades = clienteHttp.GetAsync("api/TipoPropiedad/obtenerTiposPropiedades");
            tareaObtenerTiposPropiedades.Wait();
            if (!tareaObtenerTiposPropiedades.Result.IsSuccessStatusCode)
            {
                throw new Exception(tareaObtenerTiposPropiedades.Result.StatusCode.ToString());
            }
            List<DTOTipoPropiedad> tiposPropiedades = tareaObtenerTiposPropiedades.Result.Content.ReadAsAsync<List<DTOTipoPropiedad>>().Result;
            foreach (var tipoPropiedad in tiposPropiedades) {
                DTOTipoPropiedad objTipoPropiedad = new DTOTipoPropiedad();
                objTipoPropiedad.tipoPropiedadId = tipoPropiedad.tipoPropiedadId;
                objTipoPropiedad.nombreTipoPropiedad = tipoPropiedad.nombreTipoPropiedad;
                datosCrearPublicacion.listadoTiposPropiedades.Add(objTipoPropiedad);
            }
            #endregion

            #region Obtener listado de tipos de construcciones disponibles de la bd
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
                datosCrearPublicacion.listadoTiposConstruccion.Add(objTipoConstruccion);
            }
            #endregion

            #region Obtener listado de caracteristicas (extras)
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
                datosCrearPublicacion.listadoCaracteristicas.Add(objCaracteristica);
            }
            #endregion

            #region Obtener listado de monedas vigentes
            var tareaObtenerTiposMonedas = clienteHttp.GetAsync("api/TipoMoneda/obtenerTiposMonedas");
            tareaObtenerTiposMonedas.Wait();
            if (!tareaObtenerTiposMonedas.Result.IsSuccessStatusCode) {
                throw new Exception(tareaObtenerTiposMonedas.Result.StatusCode.ToString());
            }
            List<TransferenciaTipoMoneda> tiposMonedas = tareaObtenerTiposMonedas.Result.Content.ReadAsAsync<List<TransferenciaTipoMoneda>>().Result;
            foreach (var tipoMoneda in tiposMonedas) {
                DTOTipoMoneda objTipoMoneda = new DTOTipoMoneda();
                objTipoMoneda.tipoMonedaId = tipoMoneda.tipoMonedaId;
                objTipoMoneda.denominacionMoneda = tipoMoneda.denominacionMoneda;
                datosCrearPublicacion.listadoTiposMonedas.Add(objTipoMoneda);
            }
            #endregion

            #region Obtener listado de tipos de ambientes
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
                datosCrearPublicacion.listadoTiposAmbientes.Add(objTipoAmbiente);
            }
            #endregion

            

            return (error,datosCrearPublicacion);


        }
        public static ErrorPropy EliminarPublicacion(string publicacionId) {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            (ErrorPropy error,TransferenciaPublicacion publicacion) respuesta = ObtenerPublicacion(publicacionId,clienteHttp);
            if (respuesta.error.codigoError!=0) {
                error = respuesta.error;
                return error;
            }
            respuesta.publicacion.estado = (int)CodigosEstados.Estados.inactivaPorUsuario;
            var tareaEliminarPublicacion = clienteHttp.PostAsJsonAsync<TransferenciaPublicacion>("api/Publicacion/eliminarPublicacion", respuesta.publicacion);
            tareaEliminarPublicacion.Wait();
            if (!tareaEliminarPublicacion.Result.IsSuccessStatusCode) {
                throw new Exception(tareaEliminarPublicacion.Result.StatusCode.ToString());
            }
            ExpertoUsuarios.RegistrarActividad(respuesta.publicacion.Propiedad.usuarioId,"Eliminó un inmueble con ubicación en "+respuesta.publicacion.Propiedad.ubicacion);
            return error;
        }
    }
}
