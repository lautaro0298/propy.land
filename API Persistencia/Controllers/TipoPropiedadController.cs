using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_Persistencia.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Persistencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPropiedadController : ControllerBase
    {
        private ConexionDB con;
        public TipoPropiedadController(ConexionDB conexion)
        {
            con = conexion;
        }
        [HttpGet("obtenerPorId/{id}")]
        public TipoPropiedad ObtenerPorId(string id) {
            TipoPropiedad tipoPropiedad = (from x in con.TipoPropiedad
                                          .Include(x => x.TipoPropiedadTipoAmbiente)
                                           where x.tipoPropiedadId == id
                                           select x).FirstOrDefault();
            return tipoPropiedad;
        }
        [HttpGet("obtenerTipoPropiedadesConTipoAmbiente")]
        public List<TipoPropiedad> ObtenerTipoPropiedadConTipoAmbiente()
        {
            List<TipoPropiedad> tipoPropiedades = (from x in con.TipoPropiedad.Include(x => x.TipoPropiedadTipoAmbiente) where x.activo == true orderby x.nombreTipoPropiedad ascending select x).ToList() ;
            
            return tipoPropiedades;
        }
        [HttpGet("obtenerTipoPropiedadesConCaracteristicas")]
        public List<TipoPropiedad> ObtenerTipoPropiedadConCaracteristicas()
        {
            List<TipoPropiedad> tipoPropiedades = con.TipoPropiedad.Where(e => e.activo == true).Include(x => x.caracteristica).ThenInclude(q => q.caracteristicas).ToList();

            return tipoPropiedades;
        }
        [HttpGet("obtenerTipoPropiedadesConTipoAmbientes")]
        public List<TipoPropiedad> ObtenerTipoPropiedadConAmbiente(String id)
        {
            List<TipoPropiedad> tipoPropiedades = (from x in con.TipoPropiedad select x).ToList();

            return tipoPropiedades;
        }

        [HttpGet("obtenerTiposPropiedades")]
        public List<TipoPropiedad> ObtenerTodos()
        {
            List<TipoPropiedad> listadoTiposPropiedades= (from x in con.TipoPropiedad
                                                        where x.activo == true
                                                        orderby x.nombreTipoPropiedad  
                                                        select x).ToList();
            
            return listadoTiposPropiedades;
        }
        [HttpPost("crearTipoPropiedad")]
        public ActionResult CrearTipoPropiedad(TipoPropiedad tipoPropiedad)
        {
            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Add(tipoPropiedad);
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
        [HttpPost("editarTipoPropiedad")]
        public ActionResult EditarTipoPropiedad(TipoPropiedad tipoPropiedad)
        {
            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Entry(tipoPropiedad).State = EntityState.Modified;
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
        
        [HttpPost("eliminarTipoPropiedad")]
        public ActionResult eliminarTipoPropiedad(TipoPropiedad tipoPropiedad)
        {
            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Entry(tipoPropiedad).State = EntityState.Deleted;
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
}}