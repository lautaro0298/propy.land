using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.DTO;


namespace WebApp.DTO
{
    public class DTOVistaEditarPublicacion
    {
        public Guid publicacionId { get; set; }
        [Required]
        public Guid tipoUsuarioNuevo { get; set; }
        public string tipoUsuarioViejo { get; set; }
        [Required]
        public Guid tipoOperacionNueva { get; set; }
        public string tipoOperacionVieja { get; set; }
        [Required(ErrorMessage = "El campo Dirección es obligatorio")]
        public string direccion { get; set; }
        [Required(ErrorMessage = "El campo Precio de Propiedad es obligatorio")]
        [Range(1, Double.PositiveInfinity, ErrorMessage = "El Precio de la propiedad debe ser mayor a 0")]
        
        public decimal precioPropiedad { get; set; }
        [Required]
        public Guid tipoMonedaNueva { get; set; }
        public string tipoMonedaVieja { get; set; }
        [Required]
        public Guid tipoConstruccionNueva { get; set; }
        public string tipoConstruccionVieja { get; set; }
        public Guid tipoPropiedadNueva { get; set; }
        public string tipoPropiedadVieja { get; set; }
        public List<Guid> extrasIdElegidos { get; set; }
        public string comentarios { get; set; }

        public double latitud { get; set; }
        public double longitud { get; set; }

        public int cantidadBañosElegidos { get; set; }
        public int cantidadCocherasElegidas { get; set; }
        public int cantidadDormitoriosElegidos { get; set; }
        public int cantidadAmbientesElegidos { get; set; }

        public List<DTOPais> paises { get; set; }
        public List<DTOProvincia> provincias { get; set; }
        public List<DTOCiudad> ciudades{ get; set; }
        public List<DTOTipoUsuario> tiposUsuarios { get; set; }
        public List<DTOTipoOperacion> tiposOperaciones { get; set; }
        public List<DTOTipoMoneda> tiposMonedas { get; set; }
        public List<DTOTipoConstruccion> tiposConstrucciones { get; set; }
        public List <DTOExtras> extras { get; set; }
        public List <DTOExtrasPrevios>  extrasPrevios { get; set; }
        public List<DTOTipoPropiedad> tiposPropiedades { get; set; }
        public List<DTOTipoAmbiente> tiposAmbientes { get; set; }

        public DTOVistaEditarPublicacion() {
            paises = new List<DTOPais>();
            provincias = new List<DTOProvincia>();
            ciudades = new List<DTOCiudad>();
            tiposUsuarios = new List<DTOTipoUsuario>();
            tiposOperaciones = new List<DTOTipoOperacion>();
            tiposMonedas = new List<DTOTipoMoneda>();
            tiposConstrucciones = new List<DTOTipoConstruccion>();
            extras = new List<DTOExtras>();
            extrasPrevios = new List<DTOExtrasPrevios>();
            tiposPropiedades = new List<DTOTipoPropiedad>();
            tiposAmbientes = new List<DTOTipoAmbiente>();
        }

        public void AgregarDTOTipoAmbiente(DTOTipoAmbiente dto) {
            tiposAmbientes.Add(dto);
        }
        public void AgregarDTOTipoPropiedad(DTOTipoPropiedad dto) {
            tiposPropiedades.Add(dto);
        }
        public void AgregarDTOExtraPrevio(DTOExtrasPrevios dto) {
            extrasPrevios.Add(dto);
        }
        public void AgregarDTOPais(DTOPais dto) {
            paises.Add(dto);
        }
        public void AgregarDTOProvincia(DTOProvincia dto) {
            provincias.Add(dto);
        }
        public void AgregarDTOCiudad(DTOCiudad dto) {
            ciudades.Add(dto);
        }
        public void AgregarDTOTipoUsuario(DTOTipoUsuario dto) {
            tiposUsuarios.Add(dto);
        }
        public void AgregarDTOTipoOperacion(DTOTipoOperacion dto) {
            tiposOperaciones.Add(dto);
        }
        public void AgregarDTOTipoMoneda(DTOTipoMoneda dto) {
            tiposMonedas.Add(dto);
        }
        public void AgregarDTOTipoConstruccion(DTOTipoConstruccion dto) {
            tiposConstrucciones.Add(dto);
        }
        public void AgregarDTOExtra(DTOExtras dto) {
            extras.Add(dto);
        }
    }
}