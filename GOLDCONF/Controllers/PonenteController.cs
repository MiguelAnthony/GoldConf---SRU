using System.Linq;
using GoldConf.Models;
using GoldConf.Repository;
using GoldConf.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace GoldConf.Controllers
{
    [Authorize]
    public class PonenteController : Controller
    {
        private readonly IPonenteRepository context;
        private readonly IClaimService claim;
        public readonly IHostEnvironment hostEnv;

        public PonenteController(IPonenteRepository context, IHostEnvironment hostEnv, IClaimService claim)
        {
            this.context = context;
            this.hostEnv = hostEnv;
            this.claim = claim;
        }

        [HttpGet]
        public ActionResult Ponente(string search)
        {
            claim.SetHttpContext(HttpContext); //nunca olvidarse de llamar esto para cada vez que usemos usuario
            if (claim.GetLoggedUser().Username != "LanRhXXX")
                ViewBag.Usuario = "LanRhXXX";

            ViewBag.Buscar = search;

            var ponente = context.GetPonentes();

            if (!string.IsNullOrEmpty(search))
            {
                ponente = context.GetPonentesSearch(search);
                return View("Ponente", ponente);
            }

            return View("Ponente", ponente);
        }
        [HttpGet]
        public ActionResult Detalle(int idPonente)
        {
            claim.SetHttpContext(HttpContext);
            ViewBag.Cuenta = context.GetCuentaUser()
                   .Where(o => o.UserId == claim.GetLoggedUser().Id).ToList();
            if (claim.GetLoggedUser().Username != "LanRhXXX")
            {
                ViewBag.Usuario = "LanRhXXX";
                ViewBag.Compra = context.GetCompras()
                    .Where(o => o.IdUser == claim.GetLoggedUser().Id).ToList();
            }
            else
            {
                ViewBag.Compra = context.GetCompras();
            }

            var Ponente = context.GetPonente(idPonente);

            ViewBag.Conferencias = context.GetConferenciasPonente(idPonente);

            ViewBag.Cursos = context.GetConferenciasPonente(idPonente).Count();

            return View("Detalle", Ponente);
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().Username != "LanRhXXX")
            {
                var ponentes = context.GetPonentes();
                return RedirectToAction("Ponente", ponentes);
            }
            else
            {
                return View("Registrar", new Ponente());
            }
        }
        [HttpPost]
        public ActionResult Registrar(Ponente ponente, IFormFile image, string email, string telefono)
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().Username != "LanRhXXX")
            {
                var ponentes = context.GetPonentes();
                return RedirectToAction("Ponente", ponentes);
            }
            else
            {
                var ponentes = context.GetPonentes();
                foreach (var item in ponentes)
                {
                    if (item.Email == email)
                        ModelState.AddModelError("Email", "Este email ya existe");
                    if (item.Telefono == telefono)
                        ModelState.AddModelError("Telefono", "Este numero ya existe");
                }
                if (ModelState.IsValid)
                {
                    context.NewPonente(ponente, image);
                    return RedirectToAction("Ponente");
                }
                else
                {
                    return RedirectToAction("Registrar", ponente);
                }
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().Username != "LanRhXXX")
            {
                var ponente = context.GetPonentes();
                return RedirectToAction("Ponente", ponente);
            }
            else
            {
                var ponente = context.GetPonente(id);
                return View("Edit", ponente);
            }
        }

        [HttpPost]
        public ActionResult Edit(Ponente ponente, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                context.EditPonente(ponente,image);
                return RedirectToAction("Ponente");
            }
            else
            {
                return View("Edit",ponente);
            }
        }
    }
}
