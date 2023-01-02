using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using LibreriaClases.Transferencia;

namespace API_Persistencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPropiedadCaracteristicaController : ControllerBase
    {
        private ConexionDB con;
        public TipoPropiedadCaracteristicaController(ConexionDB conexion)
        {
            con = conexion;
        }
        [HttpPost("CrearTipoPropiedadCaracteristica")]
        public ActionResult CrearTipoPropiedadCaracteristica(TipoPropiedadCaracteristica TranstipoPropiedadCaracteristica)
        {
           
            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    //Need to check if the manufacturer already exists in the db, if it does
                    //make sure your project references the EXISTING entity within your context
                    var check = con.TipoPropiedad.Where(x => x.tipoPropiedadId == TranstipoPropiedadCaracteristica.TipopropiedadId).FirstOrDefault();
                    if (check != null)
                    {
                        TranstipoPropiedadCaracteristica.tipoPropiedad = check;
                    }
                    var check1 = con.Caracteristica.Where(x => x.caracteristicaId == TranstipoPropiedadCaracteristica.caracteristicaId).FirstOrDefault();
                    if(check1 != null)
                    {
                        TranstipoPropiedadCaracteristica.caracteristicas = check1;
                    }

                    con.Add(TranstipoPropiedadCaracteristica);
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

        [HttpGet("ObtenerPorIDdePropiedad/{id}")]
        public List<TipoPropiedadCaracteristica> obtenerPorID(string id)
        {
            List<TipoPropiedadCaracteristica> tipoPropiedadCaracteristica = (from x in con.TipoPropiedadCaracteristica
                                                                             .Include(x=>x.caracteristicas)
                                                                             where x.caracteristicaId==id  select x).ToList();//uso caracteristicaid por que es el id del tipo de propiedad se guardo al revez

            return tipoPropiedadCaracteristica;
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
        //  obtenerTiposPropiedadesycaracteristicas
        [HttpGet("obtenerTiposPropiedadesycaracteristicas")]
        public List<TipoPropiedadCaracteristica> obtenerTiposPropiedadesycaracteristicas()
        {
            List<TipoPropiedadCaracteristica> tipoPropiedades = (from x in con.TipoPropiedadCaracteristica select x).ToList<TipoPropiedadCaracteristica>();

            return tipoPropiedades;
        }
    }
}
