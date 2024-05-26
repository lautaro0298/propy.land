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
    public class VisitaController : ControllerBase
    {
        private ConexionDB con;
        public VisitaController(ConexionDB conexion)
        {
            con = conexion;
        }

        [HttpGet("obtenerSolicitudContactoVisitante/{publicacionId}&{IDUsuarioVisitante}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ObtenerSolicitudContactoVisitante(string publicacionId, string IDUsuarioVisitante)
        {
            if (con == null)
            {
                return BadRequest("ConexionDB is null.");
            }

            if (string.IsNullOrEmpty(publicacionId) || string.IsNullOrEmpty(IDUsuarioVisitante))
            {
                return BadRequest("publicacionId and IDUsuarioVisitante are required.");
            }

            SolicitudContactoVisitante solicitud = (from x in con.SolicitudContactoVisitante
                                                    where x.publicacionId == publicacionId && x.usuarioId == IDUsuarioVisitante
                                                    select x).FirstOrDefault();

            if (solicitud == null)
            {
                return NotFound();
            }

            return Ok(solicitud);
        }

        [HttpPost("crearSolicitudContactoVisitante")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CrearSolicitudContactoVisitante(NoPersistidoCrearSolicitudContactoVisitante datos)
        {
            if (con == null)
            {
                return BadRequest("ConexionDB is null.");
            }

            if (datos == null || datos.solicitudContactoVisitante == null || datos.planUsuario == null)
            {
                return BadRequest("datos, solicitudContactoVisitante, and planUsuario are required.");
            }

            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.SolicitudContactoVisitante.Add(datos.solicitudContactoVisitante);
                    con.Entry(datos.planUsuario).State = EntityState.Modified;
                    con.SaveChanges();
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }

            return CreatedAtRoute("GetSolicitudContactoVisitante", new { id = datos.solicitudContactoVisitante.Id }, datos.solicitudContactoVisitante);
        }

        [HttpPost("editarSolicitudContactoVisitante")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EditarSolicitudContactoVisitante(SolicitudContactoVisitante solicitud)
        {
            if (con == null)
            {
                return BadRequest("ConexionDB is null.");
            }

            if (solicitud == null)
            {
                return BadRequest("solicitud is required.");
            }

            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Entry(solicitud).State = EntityState.Modified;
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

        [HttpPost("editarPlanusuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EditarPlanusuario(NoPersistidoPlanUsuarioVisita planUsuarioVisita)
        {
            if (con == null)
            {
                return BadRequest("ConexionDB is null.");
            }

            if (planUsuarioVisita == null || planUsuarioVisita.planUsuario == null)
            {
                return BadRequest("planUsuarioVisita and planUsuario are required.");
            }

            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Entry(planUsuarioVisita.planUsuario).State = EntityState.Modified;
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

        [HttpPost("editarVisita")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EditarVisita(VisitaInmueble visita)
        {
            if (con == null)
            {
                return BadRequest("ConexionDB is null.");
            }

            if (visita == null)
            {
                return BadRequest("visita is required.");
            }

            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Entry(visita).State = EntityState.Modified;
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

        [HttpPost("crearVisita")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CrearVisita(NoPersistidoPlanUsuarioVisita planUsuarioVisita)
        {
            if (con == null)
            {
                return BadRequest("ConexionDB is null.");
            }

            if (planUsuarioVisita == null || planUsuarioVisita.planUsuario == null || planUsuarioVisita.visita == null)
            {
                return BadRequest("planUsuarioVisita, planUsuario, and visita are required.");
            }

            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.VisitaInmueble.Add(planUsuarioVisita.visita);
                    con.Entry(planUsuarioVisita.planUsuario).State = EntityState.Modified;
                    con.SaveChanges();
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }

            return CreatedAtRoute("GetVisita", new { id = planUsuarioVisita.visita.Id }, planUsuarioVisita.visita);
        }
    }
}
