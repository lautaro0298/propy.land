using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Servicios
{
    public static class NotificacionesServicios
    {
        #region Mensajes de Clases
        public const string clasePublicacion = "Publicacion";
        public const string clasePropiedad = "Propiedad";
        public const string claseTipoPublicacion = "TipoPublicacion";
        public const string clasePropiedadExtras = "PropiedadExtras";
        #endregion

        public const string errorTipoPopiedad = "Debe seleccionar un tipo de propiedad.";
        public const string errorTipoConstruccion = "Debe seleccionar un tipo de construcción.";
        public const string errorCantidadImagenes = "Debe seleccionar al menos 1 (uno) imagen";
        public const string cuilErroneo = "Debe ingresar un número de Cuil válido.";
        public const string faltaDireccion = "Debe ingresar una dirección.";
        public const string faltaPrecio = "Debe ingresar un precio.";
        public const string precioHastaEsMenorPrecioDesde = "Ingrese un precio desde mayor al precio hasta";
        public const string precioMayorCero = "El precio debe ser mayor a 0 (cero).";
        public const string superficieTerrenoEsMenorSuperficieCubierta = "La superficie del terreno no puede ser menor a la superficie total.";
        public const string errorApiGoogleMapsIdentificador = "Ocurrió un error en la comunicación con el servicio externo de Google, no se pudo obtener el identificador de su ubicación.";
        public const string errorApiGoogleMapsUbicacion = "Ocurrió un error en la comunicación con el servicio externo de Google, no se pudo obtener la dirección específica de su ubicación.";
        public const string errorExtensionImagen = "La imagen que intenta subir no tiene el formato adecuado. Los formatos permitidos son: .png .jpeg: Imagen erronea.";
        public const string errorNuevaPublicacion = "Ha ocurrido un error inesperado al intentar crear la publicación. Por favor contáctese con el soporte técnico.";
        public const string errorEditarPublicacion= "Ha ocurrido un error inesperado al intentar editar la publicación. Por favor contáctese con el soporte técnico.";
        public const string errorSuperficieTerreno = "La superficie del terreno no puede ser negativa.";
        public const string errorSuperficieTerrenoMinimaMayorMaxima = "La superficie mínima no puede ser mayor a la superficie máxima.";
        public const string errorContratarPlan = "Se ha producido un error al intentar asignarle el plan, por favor, pongase en contacto con el soporte técnico. Lamentamos las molestias.";
    }
}