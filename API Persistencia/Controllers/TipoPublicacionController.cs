using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Persistencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPublicacionController : ControllerBase
    {
        private ConexionDB con;
        public TipoPublicacionController(ConexionDB conexion)
        {
            con = conexion;
        }
        [HttpGet("obtenerPorID/{id}")]
        public TipoPublicacion ObtenerPorID(Guid id)
        {
            TipoPublicacion tipoPublicacion = (from x in con.TipoPublicacion
                                     where x.tipoPublicacionId == id
                                     orderby x.nombreTipoPublicacion
                                     select x).FirstOrDefault();
            return tipoPublicacion;
        }

        [HttpPost("eliminarTipoPublicacion")]
        public ActionResult eliminarTipoPublicacion(TipoPublicacion tipoPublicacion)
        {
            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Entry(tipoPublicacion).State = EntityState.Deleted;
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
        [HttpGet("obtenerTiposPublicaciones")]
        public List<TipoPublicacion> ObtenerTodos()
        {
            List<TipoPublicacion> listadoTiposPublicaciones = (from x in con.TipoPublicacion
                                                                 where x.activo == true
                                                                 orderby x.nombreTipoPublicacion
                                                                 select x).ToList();
            return listadoTiposPublicaciones;
        }
        [HttpPost("crearTipoPublicacion")]
        public ActionResult CrearTipoPublicacion(TipoPublicacion tipoPublicacion)
        {
            using (var db = con.Database.BeginTransaction())
            {
                tipoPublicacion.activo = true;
                try
                {
                    con.Add(tipoPublicacion);
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
        [HttpPost("editarTipoPublicacion")]
        public ActionResult EditarTipoPublicacion(TipoPublicacion tipoPublicacion)
        {
            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Entry(tipoPublicacion).State = EntityState.Modified;
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