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
            con = conexion;
        }
        [HttpGet("obtenerPorID/{id}")]
        public TipoConstruccion ObtenerPorID(string id)
        {
            TipoConstruccion tipoConstruccion = (from x in con.TipoConstruccion
                                     where x.tipoConstruccionId == id
                                     orderby x.nombreTipoConstruccion
                                     select x).FirstOrDefault();
            return tipoConstruccion;
        }

        [HttpPost("eliminarTipoConstruccion")]
        public ActionResult eliminarTipoConstruccion(TipoConstruccion tipoConstruccion)
        {
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
                    throw;
                }
                return Ok();
            }
        }
        [HttpGet("obtenerTiposConstrucciones")]
        public List<TipoConstruccion> ObtenerTodos()
        {
            List<TipoConstruccion> listadoTiposConstrucciones = (from x in con.TipoConstruccion
                                        where x.activo == true
                                        orderby x.nombreTipoConstruccion
                                        select x).ToList();
            return listadoTiposConstrucciones;
        }
        [HttpPost("crearTipoConstruccion")]
        public ActionResult CrearTipoConstruccion(TipoConstruccion tipoConstruccion)
        {
            using (var db = con.Database.BeginTransaction())
            {
                tipoConstruccion.activo = true;
                try
                {
                    con.Add(tipoConstruccion);
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
        [HttpPost("editarTipoConstruccion")]
        public ActionResult EditarTipoConstruccion(TipoConstruccion tipoConstruccion)
        {
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
                    throw;
                }
            }
            return Ok();
        }
    }
}