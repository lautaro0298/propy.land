using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ConexionDB con;
        public LoginController(ConexionDB conexion)
        {
            con = conexion;
        }
        [HttpPost]
        [Route("google-login")]
        public async Task<ActionResult> GoogleLogin([FromHeader] string token)
        {
            await con.Authenticate(token);

            return Ok();
        }
    }
}
