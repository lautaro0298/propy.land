using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Persistencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoMPController : ControllerBase
    {
        private ConexionDB con;

        public PagoMPController(ConexionDB conexion)
        {
            con = conexion;
        }

        [HttpPost("GuardarPago")]
        public ActionResult GuardarPago(PagoMP Pago)
        {
            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Add(Pago);
                    con.SaveChanges();
                    db.Commit();
                }
                catch (Exception error)
                {
                    db.Rollback();
                    throw error;
                }
            }
            return Ok();
        }

        [HttpGet("ObtenerPago/{id}")]
        public PagoMP ObtenerPago(string id)
        {
            PagoMP pago = (from x in con.PagoMP where x.PagoMPId == id select x).FirstOrDefault();

            return pago;
        }
    }
}
