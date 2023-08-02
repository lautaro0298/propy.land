using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Persistencia.Models;
using API_Persistencia.Models.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Persistencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusquedaController : ControllerBase
    {
        private ConexionDB con;
        public BusquedaController(ConexionDB conexion)
        {
            con = conexion;
        }

        [HttpGet("obtenerPropiedadesParaEvaluarBusqueda")]
        public List<Publicacion> ObtenerPropiedadesParaEvaluarBusqueda() {
            List<Publicacion> publicaciones = (from x in con.Publicacion
                                             .Include(x => x.Propiedad)
                                             .Include(x => x.Propiedad).ThenInclude(x => x.ImagenPropiedad)  
                                             .Include(x => x.Propiedad).ThenInclude(x => x.PropiedadTipoAmbiente).ThenInclude(x => x.TipoAmbiente)
                                             .Include(x => x.Propiedad).ThenInclude(x => x.TipoConstruccion)
                                             .Include(x => x.TipoPublicacion)
                                             .Include(x => x.Propiedad).ThenInclude(x => x.TipoPublicante)
                                             .Include(x => x.Propiedad).ThenInclude(x => x.TipoMoneda)
                                               where x.estado == 0
                                               select x).ToList();

            return publicaciones;
        }
        [HttpGet("buscardortipoPropiedad")]
        public List<Models.Publicacion> filtarporTipoPropiedad([FromQuery(Name = "tipoPropiedadId")] string[] tipoPropiedadId)
        {
            List<Models.Publicacion> publicaciones = (from x in con.Publicacion
                                             .Include(x => x.Propiedad)
                                             .Include(x => x.Propiedad).ThenInclude(x => x.ImagenPropiedad)
                                             .Include(x => x.Propiedad).ThenInclude(x => x.PropiedadTipoAmbiente).ThenInclude(x => x.TipoAmbiente)
                                             .Include(x => x.Propiedad).ThenInclude(x => x.TipoConstruccion)
                                             .Include(x => x.TipoPublicacion)
                                             .Include(x => x.Propiedad).ThenInclude(x => x.TipoPublicante)
                                             .Include(x => x.Propiedad).ThenInclude(x => x.TipoMoneda)
                                                      where tipoPropiedadId.Contains(x.Propiedad.tipoPropiedadId)
                                                      select x).ToList();

            return publicaciones;
        }
        [HttpGet("buscardortipo")]
        public List<Models.Publicacion> filtarporcaracteristicas([FromQuery(Name = "tipoPropiedadId")] string[] tipoPropiedadId , [FromQuery(Name = "caracteristicaId")] string[] caracteristicaId)
        {
            List<Models.Publicacion> publicaciones = (from x in con.PublicacionCaracteristicas
                                               .Include(x =>x.Publicacion).ThenInclude(x=>x.Propiedad).ThenInclude(x => x.ImagenPropiedad)
                                             .Include(x => x.Publicacion.Propiedad).ThenInclude(x => x.TipoConstruccion)
                                             .Include(x => x.Publicacion.TipoPublicacion)
                                             .Include(x => x.Publicacion.Propiedad).ThenInclude(x => x.TipoPublicante)
                                             .Include(x => x.Publicacion.Propiedad).ThenInclude(x => x.TipoMoneda)
                                                      where tipoPropiedadId.Contains(x.Publicacion.Propiedad.tipoPropiedadId)
                                                      where caracteristicaId.Contains(x.CaracteristicaId)  

                                               select x.Publicacion).ToList();

            return publicaciones;
        }
        [HttpGet("buscardorAmbientes")]
        public List<string> filtarporAmbientes([FromQuery(Name = "cantCocheras")] int cantCocheras, [FromQuery(Name = "caracteristicaId")] int cantDormitorios)
        {
            List<string> publicaciones;
            var chocherasId = "7ef6671b-c3d9-4418-9a39-40b92d9a2d23";
            var dormitorioId = "e05b8a62-c878-4679-9845-15fa116b713f";
            if (cantCocheras == 0 || cantCocheras == null) {
                 publicaciones = (from y in con.PropiedadTipoAmbiente
                                              where y.tipoAmbienteId == dormitorioId && y.cantidad >= cantDormitorios
                                              select y.propiedadId).ToList();
            }
            else if (cantDormitorios == 0 || cantDormitorios == null) {
                publicaciones = (from x in con.PropiedadTipoAmbiente
                                              where x.tipoAmbienteId == chocherasId && x.cantidad >= cantCocheras
                                              select x.propiedadId).ToList();
            }
            else publicaciones = (from x in con.PropiedadTipoAmbiente
                                          from y in con.PropiedadTipoAmbiente
                                          where x.tipoAmbienteId == chocherasId && x.cantidad >= cantCocheras
                                          where y.tipoAmbienteId == dormitorioId && y.cantidad >= cantDormitorios
                                          && y.propiedadId == x.propiedadId
                                          select x.propiedadId ).ToList();

            return publicaciones;
        }
        [HttpGet("buscardorPorPrecio")]
        public List<Publicacion> filtarporPrecio([FromQuery(Name = "cant")] int cant, [FromQuery(Name = "Moneda")] string moneda)
        {
            List<Publicacion> publicaciones = (from x in con.Publicacion
                                             .Include(x => x.Propiedad)
                                             .Include(x => x.Propiedad).ThenInclude(x => x.ImagenPropiedad)
                                             .Include(x => x.Propiedad).ThenInclude(x => x.PropiedadTipoAmbiente).ThenInclude(x => x.TipoAmbiente)
                                             .Include(x => x.Propiedad).ThenInclude(x => x.TipoConstruccion)
                                             .Include(x => x.TipoPublicacion)
                                             .Include(x => x.Propiedad).ThenInclude(x => x.TipoPublicante)
                                             .Include(x => x.Propiedad).ThenInclude(x => x.TipoMoneda)
                                               where x.Propiedad.precioPropiedad <= cant
                                               where x.Propiedad.TipoMoneda.denominacionMoneda == moneda
                                               select x).ToList();
           

            return publicaciones;
        }
        [HttpGet("buscardorPorPalabra")]
        public List<Publicacion> filtarporPalabra([FromQuery(Name = "Palabra")] string palabra)
        {
            List<Publicacion> publicaciones = (from x in con.Publicacion
                                             .Include(x => x.Propiedad)
                                             .Include(x => x.Propiedad).ThenInclude(x => x.ImagenPropiedad)
                                             .Include(x => x.Propiedad).ThenInclude(x => x.PropiedadTipoAmbiente).ThenInclude(x => x.TipoAmbiente)
                                             .Include(x => x.Propiedad).ThenInclude(x => x.TipoConstruccion)
                                             .Include(x => x.TipoPublicacion)
                                             .Include(x => x.Propiedad).ThenInclude(x => x.TipoPublicante)
                                             .Include(x => x.Propiedad).ThenInclude(x => x.TipoMoneda)
                                               where x.Propiedad.descripcionPropiedad.Contains(palabra)

                                               select x).ToList();


            return publicaciones;
        }
        [HttpGet("buscardorPorPublicancion")]
        public List<Publicacion> filtarporPublicacion( [FromQuery(Name = "Publicacion")] string publicacion)
        {
            List<Publicacion> publicaciones = (from x in con.Publicacion
                                             .Include(x => x.Propiedad)
                                             .Include(x => x.Propiedad).ThenInclude(x => x.ImagenPropiedad)
                                             .Include(x => x.Propiedad).ThenInclude(x => x.PropiedadTipoAmbiente).ThenInclude(x => x.TipoAmbiente)
                                             .Include(x => x.Propiedad).ThenInclude(x => x.TipoConstruccion)
                                             .Include(x => x.TipoPublicacion)
                                             .Include(x => x.Propiedad).ThenInclude(x => x.TipoPublicante)
                                             .Include(x => x.Propiedad).ThenInclude(x => x.TipoMoneda)
                                               where x.TipoPublicacion.nombreTipoPublicacion== publicacion
                                               select x).ToList();



            return publicaciones;
        }
    }

}
