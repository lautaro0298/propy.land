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
    public class TipoConstruccionController : ControllerBase
    {
        private ConexionDB con;

        public TipoConstruccionController(ConexionDB conexion)
        {
            con = conexion ?? throw new ArgumentNullException(nameof(conexion));
        }

        [HttpGet("obtenerPorID/{id}")]
        public ActionResult<TipoConstruccion> ObtenerPorID(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("ID cannot be null or empty.");
            }

            TipoConstruccion tipoConstruccion = (from x in con.TipoConstruccion
                                     where x.tipoConstruccionId == id
                                     orderby x.nombreTipoConstruccion
                                     select x).FirstOrDefault();

            if (tipoConstruccion == null)
            {
                return NotFound("TipoConstruccion not found.");
            }

            return Ok(tipoConstruccion);
        }

        [HttpPost("eliminarTipoConstruccion")]
        public ActionResult eliminarTipoConstruccion(TipoConstruccion tipoConstruccion)
        {
            if (tipoConstruccion == null)
            {
                return BadRequest("TipoConstruccion cannot be null.");
            }

            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Entry(tipoConstruccion).State = EntityState.Deleted;
                    con.SaveChanges();
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error while deleting TipoConstruccion.");
                }
            }

            return Ok();
        }

        [HttpGet("obtenerTiposConstrucciones")]
        public ActionResult<List<TipoConstruccion>> ObtenerTodos()
        {
            List<TipoConstruccion> listadoTiposConstrucciones = (from x in con.TipoConstruccion
                                        where x.activo == true
                                        orderby x.nombreTipoConstruccion
                                        select x).ToList();

            if (listadoTiposConstrucciones.Count == 0)
            {
                return NotFound("No TipoConstruccion found.");
            }

            return Ok(listadoTiposConstrucciones);
        }

        [HttpPost("crearTipoConstruccion")]
        public ActionResult CrearTipoConstruccion(TipoConstruccion tipoConstruccion)
        {
            if (tipoConstruccion == null)
            {
                return BadRequest("TipoConstruccion cannot be null.");
            }

            if (tipoConstruccion.activo != true)
            {
                return BadRequest("TipoConstruccion must be active.");
            }

            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Add(tipoConstruccion);
                    con.SaveChanges();
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error while creating TipoConstruccion.");
                }
            }

            return Ok();
        }

        [HttpPost("editarTipoConstruccion")]
        public ActionResult EditarTipoConstruccion(TipoConstruccion tipoConstruccion)
        {
            if (tipoConstruccion == null)
            {
                return BadRequest("TipoConstruccion cannot be null.");
            }

            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Entry(tipoConstruccion).State = EntityState.Modified;
                    con.SaveChanges();
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating TipoConstruccion.");
                }
            }

            return Ok();
        }
    }
}
