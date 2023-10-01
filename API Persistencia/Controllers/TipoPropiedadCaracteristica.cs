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
        [HttpGet("obtenerTiposPropiedadesycaracteristica")]
        public List<TipoPropiedadCaracteristica> obtenerC()
        {
            List<TipoPropiedadCaracteristica> tipoPropiedadCaracteristica = (from x in con.TipoPropiedadCaracteristica
                                                                             .Include(x => x.caracteristicas)
                                                                             select x).ToList();//uso caracteristicaid por que es el id del tipo de propiedad se guardo al revez

            return tipoPropiedadCaracteristica;
        }
        [HttpGet("ObtenerPorIDdePropiedadCaracteristica")]
        public List<TipoPropiedadCaracteristica> PropiedadCaracteristica(string id, string idPropiedad)
        {
            List<TipoPropiedadCaracteristica> tipoPropiedadCaracteristica = (from x in con.TipoPropiedadCaracteristica
                                                                where x.TipopropiedadId==idPropiedad
                                                                where x.caracteristicaId == id
                                                                select x).ToList();//uso caracteristicaid por que es el id del tipo de propiedad se guardo al revez

            return tipoPropiedadCaracteristica;
        }
        [HttpGet("ObtenerPorIDdePropiedad/{id}")]
        public List<Caracteristica> obtenerPorID(string id)
        {
            List<Caracteristica> tipoPropiedadCaracteristica = (from x in con.TipoPropiedadCaracteristica
                                                                             
                                                                             where x.caracteristicaId==id  select x.caracteristicas).ToList();//uso caracteristicaid por que es el id del tipo de propiedad se guardo al revez

            return tipoPropiedadCaracteristica;
        }
        [HttpPost("eliminarCaracteristica")]
        public List<TipoPropiedadCaracteristica> eliminarTipoAmbiente(TipoPropiedadCaracteristica dto)
        {
            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    TipoPropiedadCaracteristica tipoPropiedadCaracteristica = (from x in con.TipoPropiedadCaracteristica
                                                                                    where x.tipoPropiedadCaracteristicaID== dto.tipoPropiedadCaracteristicaID
                                                                                     select x).FirstOrDefault();
                    con.Entry(tipoPropiedadCaracteristica).State = EntityState.Deleted;
                    con.SaveChanges();
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
                return obtenerTiposPropiedadesycaracteristicas();
            }
        }
        //  obtenerTiposPropiedadesycaracteristicas
        [HttpGet("obtenerTiposPropiedadesycaracteristicas")]
        public List<TipoPropiedadCaracteristica> obtenerTiposPropiedadesycaracteristicas()
        {
            List<TipoPropiedadCaracteristica> tipoPropiedades = (from x in con.TipoPropiedadCaracteristica.Include(x => x.caracteristicas).Include(x=> x.tipoPropiedad) select x).ToList<TipoPropiedadCaracteristica>();

            return tipoPropiedades;
        }
    }
}
