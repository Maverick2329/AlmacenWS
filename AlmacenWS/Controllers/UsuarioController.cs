using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlmacenWS.Models.Response;
using AlmacenWS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlmacenWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioServicio _usuarioServicio;

        public UsuarioController(IUsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        [HttpPost("login")]
        public IActionResult Autenticacion([FromBody] Autenticacion autenticacion)
        {
            Respuesta respuesta = new Respuesta();
            var usuarioRespuesta = _usuarioServicio.Auth(autenticacion);
            if (usuarioRespuesta == null)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Usuario o Contraseña Incorrecta";
                return BadRequest(respuesta);
            }
            respuesta.Exito = 1;
            respuesta.Data = usuarioRespuesta;
            return Ok(respuesta);
        }
    }
}
