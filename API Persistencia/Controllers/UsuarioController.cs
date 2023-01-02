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
            con = conexion;
        }
        [HttpPost("registrarActividad")]
        public ActionResult RegistrarActividad(Actividad actividad) {
            using (var db = con.Database.BeginTransaction()) {
                try
                {
                    con.Add(actividad);
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
        [HttpPost("editarUsuario")]
        public ActionResult EditarUsuario(Usuario usuario) {
            using (var db = con.Database.BeginTransaction()) {
                try
                {
                    con.Entry(usuario).State = EntityState.Modified;
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
        [HttpGet("obtenerUsuarioPorEmail/{email}")]
        public Usuario ObtenerUsuarioPorEmail(string email) {
            Usuario usuario = (from x in con.Usuario
                               where x.email == email
                               select x).FirstOrDefault();
            return usuario;
        }
        [HttpGet("obtenerUsuarioPorID/{id}")]
        public Usuario ObtenerUsuarioPorID(string id)
        {
            Usuario usuario = (from x in con.Usuario
                               .Include(x=>x.PlanUsuario).ThenInclude(x=>x.Plan)
                               .Include(x=>x.Favorito)
                               .Include(x=>x.Actividad)
                               where x.usuarioId == id
                               select x).FirstOrDefault();
            return usuario;
        }
        [HttpPost("crearCuenta")]
        public ActionResult CrearCuenta(Usuario usuario) {
            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Add(usuario);
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