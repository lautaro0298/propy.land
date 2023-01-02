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
        public List<Caracteristica> ObtenerTodos() {
            List<Caracteristica> listadoCaracteristicas = (from x in con.Caracteristica
                                                           where x.activo == true
                                                           orderby x.nombreCaracteristica
                                                           select x).ToList();
            return listadoCaracteristicas;
        }
        [HttpGet("obtenerPorID/{id}")]
        public Caracteristica ObtenerPorID(string id)
        {
            Caracteristica caracteristica = (from x in con.Caracteristica
                         where x.caracteristicaId == id
                         select x).FirstOrDefault();
            return caracteristica;
        }

        [HttpPost("crearCaracteristica")]
        public ActionResult CrearCaracteristica(Caracteristica caracteristica) {
            caracteristica.activo = true;
            using (var db = con.Database.BeginTransaction()) {
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
            return Ok();
        }
        [HttpPost("editarCaracteristica")]
        public ActionResult EditarCaracteristica(Caracteristica caracteristica) {
            using (var db = con.Database.BeginTransaction()) {
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
            return Ok();
        }
        [HttpPost("eliminarCaracteristica")]
        public ActionResult eliminarTipoAmbiente(Caracteristica caracteristica)
        {
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
                return Ok();
            }
        }
    }
}