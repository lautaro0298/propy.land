using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace API_Persistencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPropiedadTipoAmbienteController : Controller
    {
        private ConexionDB con;
        public TipoPropiedadTipoAmbienteController(ConexionDB conexion)
        {
            con = conexion;
        }
        [HttpPost("crearTipoPropiedadTipoAmbiente")]
        public ActionResult CrearTipoPropiedad(TipoPropiedadTipoAmbiente tipoPropiedadTipoAmbiente)
        {
            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Add(tipoPropiedadTipoAmbiente);
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
        [HttpPost("EliminarTipoPropiedadTipoAmbiente")]
        public ActionResult eliminarTipoPropiedad(TipoPropiedadTipoAmbiente tipoPropiedad)
        {
            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Entry(tipoPropiedad).State = EntityState.Deleted;
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
        [HttpGet("ObtenerPorID/{id}")]
        public TipoPropiedadTipoAmbiente obtenerPorID(string id)
        {
            TipoPropiedadTipoAmbiente tipoPropiedadTipoAmbiente = (from x in con.TipoPropiedadTipoAmbiente where x.tipoPropiedadTipoAmbienteId == id select x).FirstOrDefault();

            return tipoPropiedadTipoAmbiente;
        }
        [HttpGet("Obtener")]
        public List<TipoPropiedadTipoAmbiente> obtener()
        {
            List<TipoPropiedadTipoAmbiente> tipoPropiedadTipoAmbiente = (from x in con.TipoPropiedadTipoAmbiente select x).ToList() ;

            return tipoPropiedadTipoAmbiente;
        }
    }
}