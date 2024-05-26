using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using API_Persistencia.Models;
using LibreriaClases.Transferencia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Persistencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditoController : ControllerBase
    {
        private readonly ConexionDB _con;

        public CreditoController(ConexionDB conexion)
        {
            _con = conexion;
        }

        [HttpGet("obtenerCredito")]
        public async Task<ActionResult<List<Credito>>> ObtenerTodos()
        {
            List<Credito> listadoCredito = await _con.Credito
                .Include(x => x.TipoMoneda)
                .Where(x => x.Activo == true)
                .OrderBy(x => x.NombrePack)
                .ToListAsync();

            return Ok(listadoCredito);
        }

        [HttpPost("crearPack")]
        public async Task<ActionResult<Credito>> CrearPlan(Credito credito)
        {
            if (credito == null)
            {
                return BadRequest("Credito object is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var db = _con.Database.BeginTransaction())
            {
                try
                {
                    _con.Add(credito);
                    await _con.SaveChangesAsync();
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }

            return CreatedAtAction(nameof(ObtenerPorID), new { id = credito.PaqueteID }, credito);
        }

        [HttpGet("Obtener_Packs_Creditos")]
        public async Task<ActionResult<List<Credito>>> Obtener_Packs_Creditos()
        {
            List<Credito> creditos = await _con.Credito
                .Include(x => x.TipoMoneda)
                .Where(x => x.Activo == true)
                .OrderBy(x => x.Precio)
                .ToListAsync();

            return Ok(creditos);
        }

        [HttpGet("ObtenerPorID/{id}")]
        public async Task<ActionResult<Credito>> ObtenerPorID(string id)
        {
            Credito credito = await _con.Credito
                .Include(x => x.TipoMoneda)
                .FirstOrDefaultAsync(x => x.PaqueteID == id);

            if (credito == null)
            {
                return NotFound();
            }

            return Ok(credito);
        }

        [HttpPost("EditarCredito")]
        public async Task<ActionResult> EditarCaracteristica(Credito credito)
        {
            if (credito == null)
            {
                return BadRequest("Credito object is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var db = _con.Database.BeginTransaction())
            {
                try
                {
                    _con.Entry(credito).State = EntityState.Modified;
                    await _con.SaveChangesAsync();
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost("eliminarCredito")]
        public async Task<ActionResult> eliminarCredito(Credito credito)
        {
            if (credito == null)
            {
                return BadRequest("Credito object is null.");
            }

            Credito existingCredito = await _con.Credito.FindAsync(credito.PaqueteID);

            if (existingCredito == null)
            {
                return NotFound();
            }

            using (var db = _con.Database.BeginTransaction())
            {
                try
                {
                    _con.Entry(existingCredito).State = EntityState.Deleted;
                    await _con.SaveChangesAsync();
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }

            return NoContent();
        }
    }
}
