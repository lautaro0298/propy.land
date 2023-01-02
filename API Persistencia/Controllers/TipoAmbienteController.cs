using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_Persistencia.Models;
using Microsoft.EntityFrameworkCore;
using LibreriaClases.DTO;

namespace API_Persistencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoAmbienteController : ControllerBase
    {
        private ConexionDB con;
        public TipoAmbienteController(ConexionDB conexion)
        {
            con = conexion;
        }
        [HttpGet("obtenerPorID/{id}")]
        public TipoAmbiente ObtenerPorID(string id)
        {
            TipoAmbiente tipoAmbiente = (from x in con.TipoAmbiente where x.tipoAmbienteId == id orderby x.tipoAmbienteId ascending select x).FirstOrDefault();

            return tipoAmbiente;

        }

        [HttpGet("obtenerTiposAmbientes")]
        public List<TipoAmbiente> ObtenerTodos()
        {
            List<TipoAmbiente> listadoTiposAmbientes = (from x in con.TipoAmbiente
                                                        where x.activo == true
                                                        select x).ToList();
            return listadoTiposAmbientes;
        }
        [HttpPost("crearTipoAmbiente")]
        public ActionResult CrearTipoAmbiente(DTOTipoAmbiente DTOtipoAmbiente)
        {
            if (DTOtipoAmbiente is null)
            {
                throw new ArgumentNullException(nameof(DTOtipoAmbiente));
            }

            TipoAmbiente tipoAmbiente = new TipoAmbiente();
            tipoAmbiente.nombreTipoAmbiente = DTOtipoAmbiente.nombreTipoAmbiente;
            tipoAmbiente.activo = true;
            tipoAmbiente.tipoAmbienteId = DTOtipoAmbiente.tipoAmbienteId;
            using (var db = con.Database.BeginTransaction())
            {
                try
                {
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
            return Ok();
        }
        [HttpPost("editarTipoAmbiente")]
        public ActionResult EditarTipoAmbiente(TipoAmbiente tipoAmbiente)
        {
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
        public ActionResult eliminarTipoAmbiente(TipoAmbiente tipoAmbiente)
        {
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
                return Ok();
            }
        }
    }
}