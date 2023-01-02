using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using LibreriaClases;
using LibreriaClases.DTO;
using LibreriaClases.Transferencia;

namespace LibreriaExperto.Estadisticas
{
    public static class ExpertoEstadisticas
    {
        public static (ErrorPropy,DTOConsultaEstadisticaPublicacion) ConsultarEstadisticasPublicacion(string publicacionId) {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            TimeZoneInfo zonaHorariaArgentina = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
            DTOConsultaEstadisticaPublicacion datosEstadisticas = new DTOConsultaEstadisticaPublicacion();
            var tareaObtenerPublicacion = clienteHttp.GetAsync("api/Publicacion/obtenerPublicacionPorId/" + publicacionId);
            tareaObtenerPublicacion.Wait();
            if (!tareaObtenerPublicacion.Result.IsSuccessStatusCode) {
                throw new Exception(tareaObtenerPublicacion.Result.StatusCode.ToString());
            }
            TransferenciaPublicacion publicacion = tareaObtenerPublicacion.Result.Content.ReadAsAsync<TransferenciaPublicacion>().Result;
            if (publicacion == null) {
                error.codigoError = 666;
                error.descripcionError = "Error: " + error.codigoError + " no se pudieron obtener los datos de la publicación";
                return (error, null);
            }
            datosEstadisticas.ubicacionPropiedad = publicacion.Propiedad.ubicacion;
            datosEstadisticas.publicacionId = publicacion.publicacionId;
            foreach (var visita in publicacion.VisitaInmueble) {
                var tareaObtenerUsuario = clienteHttp.GetAsync("api/Usuario/obtenerUsuarioPorID/" + visita.usuarioId);
                tareaObtenerUsuario.Wait();
                if (!tareaObtenerUsuario.Result.IsSuccessStatusCode) {
                    throw new Exception(tareaObtenerUsuario.Result.StatusCode.ToString());
                }
                TransferenciaUsuario usuarioVisitante = tareaObtenerUsuario.Result.Content.ReadAsAsync<TransferenciaUsuario>().Result;
                DTOEstadisticaPublicacion datoEstadistica = new DTOEstadisticaPublicacion();
                datoEstadistica.cantidadVisitas = visita.cantidadVecesQueRepitioVisita;
                if (usuarioVisitante.permiterSerContactadoPorPublicante == true)
                {
                    datoEstadistica.nombreVisitante = usuarioVisitante.nombreUsuario + " " + usuarioVisitante.apellidoUsuario;
                }
                else {
                    datoEstadistica.nombreVisitante = usuarioVisitante.nombreUsuario + " *****";
                }
                datoEstadistica.IDUsuarioVisitante = usuarioVisitante.usuarioId;
                datoEstadistica.usuarioVisitantePermiteSerContactadoPorPublicante = usuarioVisitante.permiterSerContactadoPorPublicante;
                datoEstadistica.solicitoDatosPublicante = visita.contactoPublicante;
                datoEstadistica.fechaUltimaVisita = TimeZoneInfo.ConvertTimeFromUtc(visita.fechaHoraVisitaInmueble,zonaHorariaArgentina).ToShortDateString();
                datosEstadisticas.estadisticas.Add(datoEstadistica);
            }
            return (error, datosEstadisticas);
        }
    }
}
