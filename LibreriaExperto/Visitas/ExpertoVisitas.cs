using LibreriaClases;
using LibreriaClases.DTO;
using LibreriaClases.Transferencia;
using LibreriaExperto.Mensajeria;
using LibreriaExperto.Publicaciones;
using LibreriaExperto.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaExperto.Visitas
{
    public static class ExpertoVisitas
    {
        public static async Task<(ErrorPropy, DTOContactoVisitante)> ObtenerContactoVisitante(string usuarioIdVisita,string IDUsuarioPublicante,string publicacionId) {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            DTOContactoVisitante datosVisitante = new DTOContactoVisitante();
            var tareaObtenerUsuarioVisitante = clienteHttp.GetAsync("api/Usuario/obtenerUsuarioPorID/" +usuarioIdVisita);
            
            tareaObtenerUsuarioVisitante.Wait();
            if (!tareaObtenerUsuarioVisitante.Result.IsSuccessStatusCode) {
                error.codigoError = (int)tareaObtenerUsuarioVisitante.Result.StatusCode;
                error.descripcionError = "Error: " + error.codigoError + " " + tareaObtenerUsuarioVisitante.Result.StatusCode;
                return (error, null);
            }
            TransferenciaUsuario usuarioVisitante = tareaObtenerUsuarioVisitante.Result.Content.ReadAsAsync<TransferenciaUsuario>().Result;
            var tareaObtenerSolicitudContactoVisitante = clienteHttp.GetAsync("api/Visita/obtenerSolicitudContactoVisitante/" + publicacionId + "&" + usuarioIdVisita);
            tareaObtenerSolicitudContactoVisitante.Wait();
            if (!tareaObtenerSolicitudContactoVisitante.Result.IsSuccessStatusCode) {
                error.codigoError = (int)tareaObtenerSolicitudContactoVisitante.Result.StatusCode;
                error.descripcionError = "Error: " + error.codigoError + " " + tareaObtenerSolicitudContactoVisitante.Result.StatusCode;
                return (error, null);
            }
            TransferenciaSolicitudContactoVisitante solicitudPrevia = tareaObtenerSolicitudContactoVisitante.Result.Content.ReadAsAsync<TransferenciaSolicitudContactoVisitante>().Result;
            if (solicitudPrevia == null)//Si es null significa que es la primera vez que el publicante quiere contactar al visitante.
            {
                var tareaObtenerUsuarioPublicante = clienteHttp.GetAsync("api/Usuario/obtenerUsuarioPorID/"+IDUsuarioPublicante);
                tareaObtenerUsuarioPublicante.Wait();
                if (!tareaObtenerUsuarioPublicante.Result.IsSuccessStatusCode) {
                    error.codigoError = (int)tareaObtenerUsuarioPublicante.Result.StatusCode;
                    error.descripcionError = "Error: " + error.codigoError + " " + tareaObtenerUsuarioPublicante.Result.StatusCode;
                    return (error, null);
                }
                TransferenciaUsuario usuarioPublicante = tareaObtenerUsuarioPublicante.Result.Content.ReadAsAsync<TransferenciaUsuario>().Result;
                TransferenciaPlanUsuario planActivoUsuarioPublicante = usuarioPublicante.PlanUsuario.Where(x => x.activo == true).FirstOrDefault();
                
                if (planActivoUsuarioPublicante.cantidadCreditosActivos <= 0) {
                    await ExpertoMensajeria.EnviarMailAvisoCreditosAgotados(usuarioPublicante.email);
                    error.codigoError = -1;
                    error.descripcionError = "Usted no disponde de créditos suficientes para realizar la operación.";
                    return (error, null);
                }
                else {
                    planActivoUsuarioPublicante.cantidadCreditosActivos--;
                }
                TransferenciaSolicitudContactoVisitante solicitudContactoPublicante = new TransferenciaSolicitudContactoVisitante();
                solicitudContactoPublicante.solicitudContactoVisitanteId = Guid.NewGuid().ToString();
                solicitudContactoPublicante.fechaSolicitud = DateTime.UtcNow;
                solicitudContactoPublicante.cantidadVecesRealizoSolicitud = 1;
                solicitudContactoPublicante.publicacionId = publicacionId;
                solicitudContactoPublicante.usuarioId = usuarioIdVisita;
                TransferenciaNoPersistidoCrearSolicitudContactoVisitante datosCrearSolicitudContactoVisitante = new TransferenciaNoPersistidoCrearSolicitudContactoVisitante();
                datosCrearSolicitudContactoVisitante.planUsuario = planActivoUsuarioPublicante;
                datosCrearSolicitudContactoVisitante.solicitudContactoVisitante = solicitudContactoPublicante;
                var tareaCrearSolicitudContactoVisitante = clienteHttp.PostAsJsonAsync<TransferenciaNoPersistidoCrearSolicitudContactoVisitante>("api/Visita/crearSolicitudContactoVisitante", datosCrearSolicitudContactoVisitante);
                tareaCrearSolicitudContactoVisitante.Wait();
                if (!tareaCrearSolicitudContactoVisitante.Result.IsSuccessStatusCode) {
                    error.codigoError = (int)tareaCrearSolicitudContactoVisitante.Result.StatusCode;
                    error.descripcionError = "Error: " + error.codigoError + " " + tareaCrearSolicitudContactoVisitante.Result.StatusCode;
                    return (error, null);
                }
                
                

            }
            else {//Sino significa que el publicante ya ha solicitado los datos de contacto del visitante.
                solicitudPrevia.cantidadVecesRealizoSolicitud++;
                solicitudPrevia.fechaSolicitud = DateTime.UtcNow;
                var tareaEditarSolicitudContactoVisitante = clienteHttp.PostAsJsonAsync<TransferenciaSolicitudContactoVisitante>("api/Visita/editarSolicitudContactoVisitante", solicitudPrevia);
                tareaEditarSolicitudContactoVisitante.Wait();
                if (!tareaEditarSolicitudContactoVisitante.Result.IsSuccessStatusCode) {
                    error.codigoError = (int)tareaEditarSolicitudContactoVisitante.Result.StatusCode;
                    error.descripcionError = "Error: " + error.codigoError + " " + tareaEditarSolicitudContactoVisitante.Result.StatusCode;
                    return (error, null);
                }

            }
            datosVisitante.email = usuarioVisitante.email;
            datosVisitante.nombreVisitante = usuarioVisitante.nombreUsuario + " " + usuarioVisitante.apellidoUsuario;
            if (usuarioVisitante.telefono2 != -1)
            {
                datosVisitante.telefonoContactoAlternativo = String.Format("{0:(###) ### ####}", usuarioVisitante.telefono2);
            }
            else
            {
                datosVisitante.telefonoContactoAlternativo = "-1";
            }
            datosVisitante.telefonoContactoPrincipal = String.Format("{0:(###) ### ####}", usuarioVisitante.telefono1);
            return (error, datosVisitante);
            
        }
        //aca tendria que ir el restar el credito
        public static async Task<(ErrorPropy, DTOContactoPublicante)> ObtenerContactoPublicanteAsync(string publicacionId, string usuarioIdVisita ,int puntosResta) {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            DTOContactoPublicante datosPublicante = new DTOContactoPublicante();
      
            var tareaObtenerPublicacion = clienteHttp.GetAsync("api/Publicacion/obtenerPublicacionPorId/"+publicacionId);
            tareaObtenerPublicacion.Wait();
            if (!tareaObtenerPublicacion.Result.IsSuccessStatusCode) {
                error.codigoError = (int)tareaObtenerPublicacion.Result.StatusCode;
                error.descripcionError = "Error: " + error.codigoError + " " + tareaObtenerPublicacion.Result.StatusCode;
                return(error, null);
            }
            TransferenciaPublicacion publicacion = tareaObtenerPublicacion.Result.Content.ReadAsAsync<TransferenciaPublicacion>().Result;
            TransferenciaVisitaInmueble visita = publicacion.VisitaInmueble.Where(x => x.usuarioId == usuarioIdVisita).FirstOrDefault();
           
            datosPublicante.nombreCompletoPublicante = publicacion.Propiedad.Usuario.nombreUsuario + " " + publicacion.Propiedad.Usuario.apellidoUsuario;
            datosPublicante.email = publicacion.Propiedad.Usuario.email;
            datosPublicante.telefonoContactoPrincipal = String.Format("{0:(###) ### ####}", publicacion.Propiedad.Usuario.telefono1);
            if (publicacion.Propiedad.Usuario.telefono2 != -1)
            {
                datosPublicante.telefonoContactoAlternativo = String.Format("{0:(###) ### ####}", publicacion.Propiedad.Usuario.telefono2);
            }
            else
            {
                datosPublicante.telefonoContactoAlternativo = publicacion.Propiedad.Usuario.telefono2.ToString();
            }
           
                datosPublicante.tipoPublicante = publicacion.TipoPublicacion.nombreTipoPublicacion ;
            #region Restar Crédito 
            (ErrorPropy error, TransferenciaUsuario usuarioPublicante) respuestaObtenerUsuarioPublicante = ExpertoUsuarios.ObtenerUsuarioPorID(publicacion.Propiedad.Usuario.usuarioId, clienteHttp);
            if (respuestaObtenerUsuarioPublicante.error.codigoError != 0)
            {
                error = respuestaObtenerUsuarioPublicante.error;
                return (error, null);
            }
            bool contacto=false ;

            TransferenciaVisitaInmueble visitaInmueble = publicacion.VisitaInmueble.Where(x =>  x.usuarioId == usuarioIdVisita).FirstOrDefault();
            if (visitaInmueble != null)
            {
                
                contacto = visitaInmueble.contactoPublicante;
            }
            // aca se resta el credito
            TransferenciaPlanUsuario planUsuario = respuestaObtenerUsuarioPublicante.usuarioPublicante.PlanUsuario.Where(x => x.activo == true).FirstOrDefault();
            if (contacto == false) { planUsuario.cantidadCreditosActivos = planUsuario.cantidadCreditosActivos - puntosResta; }
           
            if (planUsuario.cantidadCreditosActivos <= 0)
            {
                var tareaEditarPlanUsuario = clienteHttp.PostAsJsonAsync<TransferenciaPlanUsuario>("api/PlanUsuario/editarPlanUsuario", planUsuario);
                if (!tareaEditarPlanUsuario.Result.IsSuccessStatusCode)
                {
                    throw new Exception(tareaEditarPlanUsuario.Result.StatusCode.ToString());
                }
                var tareaObtenerPublicacionesDelUsuario = clienteHttp.GetAsync("api/Publicacion/obtenerPublicacionesPorUsuario/" + respuestaObtenerUsuarioPublicante.usuarioPublicante.usuarioId);
                tareaObtenerPublicacionesDelUsuario.Wait();
                if (!tareaObtenerPublicacionesDelUsuario.Result.IsSuccessStatusCode)
                {
                    throw new Exception(tareaObtenerPublicacionesDelUsuario.Result.StatusCode.ToString());
                }
                List<TransferenciaPublicacion> publicacionesUsuario = tareaObtenerPublicacionesDelUsuario.Result.Content.ReadAsAsync<List<TransferenciaPublicacion>>().Result;
                ExpertoPublicaciones.HabilitarDeshabilitarPublicacionesUsuario(publicacionesUsuario, (int)CodigosEstados.Estados.inactivaPorFaltaDeCreditos, clienteHttp);

                await ExpertoMensajeria.EnviarMailAvisoCreditosAgotados(publicacion.Propiedad.Usuario.email);
                return (error, datosPublicante);
            }
           
            if (visita == null)
            {
                error.codigoError = 666;
                error.descripcionError = "Error: " + error.codigoError + " no se han podido obtener los datos del publicante.";
                return (error, null);
            }
            if (visita.contactoPublicante == false)
            {
                visita.contactoPublicante = true;
                var tareaEditarPlanUsuario = clienteHttp.PostAsJsonAsync<TransferenciaPlanUsuario>("api/PlanUsuario/editarPlanUsuario", planUsuario);
                if (!tareaEditarPlanUsuario.Result.IsSuccessStatusCode)
                {
                    throw new Exception(tareaEditarPlanUsuario.Result.StatusCode.ToString());
                }
                var tareaEditarVisita = clienteHttp.PostAsJsonAsync<TransferenciaVisitaInmueble>("api/Visita/editarVisita", visita);
                if (!tareaEditarVisita.Result.IsSuccessStatusCode)
                {
                    error.codigoError = (int)tareaEditarVisita.Result.StatusCode;
                    error.descripcionError = "Error: " + error.codigoError + " " + tareaEditarVisita.Result.StatusCode;
                    return (error, null);

                }

            }





            #endregion




            return (error, datosPublicante);
        }
        public static async Task<(ErrorPropy, DTOVisitaInmueble)> VisitarInmueble(string publicacionId, string usuarioIdVisita)
        {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            var tareaObtenerPublicacion = clienteHttp.GetAsync("api/Publicacion/obtenerPublicacionPorId/" + publicacionId);
            tareaObtenerPublicacion.Wait();
            if (!tareaObtenerPublicacion.Result.IsSuccessStatusCode)
            {
                error.codigoError = (int)tareaObtenerPublicacion.Result.StatusCode;
                error.descripcionError = "Error: " + error.codigoError + " " + tareaObtenerPublicacion.Result.StatusCode;
                return (error, null);
            }
            TransferenciaPublicacion publicacion = tareaObtenerPublicacion.Result.Content.ReadAsAsync<TransferenciaPublicacion>().Result;
            DTOVisitaInmueble datosInmueble = new DTOVisitaInmueble();
            foreach (var imagen in publicacion.Propiedad.ImagenPropiedad)
            {
                if (imagen.activo == true)
                {
                    datosInmueble.imagenes.Add(imagen.rutaImagenPropiedad);
                }

            }
            foreach (var tipoAmbiente in publicacion.Propiedad.PropiedadTipoAmbiente)
            {
                if (tipoAmbiente.activo == true)
                {
                    switch (tipoAmbiente.TipoAmbiente.nombreTipoAmbiente)
                    {
                        case "Dormitorios":
                            datosInmueble.cantidadDormitorios = tipoAmbiente.cantidad;
                            break;
                        case "Baños":
                            datosInmueble.cantidadBaños = tipoAmbiente.cantidad;
                            break;
                        case "Ambientes":
                            datosInmueble.cantidadAmbientes = tipoAmbiente.cantidad;
                            break;
                        case "Cocheras":
                            datosInmueble.cantidadCocheras = tipoAmbiente.cantidad;
                            break;

                    }
                }
            }

            //foreach (var extra in publicacion.Propiedad.PropiedadCaracteristica) {
            //    if (extra.activo==true) {
            //        datosInmueble.extras.Add(extra.caracteristicas.nombreCaracteristica);

            //    }
            //}
            datosInmueble.precioPropiedad = String.Format("{0:c}", publicacion.Propiedad.precioPropiedad);
            datosInmueble.tipoMoneda = publicacion.Propiedad.TipoMoneda.denominacionMoneda;
            datosInmueble.ubicacionPropiedad = publicacion.Propiedad.ubicacion;
            datosInmueble.publicacionId = publicacion.publicacionId;
            int count = 0;
            //foreach (var c in datosInmueble.tipoPropiedad)
            //{
            //    datosInmueble.tipoPropiedad.Add(publicacion.Propiedad.TipoPropiedad.ElementAt(count).nombreTipoPropiedad);
            //    count++;
            //}
            datosInmueble.tipoConstruccion = publicacion.Propiedad.TipoConstruccion.nombreTipoConstruccion;
            datosInmueble.tipoPublicacion = publicacion.TipoPublicacion.nombreTipoPublicacion;

            datosInmueble.superficieCubierta = publicacion.Propiedad.superficieCubierta;
            datosInmueble.superficieTerreno = publicacion.Propiedad.superficieTerreno;
            datosInmueble.importeExpensasUltimoMes = String.Format("{0:c}", publicacion.Propiedad.importeExpensasUltimoMes);
            datosInmueble.reseña = publicacion.Propiedad.descripcionPropiedad;
            if (publicacion.Propiedad.importeExpensasUltimoMes != 0)
            {
                datosInmueble.inmueblePagaExpensas = true;
            }
            else
            {
                datosInmueble.inmueblePagaExpensas = false;
            }
            datosInmueble.amueblado = publicacion.Propiedad.amueblado;

            #region  Crear Visita
            (ErrorPropy error, TransferenciaUsuario usuarioPublicante) respuestaObtenerUsuarioPublicante = ExpertoUsuarios.ObtenerUsuarioPorID(publicacion.Propiedad.Usuario.usuarioId, clienteHttp);
            if (respuestaObtenerUsuarioPublicante.error.codigoError != 0)
            {
                error = respuestaObtenerUsuarioPublicante.error;
                return (error, null);
            }
            int cantidadVisitasUnMismoUsuario = 0;

            TransferenciaVisitaInmueble visitaInmueble = publicacion.VisitaInmueble.Where(x => x.usuarioId == usuarioIdVisita).FirstOrDefault();
            if (visitaInmueble != null)
            {
                visitaInmueble.cantidadVecesQueRepitioVisita++;
                cantidadVisitasUnMismoUsuario = visitaInmueble.cantidadVecesQueRepitioVisita;
            }
            TransferenciaPlanUsuario planUsuario = respuestaObtenerUsuarioPublicante.usuarioPublicante.PlanUsuario.Where(x => x.activo == true).FirstOrDefault();
            //if (cantidadVisitasUnMismoUsuario == 0) { planUsuario.cantidadCreditosActivos = planUsuario.cantidadCreditosActivos - puntosResta; }
           
                if (planUsuario.cantidadCreditosActivos <= 0)
            {
                var tareaEditarPlanUsuario = clienteHttp.PostAsJsonAsync<TransferenciaPlanUsuario>("api/PlanUsuario/editarPlanUsuario", planUsuario);
                if (!tareaEditarPlanUsuario.Result.IsSuccessStatusCode)
                {
                    throw new Exception(tareaEditarPlanUsuario.Result.StatusCode.ToString());
                }
                var tareaObtenerPublicacionesDelUsuario = clienteHttp.GetAsync("api/Publicacion/obtenerPublicacionesPorUsuario/" + respuestaObtenerUsuarioPublicante.usuarioPublicante.usuarioId);
                tareaObtenerPublicacionesDelUsuario.Wait();
                if (!tareaObtenerPublicacionesDelUsuario.Result.IsSuccessStatusCode)
                {
                    throw new Exception(tareaObtenerPublicacionesDelUsuario.Result.StatusCode.ToString());
                }
                List<TransferenciaPublicacion> publicacionesUsuario = tareaObtenerPublicacionesDelUsuario.Result.Content.ReadAsAsync<List<TransferenciaPublicacion>>().Result;
                ExpertoPublicaciones.HabilitarDeshabilitarPublicacionesUsuario(publicacionesUsuario, (int)CodigosEstados.Estados.inactivaPorFaltaDeCreditos, clienteHttp);

                await ExpertoMensajeria.EnviarMailAvisoCreditosAgotados(publicacion.Propiedad.Usuario.email);
                return (error, datosInmueble);
            }
            if (cantidadVisitasUnMismoUsuario == 0) //Si es la primera vez que el usuario visita la publicacion se crea instancia de VisitaInmueble 
            {
                TransferenciaNoPersistidoPlanUsuarioVisita planUsuarioVisita = new TransferenciaNoPersistidoPlanUsuarioVisita();
                planUsuarioVisita.visita = new TransferenciaVisitaInmueble();
                planUsuarioVisita.visita.visitaInmuebleId = Guid.NewGuid().ToString();
                planUsuarioVisita.visita.fechaHoraVisitaInmueble = DateTime.UtcNow;
                planUsuarioVisita.visita.publicacionId = publicacion.publicacionId;
                planUsuarioVisita.visita.usuarioId = usuarioIdVisita;
                planUsuarioVisita.visita.cantidadVecesQueRepitioVisita = 1;
                planUsuarioVisita.planUsuario = planUsuario;
                var tareaCrearVisita = clienteHttp.PostAsJsonAsync<TransferenciaNoPersistidoPlanUsuarioVisita>("api/Visita/crearVisita", planUsuarioVisita);
                tareaCrearVisita.Wait();
                if (!tareaCrearVisita.Result.IsSuccessStatusCode)
                {
                    throw new Exception(tareaCrearVisita.Result.StatusCode.ToString());
                }
                if (cantidadVisitasUnMismoUsuario == 0)
                {
                    try {
                        await ExpertoMensajeria.EnviarMailAvisoVisita(respuestaObtenerUsuarioPublicante.usuarioPublicante.email, publicacion.Propiedad.ubicacion, publicacion.Propiedad.TipoPropiedad.FirstOrDefault().nombreTipoPropiedad);

                    }catch (Exception ex)
                    {
                        Console.WriteLine("error al enviar el mail");

                    }

                }
            }
            else
            {//Sino modifica la instancia de VisitaInmueble.

                var tareaEditarVista = clienteHttp.PostAsJsonAsync<TransferenciaVisitaInmueble>("api/Visita/editarVisita", visitaInmueble);
                tareaEditarVista.Wait();
                if (!tareaEditarVista.Result.IsSuccessStatusCode)
                {
                    throw new Exception(tareaEditarVista.Result.StatusCode.ToString());
                }
            }


            #endregion


            ExpertoUsuarios.RegistrarActividad(usuarioIdVisita, "Visitó un inmueble con ubicación en " + publicacion.Propiedad.ubicacion);
            return (error, datosInmueble);
        }
    }
}
