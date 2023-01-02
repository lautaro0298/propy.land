using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.DTO;

namespace WebApp.DTO
{
    public class DTOFiltrosBusqueda
    {
        public List<DTOTipoPropiedad> dtoTP = new List<DTOTipoPropiedad>();
        public List<DTOTipoOperacion> dtoTO = new List<DTOTipoOperacion>();
        public List<DTOPais> dtoPais = new List<DTOPais>();
        public List<DTOProvincia> dtoProvincia = new List<DTOProvincia>();
        public List<DTOCiudad> dtoCiudad = new List<DTOCiudad>();
        public List<DTOTipoConstruccion> dtoTC = new List<DTOTipoConstruccion>();
        public List<DTOExtras> dtoE = new List<DTOExtras>();
        public List<DTOTipoMoneda> dtoTM = new List<DTOTipoMoneda>();
        public List<DTOResultadoBusquedaPublicacion> dtoResultadoPublicacion = new List<DTOResultadoBusquedaPublicacion>();

        [Display(Name ="Precio Rápido:")]
        public decimal precioMax { get; set; }
        [Display(Name ="Precio Desde:")]
        public decimal precioDesde { get; set; }
        [Display(Name ="Precio Hasta:")]
        public decimal precioHasta { get; set; }
        [Display(Name ="Tipo de Moneda:")]
        public string tipoMoneda { get; set; }
        [Display(Name ="Máx cantidad de ambientes:")]
        public int cantidadAmbientes { get; set; }
        [Display(Name ="Cantidad de baños:")]
        public int cantidadBaños { get; set; }
        [Display(Name ="Cantidad de dormitorios:")]
        public int cantidadDormitorios { get; set; }
        [Display(Name ="Cantidad de cocheras:")]
        public int cantidadCocheras { get; set; }
        [Display(Name ="Años de antiguedad:")]
        public int añosAntiguedad { get; set; }
        [Display(Name ="Número de plantas:")]
        public int cantidadPlantas { get; set; }
        [Display(Name = "¿Desea buscar precios equivalentes en distintas monedas?")]
        public bool Precioequivalente { get; set; }

        public void AgregarDTOTipoPropiedad(DTOTipoPropiedad dtoTipoPropiedad) {
            dtoTP.Add(dtoTipoPropiedad);
        }
        public void AgregarDTOTipoOperacion(DTOTipoOperacion dtoTipoOperacion) {
            dtoTO.Add(dtoTipoOperacion);
        }
        
        public void AgregarDTOTipoConstruccion(DTOTipoConstruccion dtoTipoC) {
            dtoTC.Add(dtoTipoC);
        }
        public void AgregarDTOExtra(DTOExtras dtoExtra) {
            dtoE.Add(dtoExtra);
        }

        public void AgregarDTOPublicacion(DTOResultadoBusquedaPublicacion dtoPublicacion)
        {
            dtoResultadoPublicacion.Add(dtoPublicacion);
        }
        public void AgregarDTOTipoMoneda(DTOTipoMoneda dtoTipoMoneda) {
            dtoTM.Add(dtoTipoMoneda);
        }

    }
}