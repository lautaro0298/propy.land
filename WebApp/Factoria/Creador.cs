using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.DTO;

namespace WebApp.Factoria
{
    public class Creador
    {
        public static ImagenPublicacion CrearImagen(Guid publicacionId,string rutaImagenBD) {
            return new ImagenPublicacion()
            {
                imagenPublicacionId = System.Guid.NewGuid(),
                rutaBD = rutaImagenBD,
                publicacionId = publicacionId
            };
        }

        public static Propiedad CrearPropiedad(int añosAntiguedad,string calle,int nroCalle,string pais,
            string areaAdmNivel1,string areaAdmNivel2,string direccionFormateada,string idUbicacionGoogle,
            double latitud, double longitud,Guid tipoConstruccionId,Guid tipoPropiedadId,DateTime fechaRegistro,
            float superficieTerreno, float superficieCubierta,int nroPlantas,string idUsuario) {
            return new Propiedad() {
                propiedadId = System.Guid.NewGuid(),
                antiguedad=añosAntiguedad,
                calle=calle,
                pais=pais,
                areaAdministrativaNivel1=areaAdmNivel1,
                areaAdministrativaNivel2=areaAdmNivel2,
                direccionFormateada=direccionFormateada,
                identificadorUbicacionGoogle=idUbicacionGoogle,
                latitud=latitud,
                longitud=longitud,
                tipoConstruccionId=tipoConstruccionId,
                tipoPropiedadId=tipoPropiedadId,
                fechaRegistro=fechaRegistro,
                superficieTerreno=superficieTerreno,
                superficieCubierta=superficieCubierta,
                nroPlantas=nroPlantas,
                UserId=idUsuario
            };
        }
        public static PropiedadTipoAmbiente CrearPropiedadTipoAmbiente(Guid propiedadId,Guid tipoAmbienteId,int cantidadAmbientes) {
            return new PropiedadTipoAmbiente()
            {
                propiedadTipoAmbienteId = System.Guid.NewGuid(),
                propiedadId = propiedadId,
                tipoAmbienteId = tipoAmbienteId,
                cantidadAmbientes = cantidadAmbientes
            };
        }
        public static PropiedadExtras CrearPropiedadExtras(Guid propiedadId,Guid extraId,bool activo) {
            return new PropiedadExtras() {
                propiedadExtrasId = System.Guid.NewGuid(),
                propiedadId=propiedadId,
                extrasId=extraId,
                activo=activo,
            };
        }
        public static Publicacion CrearPublicacion(DateTime fechaInicioPublicacion,Guid tipoPublicacionId,string usuarioId,
            Guid propiedadId,Guid tipoUsuarioId,Guid tipoMonedaId,decimal precioPropiedad) {

            return new Publicacion()
            {
                publicacionId = System.Guid.NewGuid(),
                fechaInicioPublicacion = DateTime.UtcNow,
                fechaFinPublicacion = fechaInicioPublicacion.AddDays(15),
                tipoPublicacionId=tipoPublicacionId,
                UserId=usuarioId,
                propiedadId=propiedadId,
                tipoUserId=tipoUsuarioId,
                tipoMonedaId=tipoMonedaId,
                precioPropiedad=precioPropiedad
            };
        }
        public static DTOUsuario CrearDTOUsuario(ApplicationUser usuario) {
            return new DTOUsuario()
            {
                nombre = usuario.nombre,
                apellido=usuario.apellido,
                permitirSerContactadoPublicante=usuario.permitirSerContactadoPublicante,
                permitirSerNotificado=usuario.permitirSerNotificado,
                
                nroTelefono=usuario.nroTelefono
            };
        }

    }
}