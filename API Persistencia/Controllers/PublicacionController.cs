using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using API_Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Persistencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionController : ControllerBase
    {
        private ConexionDB con;
        public PublicacionController(ConexionDB conexion)
        {
            con = conexion;
        }
        [HttpGet("obtenerPropiedades")]
        public List<Propiedad> ObtenerInmueble() {
            List<Propiedad> propiedades = (from x in con.Publicacion
                                   where x.estado == 0
                                   select x.Propiedad).ToList();
            return propiedades;
        }
        [HttpGet("obtenerPublicacionPorId/{publicacionId}")]
        public Publicacion ObtenerPublicacionPorIDExpandida(string publicacionId) {
            Publicacion publicacion = (from x in con.Publicacion
                                       .Include(x=>x.Propiedad.Usuario)
                                      .Include(x => x.Propiedad)
                                      .Include(x=>x.Caracteristicas).ThenInclude(pc => pc.Caracteristica)
                                      .Include(x => x.Propiedad).ThenInclude(x=>x.TipoPublicante)
                                      .Include(x => x.TipoPublicacion)
                                      .Include(x => x.Propiedad.TipoConstruccion)
                                      .Include(x=>x.Propiedad.TipoMoneda)
                                      //fue editada 
                                      //.Include(x => x.Propiedad.TipoPropiedad).ThenInclude(x => x.caracteristica)
                                      .Include(x => x.Propiedad.PropiedadTipoAmbiente).ThenInclude(x => x.TipoAmbiente)
                                      .Include(x => x.Propiedad.ImagenPropiedad)
                                      .Include(x=>x.VisitaInmueble)
                                       where x.publicacionId == publicacionId
                                       select x).FirstOrDefault();
            return publicacion;
                                        
                                     
        }
        [HttpGet("obtenerPublicacionPorIDReducida/{publicacionId}")]
        public Publicacion ObtenerPublicacionPorIDReducida(string publicacionId) {
            Publicacion publicacion = (from x in con.Publicacion
                                       where x.estado == 0
                                       select x).FirstOrDefault();
            return publicacion;
        }
        [HttpGet("obtenerPublicacionesPorUsuario/{usuarioId}")]
        public List<Publicacion> ListarPublicacionesPorUsuario(string usuarioId) {
            List<Publicacion> publicaciones = (from x in con.Publicacion
                                               .Include(x=>x.Propiedad)
                                               //.Include(x=>x.Propiedad.TipoPropiedad)
                                               .Include(x=>x.Propiedad.TipoMoneda)
                                               
                                               
                                               where x.Propiedad.usuarioId == usuarioId && (x.estado==0||x.estado==1)
                                               select x).ToList();
            return publicaciones;
        }
        [HttpPost("crearPublicacion")]
        public ActionResult CrearPublicacion(Publicacion publicacion) {
            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Add(publicacion);
                    con.SaveChanges();
                    db.Commit();
                
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
                return Ok();
            }
        }
        [HttpPost("eliminarPublicacion")]
        public ActionResult EliminarPublicacion(Publicacion publicacion) {
            using (var db=con.Database.BeginTransaction()) {
                try
                {
                    con.Entry(publicacion).State = EntityState.Modified;
                    con.SaveChanges();
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
                return Ok();
            }
        }
        [HttpPost("habilitarDeshabilitarPublicaciones")]
        public ActionResult DeshabilitarPublicaciones(List<Publicacion> publicaciones) {
            using (var db = con.Database.BeginTransaction()) {
                try
                {
                    foreach (var publicacion in publicaciones) {
                        con.Entry(publicacion).State = EntityState.Deleted;
                    }
                    
                    con.SaveChanges();
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }
            return Ok();
        }
        [HttpPost("editarPublicacion")]
        public ActionResult EditarPublicacion(Publicacion publicacion) {
            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    foreach (var imagen in publicacion.Propiedad.ImagenPropiedad) {
                        if (con.ImagenPropiedad.Contains(imagen))
                        {
                            con.Entry(imagen).State = EntityState.Modified;
                        }
                        else {
                            con.ImagenPropiedad.Add(imagen);
                        }
                    }
                    // esta parte fue editada
                    //foreach (var propiedadCaracteristica in publicacion.Propiedad.TipoPropiedad.caracteristica) {
                    //    if (con.TipoPropiedadCaracteristica.Contains(propiedadCaracteristica))
                    //    {
                    //        con.Entry(propiedadCaracteristica).State = EntityState.Modified;
                    //    }
                    //    else {
                    //        con.TipoPropiedadCaracteristica.Add(propiedadCaracteristica);
                    //    }
                    //}
                    foreach (var propiedadTipoAmbiente in publicacion.Propiedad.PropiedadTipoAmbiente) {
                        if (con.PropiedadTipoAmbiente.Contains(propiedadTipoAmbiente))
                        {
                            con.Entry(propiedadTipoAmbiente).State = EntityState.Modified;
                        }
                        else {
                            con.PropiedadTipoAmbiente.Add(propiedadTipoAmbiente);
                        }
                    }
                    
                    con.Entry(publicacion.Propiedad).State = EntityState.Modified;
                    con.Entry(publicacion).State = EntityState.Modified;
                    
                    con.SaveChanges();
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }
            return Ok();
        }
    }
}