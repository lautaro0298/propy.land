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
    public class TipoMonedaController : ControllerBase
    {
        private ConexionDB con;
        public TipoMonedaController(ConexionDB conexion)
        {
            con = conexion;
        }
        [HttpPost("eliminarTipoMoneda")]
        public ActionResult eliminarTipoMoneda(TipoMoneda tipoMoneda)
        {
            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Entry(tipoMoneda).State = EntityState.Deleted;
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
        [HttpPost("crearTipoMoneda")]
        public ActionResult crearTipoMoneda(TipoMoneda tipoTipoMoneda)
        {
            using (var db = con.Database.BeginTransaction())
            {
                tipoTipoMoneda.activo = true;
                try
                {
                    con.Add(tipoTipoMoneda);
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
        [HttpGet("obtenerTiposMonedas")]
        public List<TipoMoneda> ObtenerTodos() {
            List<TipoMoneda> tiposMonedas = (from x in con.TipoMoneda
                                             where x.activo == true
                                             select x).ToList();
            return tiposMonedas;
        }
        [HttpGet("obtenerPorID/{id}")]
        public TipoMoneda ObtenerPorID(string id)
        {
            TipoMoneda tipoMoneda = (from x in con.TipoMoneda
                                             where x.tipoMonedaId == id
                                             orderby x.denominacionMoneda
                                             select x).FirstOrDefault();
            return tipoMoneda;
        }

    }
}