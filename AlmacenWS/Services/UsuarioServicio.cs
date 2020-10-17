using AlmacenWS.Models;
using AlmacenWS.Models.Common;
using AlmacenWS.Models.Response;
using AlmacenWS.Tools;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AlmacenWS.Services
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly AppSettings _appSettings;

        public UsuarioServicio(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public UsuarioRespuesta Auth(Autenticacion autenticacion)
        {
            UsuarioRespuesta usuarioRespuesta = new UsuarioRespuesta();
            using (var db = new Almacen_dbContext())
            {
                string password = Encrypt.GetSHA256(autenticacion.Password);
                var usuario = db.Usuario.Where(u => u.Email == autenticacion.Email && u.Password == password).FirstOrDefault();
                if (usuario == null) return null;
                usuarioRespuesta.Email = usuario.Email;
                usuarioRespuesta.Token = GetToken(usuario);
            }
            return usuarioRespuesta;
        }

        private string GetToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier,usuario.IdUsuario.ToString()),
                        new Claim(ClaimTypes.Email,usuario.Email)
                    }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),  SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
