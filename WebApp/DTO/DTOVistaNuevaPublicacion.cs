using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.DTO
{
    public class DTOVistaNuevaPublicacion
    {
        [Required]
        public List<DTOPais> listaDtoPais { get; set; }
        public List<DTOProvincia> listaDtoProvincia { get; set; }
        public List<DTOCiudad> listaDtoCiudad { get; set; }
        public string direccion { get; set; }
        public decimal precioPropiedad { get; set; }
        public float superficieTerreno { get; set; }
        public float superficieCubierta { get; set; }
        public int añosAntiguedad { get; set; }
        public int nroPlantas { get; set; }
        public byte[] imagen { get; set; }
        public DateTime fechaRegistro { get; set; }
        public List<DTOTipoAmbiente> listaDtoTipoAmbiente { get; set; }
        public List<DTOTipoPropiedad> listaDtoTipoPropiedad { get; set; }
        public List<DTOTipoOperacion> listaDtoTipoOperacion { get; set; }
        public List<DTOTipoConstruccion> listaDtoTipoConstruccion { get; set; }
        public List<DTOTipoUsuario> listaDtoTipoUsuario { get; set; }
        public List<DTOTipoMoneda> listaDtoTipoMoneda { get; set; }
        public List<DTOExtras> listaDtoExtras { get; set; }

        public void Inicializar() {
            listaDtoPais = new List<DTOPais>();
            listaDtoProvincia = new List<DTOProvincia>();
            listaDtoCiudad = new List<DTOCiudad>();
            listaDtoTipoPropiedad = new List<DTOTipoPropiedad>();
            listaDtoTipoConstruccion = new List<DTOTipoConstruccion>();
            listaDtoTipoUsuario = new List<DTOTipoUsuario>();
            listaDtoTipoMoneda = new List<DTOTipoMoneda>();
            listaDtoTipoOperacion = new List<DTOTipoOperacion>();
            listaDtoTipoAmbiente = new List<DTOTipoAmbiente>();
            listaDtoExtras = new List<DTOExtras>();
        }

        public void AddDTOPais(DTOPais dto) {
            
            listaDtoPais.Add(dto);
        }
        public void AddDTOProvincia(DTOProvincia dto) {
            
            listaDtoProvincia.Add(dto);
        }
        public void AddDTOCiudad(DTOCiudad dto) {
            
            listaDtoCiudad.Add(dto);
        }
        public void AddDTOTipoPropiedad(DTOTipoPropiedad dto) {
            
            listaDtoTipoPropiedad.Add(dto);
        }
        public void AddDTOTipoOperacion(DTOTipoOperacion dto) {
            
            listaDtoTipoOperacion.Add(dto);
        }
        public void AddDTOTipoConstruccion(DTOTipoConstruccion dto) {
            
            listaDtoTipoConstruccion.Add(dto);
        }
        public void AddDTOTipoUsuario(DTOTipoUsuario dto) {
            
            listaDtoTipoUsuario.Add(dto);
        }
        public void AddDTOTipoMoneda(DTOTipoMoneda dto) {
            
            listaDtoTipoMoneda.Add(dto);
        }
        public void AddDTOTipoAmbiente(DTOTipoAmbiente dto) {
            listaDtoTipoAmbiente.Add(dto);
        }
        public void AddDTOExtras(DTOExtras dto) {
            listaDtoExtras.Add(dto);
        }

    }
}