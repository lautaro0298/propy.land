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
    public class UsuarioController : ControllerBase
    {
        private ConexionDB con;
        public UsuarioController(ConexionDB conexion)
        {
            if (conexion == null)
                throw new ArgumentNullException(nameof(conexion));
            con = conexion;
        }
        [HttpPost("registrarActividad")]
        public ActionResult RegistrarActividad(Actividad actividad)
        {
            if (actividad == null)
                return BadRequest("La actividad no puede ser nula.");

            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Add(actividad);
                    con.SaveChanges();
                    db.Commit();
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    return StatusCode(500, $"Error al registrar la actividad: {ex.Message}");
                }
            }
            return Ok();
        }
        [HttpPost("editarUsuario")]
        public ActionResult EditarUsuario(Usuario usuario)
        {
            if (usuario == null)
                return BadRequest("El usuario no puede ser nulo.");

            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Entry(usuario).State = EntityState.Modified;
                    con.SaveChanges();
                    db.Commit();
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    return StatusCode(500, $"Error al editar el usuario: {ex.Message}");
                }
            }
            return Ok();
        }
        public ActionResult EditarUsuarioGoogle(Usuario usuario)
        {
            if (usuario == null)
                return BadRequest("El usuario no puede ser nulo.");

            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Entry(usuario).State = EntityState.Modified;
                    con.SaveChanges();
                    db.Commit();
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    return StatusCode(500, $"Error al editar el usuario: {ex.Message}");
                }
            }
            return Ok();
        }
        [HttpGet("obtenerUsuarioPorEmail/{email}")]
        public Usuario ObtenerUsuarioPorEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            Usuario usuario = (from x in con.Usuario
                               where x.email == email
                               select x).FirstOrDefault();
            return usuario;
        }
        [HttpGet("obtenerUsuarioPorID/{id}")]
        public Usuario ObtenerUsuarioPorID(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            Usuario usuario = (from x in con.Usuario
                               .Include(x => x.PlanUsuario).ThenInclude(x => x.Plan)
                               .Include(x => x.Favorito)
                               .Include(x => x.Actividad)
                               where x.usuarioId == id
                               select x).FirstOrDefault();
            return usuario;
        }
        [HttpPost("crearCuenta")]
        public ActionResult CrearCuenta(Usuario usuario)
        {
            if (usuario == null)
                return BadRequest("El usuario no puede ser nulo.");

            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Add(usuario);
                    con.SaveChanges();
                    db.Commit();
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    return StatusCode(500, $"Error al crear la cuenta: {ex.Message}");
                }
            }
            return Ok();
        }
    }
}
