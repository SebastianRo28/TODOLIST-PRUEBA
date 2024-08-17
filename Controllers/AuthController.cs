using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ToDoListPruebaTecnica.Models;

namespace ToDoListNuevo.Controllers
{
    public class AuthController : Controller
    {
        private ToDoListDbContext db = new ToDoListDbContext();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = db.Users.SingleOrDefault(u => u.Username == username);
            if (user != null && VerifyPassword(password, user.PasswordHash))
            {
                var token = GenerateJwtToken(user.Username);
                return Json(new { success = true, userId = user.Id, username = user.Username, token });
            }

            return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            // Lógica para verificar el hash de la contraseña
            return true;
        }

        private string GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("sDFLkjfksdJFl4jZ234jksjdLJ34klj2==");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
