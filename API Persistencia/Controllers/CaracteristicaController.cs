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
    public class CaracteristicaController : ControllerBase
    {
        private ConexionDB con;
        public CaracteristicaController(ConexionDB conexion)
        {
            con = conexion;
        }

        [HttpGet("obtenerCaracteristicas")]
        public ActionResult<List<Caracteristica>> ObtenerTodos()
        {
            if (con == null)
            {
                return BadRequest("ConexionDB is null.");
            }

            List<Caracteristica> listadoCaracteristicas = (from x in con.Caracteristica
                                                           where x.activo == true
                                                           orderby x.nombreCaracteristica
                                                           select x).ToList();
            return Ok(listadoCaracteristicas);
        }

        [HttpGet("obtenerPorID/{id}")]
        public ActionResult<Caracteristica> ObtenerPorID(string id)
        {
            if (con == null)
            {
                return BadRequest("ConexionDB is null.");
            }

            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("ID is null or empty.");
            }

            Caracteristica caracteristica = (from x in con.Caracteristica
                         where x.caracteristicaId == id
                         select x).FirstOrDefault();

            if (caracteristica == null)
            {
                return NotFound("Caracteristica not found.");
            }

            return Ok(caracteristica);
        }

        [HttpPost("crearCaracteristica")]
        public ActionResult CrearCaracteristica(Caracteristica caracteristica)
        {
            if (con == null)
            {
                return BadRequest("ConexionDB is null.");
            }

            if (caracteristica == null)
            {
                return BadRequest("Caracteristica is null.");
            }

            caracteristica.activo = true;
            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Add(caracteristica);
                    con.SaveChanges();
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }

            return CreatedAtAction(nameof(ObtenerPorID), new { id = caracteristica.caracteristicaId }, caracteristica);
        }

        [HttpPost("editarCaracteristica")]
        public ActionResult EditarCaracteristica(Caracteristica caracteristica)
        {
            if (con == null)
            {
                return BadRequest("ConexionDB is null.");
            }

            if (caracteristica == null)
            {
                return BadRequest("Caracteristica is null.");
            }

            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Entry(caracteristica).State = EntityState.Modified;
                    con.SaveChanges();
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }

            return Ok("Caracteristica updated.");
        }

        [HttpPost("eliminarCaracteristica")]
        public ActionResult eliminarCaracteristica(Caracteristica caracteristica)
        {
            if (con == null)
            {
                return BadRequest("ConexionDB is null.");
            }

            if (caracteristica == null)
            {
                return BadRequest("Caracteristica is null.");
            }

            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Entry(caracteristica).State = EntityState.Deleted;
                    con.SaveChanges();
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }

            return Ok("Caracteristica deleted.");
        }
    }
}
