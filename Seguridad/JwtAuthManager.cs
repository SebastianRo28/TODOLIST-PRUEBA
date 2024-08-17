using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;

using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace ToDoListNuevo.Seguridad
{
    public class JwtAuthManager : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += (sender, e) =>
            {
                var authHeader = context.Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Bearer "))
                {
                    var token = authHeader.Substring("Bearer ".Length).Trim();
                    ValidateToken(token);
                }
            };
        }

        private void ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("sDFLkjfksdJFl4jZ234jksjdLJ34klj2==");

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userName = jwtToken.Claims.First(x => x.Type == ClaimTypes.Name).Value;

                HttpContext.Current.User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userName) }, "jwt"));
            }
            catch
            {
                
                
            }
        }

        public void Dispose() { }
    }
}