using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Persistencia.Models;
using LibreriaClases.DTO;

namespace API_Persistencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoAmbienteController : ControllerBase
    {
        private readonly ConexionDB con;

        public TipoAmbienteController(ConexionDB conexion)
        {
            con = conexion ?? throw new ArgumentNullException(nameof(conexion));
        }

        [HttpGet("obtenerPorID/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ObtenerPorID(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            TipoAmbiente tipoAmbiente = (from x in con.TipoAmbiente where x.tipoAmbienteId == id orderby x.tipoAmbienteId ascending select x).FirstOrDefault();

            if (tipoAmbiente == null)
            {
                return NotFound();
            }

            return Ok(tipoAmbiente);
        }

        [HttpGet("obtenerTiposAmbientes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public List<TipoAmbiente> ObtenerTodos()
        {
            List<TipoAmbiente> listadoTiposAmbientes = (from x in con.TipoAmbiente
                                                        where x.activo == true
                                                        select x).ToList();
            return listadoTiposAmbientes;
        }

        [HttpPost("crearTipoAmbiente")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CrearTipoAmbiente(DTOTipoAmbiente DTOtipoAmbiente)
        {
            if (DTOtipoAmbiente is null)
            {
                throw new ArgumentNullException(nameof(DTOtipoAmbiente));
            }

            if (string.IsNullOrEmpty(DTOtipoAmbiente.tipoAmbienteId) || string.IsNullOrEmpty(DTOtipoAmbiente.nombreTipoAmbiente))
            {
                return BadRequest();
            }

            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    TipoAmbiente tipoAmbiente = new TipoAmbiente();
                    tipoAmbiente.nombreTipoAmbiente = DTOtipoAmbiente.nombreTipoAmbiente;
                    tipoAmbiente.activo = true;
                    tipoAmbiente.tipoAmbienteId = DTOtipoAmbiente.tipoAmbienteId;
                    con.Add(tipoAmbiente);
                    con.SaveChanges();
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }

            return CreatedAtAction(nameof(ObtenerPorID), new { id = DTOtipoAmbiente.tipoAmbienteId }, null);
        }

        [HttpPost("editarTipoAmbiente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EditarTipoAmbiente(TipoAmbiente tipoAmbiente)
        {
            if (tipoAmbiente is null)
            {
                throw new ArgumentNullException(nameof(tipoAmbiente));
            }

            if (string.IsNullOrEmpty(tipoAmbiente.tipoAmbienteId) || string.IsNullOrEmpty(tipoAmbiente.nombreTipoAmbiente))
            {
                return BadRequest();
            }

            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Entry(tipoAmbiente).State = EntityState.Modified;
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

        [HttpPost("eliminarTipoAmbiente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult eliminarTipoAmbiente(TipoAmbiente tipoAmbiente)
        {
            if (tipoAmbiente is null)
            {
                throw new ArgumentNullException(nameof(tipoAmbiente));
            }

            if (string.IsNullOrEmpty(tipoAmbiente.tipoAmbienteId))
            {
                return BadRequest();
            }

            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Entry(tipoAmbiente).State = EntityState.Deleted;
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
