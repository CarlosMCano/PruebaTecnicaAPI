using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Prueba.Modelos;
using Prueba.Modelos.BD;
using Prueba.Modelos.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly Context _context;

        public LoginController(Context context)
        {
            _context = context;
        }

        [HttpPost, Route("autenticar")]
        public ActionResult Get(LoginModel datos)
        {
            try
            {
                string clave = Utilidad.GenerarClave(datos.Clave);
                Usuario usuarioValido = _context.Usuarios.FirstOrDefault(c => c.Correo == datos.Correo && c.Clave == clave && c.Estado == "1");

                if (usuarioValido == null)
                    throw new Exception("Usuario no valido");

                if (usuarioValido.Bloqueo == "1")
                    throw new Exception("Usuario bloqueado");        

                var fechahoy = DateTime.Now.AddMinutes(120); //TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));            

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, usuarioValido.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Email, usuarioValido.Correo));

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);


                var tokeOptions = new JwtSecurityToken(
#if DEBUG
                    issuer: "http://localhost:44316",
                    audience: "http://localhost:44316",
#else
                    issuer: "http://192.168.200.13:8045",
                    audience: "http://192.168.200.13:8045",
#endif
                    claims: claims, //new List<Claim>(),
                                    //notBefore: fechahoy,
                    expires: fechahoy,

                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);


                VMLogin data = new VMLogin(tokenString);

                return Ok(new { estado = true, data = data, mensaje = "" });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
