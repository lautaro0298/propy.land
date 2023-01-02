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
    public class TipoPublicanteController : ControllerBase
    {
        private ConexionDB con;
        public TipoPublicanteController(ConexionDB conexion)
        {
            con = conexion;
        }
        [HttpGet("obtenerTiposPublicantes")]
        public List<TipoPublicante> ObtenerTodos()
        {
            
            List<TipoPublicante> listadoTiposPublicantes = (from x in con.TipoPublicante
                                                               where x.activo == true
                                                               orderby x.nombreTipoPublicante
                                                               select x).ToList();
            return listadoTiposPublicantes;
        }
        [HttpPost("crearTipoPublicante")]
        public ActionResult CrearTipoPublicante(TipoPublicante tipoPublicante)
        {
            using (var db = con.Database.BeginTransaction())
            {
                tipoPublicante.activo = true;
                try
                {
                    con.Add(tipoPublicante);
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
        [HttpPost("editarTipoPublicante")]
        public ActionResult EditarTipoPublicante(TipoPublicante tipoPublicante)
        {
            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Entry(tipoPublicante).State = EntityState.Modified;
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
        [HttpGet("obtenerPorID/{id}")]
        public TipoPublicante ObtenerPorID(string id)
        {
            TipoPublicante tipoPublicante = (from x in con.TipoPublicante
                                               where x.tipoPublicanteId == id
                                               orderby x.nombreTipoPublicante
                                               select x).FirstOrDefault();
            return tipoPublicante;
        }

        [HttpPost("eliminarTipoPublicante")]
        public ActionResult eliminarTipoPublicante(TipoPublicante tipoPublicante)
        {
            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Entry(tipoPublicante).State = EntityState.Deleted;
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
    }
}