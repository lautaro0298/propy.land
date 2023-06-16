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
        public SolicitudContactoVisitante ObtenerSolicitudContactoVisitante(string publicacionId,string IDUsuarioVisitante) {
            SolicitudContactoVisitante solicitud = (from x in con.SolicitudContactoVisitante
                                                    where x.publicacionId == publicacionId && x.usuarioId == IDUsuarioVisitante
                                                    select x).FirstOrDefault();
            return solicitud;
        }
        [HttpPost("crearSolicitudContactoVisitante")]
        public ActionResult CrearSolicitudContactoVisitante(NoPersistidoCrearSolicitudContactoVisitante datos) {
            using (var db = con.Database.BeginTransaction()) {
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
                return Ok();
            }
        }
        [HttpPost("editarSolicitudContactoVisitante")]
        public ActionResult EditarSolicitudContactoVisitante(SolicitudContactoVisitante solicitud) {
            using (var db = con.Database.BeginTransaction()) {
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
                return Ok();
            }
        }
        [HttpPost("editarPlanusuario")]
        public ActionResult EditarPlanusuario( NoPersistidoPlanUsuarioVisita planUsuarioVisita)
        {
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
                return Ok();
            }
        }
        [HttpPost("editarVisita")]
        public ActionResult EditarVisita(VisitaInmueble visita) {
            using (var db = con.Database.BeginTransaction()) {
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
                return Ok();
            }
        }
        [HttpPost("crearVisita")]
        public ActionResult CrearVisita(NoPersistidoPlanUsuarioVisita planUsuarioVisita) {
            using (var db = con.Database.BeginTransaction()) {
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
            return Ok();
                
        }
    }
}
