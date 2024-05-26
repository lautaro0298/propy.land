using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Persistencia.Models;
using API_Persistencia.Models.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Persistencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusquedaController : ControllerBase
    {
        private readonly ConexionDB con;
        public BusquedaController(ConexionDB conexion)
        {
            con = conexion;
        }

        [HttpGet("obtenerPropiedadesParaEvaluarBusqueda")]
        public async Task<ActionResult<IEnumerable<Publicacion>>> ObtenerPropiedadesParaEvaluarBusqueda(int page = 1, int pageSize = 10)
        {
            try
            {
                var publicaciones = await con.Publicacion
                    .Include(x => x.Propiedad)
                    .Include(x => x.Caracteristicas)
                    .Include(x => x.Propiedad).ThenInclude(x => x.ImagenPropiedad)
                    .Include(x => x.Propiedad).ThenInclude(x => x.PropiedadTipoAmbiente).ThenInclude(x => x.TipoAmbiente)
                    .Include(x => x.Propiedad).ThenInclude(x => x.TipoConstruccion)
                    .Include(x => x.TipoPublicacion)
                    .Include(x => x.Propiedad).ThenInclude(x => x.TipoPublicante)
                    .Include(x => x.Propiedad).ThenInclude(x => x.TipoMoneda)
                    .Where(x => x.estado == 0)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Ok(publicaciones);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("buscardortipoPropiedad")]
        public async Task<ActionResult<IEnumerable<Publicacion>>> filtarporTipoPropiedad(string[] tipoPropiedadId, int page = 1, int pageSize = 10)
        {
            try
            {
                if (tipoPropiedadId == null || !tipoPropiedadId.Any())
                {
                    return BadRequest("Invalid query");
                }

                var publicaciones = await con.Publicacion
                    .Include(x => x.Propiedad)
                    .Include(x => x.Caracteristicas)
                    .Include(x => x.Propiedad).ThenInclude(x => x.ImagenPropiedad)
                    .Include(x => x.Propiedad).ThenInclude(x => x.PropiedadTipoAmbiente).ThenInclude(x => x.TipoAmbiente)
                    .Include(x => x.Propiedad).ThenInclude(x => x.TipoConstruccion)
                    .Include(x => x.TipoPublicacion)
                    .Include(x => x.Propiedad).ThenInclude(x => x.TipoPublicante)
                    .Include(x => x.Propiedad).ThenInclude(x => x.TipoMoneda)
                    .Where(x => tipoPropiedadId.Contains(x.Propiedad.tipoPropiedadId))
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Ok(publicaciones);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
           
