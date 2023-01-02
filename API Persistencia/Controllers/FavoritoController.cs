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
    public class FavoritoController : ControllerBase
    {
        private ConexionDB con;
        public FavoritoController(ConexionDB conexion)
        {
            con = conexion;
        }
        [HttpPost("crearFavorito")]
        public ActionResult CrearFavorito(Favorito favorito) {
            using (var db = con.Database.BeginTransaction()) {
                try
                {
                    con.Favorito.Add(favorito);
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
        [HttpPost("eliminarFavorito")]
        public ActionResult EliminarFavorito(Favorito favorito) {
            using (var db = con.Database.BeginTransaction()) {
                try
                {
                    con.Entry(favorito).State = EntityState.Modified;
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
