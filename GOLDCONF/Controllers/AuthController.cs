using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using GoldConf.Models;
using GoldConf.Repository;
using GoldConf.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GoldConf.Controllers
{

    public class AuthController : Controller
    {
        private readonly IAuthRepository context;
        private readonly IClaimService claim;

        public AuthController(IAuthRepository context, IClaimService claim)
        {
            this.context = context;
            this.claim = claim;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = context.GetUsuario(username,password);

            if (user != null)
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                claim.SetHttpContext(HttpContext);
                claim.Login(claimsPrincipal);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("Login", "Usuario o contraseña incorrectos.");
            return View("Login");
        }
        [HttpGet]
        public IActionResult Logout()
        {
            claim.SetHttpContext(HttpContext);
            claim.Logout();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Registrar()
        {
            return View("Registrar");
        }

        [HttpPost]
        public ActionResult Registrar(User user, string passwordConf)
        {
            var usuarios = context.GetUsuarios();
            foreach (var item in usuarios)
            {
                if (item.Email == user.Email)
                    ModelState.AddModelError("Email", "Este email ya existe");
                if (item.Username == user.Username)
                    ModelState.AddModelError("Username", "Este usuario ya existe");
            }

            if (user.Password != passwordConf)
                ModelState.AddModelError("PasswordConf", "Las contraseñas no coinciden");

            if (ModelState.IsValid)
            {
                context.SaveUsuario(user);
                return RedirectToAction("Login");
            }
            return View("Registrar", user);
        }
    }
}
