using GoldConf.Models;
using GoldConf.Repository;
using GoldConf.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace GoldConf.Controllers
{
    [Authorize]
    public class ConferenciaController : Controller
    {
        private readonly IConferenciaRepository context;
        private readonly IClaimService claim;

        public ConferenciaController(IConferenciaRepository ctx, IClaimService claim)
        {
            this.context = ctx;
            this.claim = claim;
        }
        [HttpGet]
        public ActionResult Conferencias(string search)
        {
            claim.SetHttpContext(HttpContext);
            var ponentes = context.GetPonentes();
            var conferencias = context.GetConferencias();
            ViewBag.Cuenta = context.GetCuentas()
                .Where(o => o.UserId == claim.GetLoggedUser().Id)
                .ToList();


            if (claim.GetLoggedUser().Username != "LanRhXXX")
            {
                ViewBag.Usuario = "LanRhXXX";
                conferencias = context.GetConferenciasOrder();
                ViewBag.Compra = context.GetCompras().
                    Where(o => o.IdUser == claim.GetLoggedUser().Id).
                    ToList();
            }
            else
            {
                conferencias = context.GetConferenciasOrderUC();
                ViewBag.Compra = context.GetCompras();
            }

            ViewBag.Buscar = search;

            ViewBag.IdUser = claim.GetLoggedUser().Id;

            if (!string.IsNullOrEmpty(search))
            {
                conferencias = conferencias.Where(s => s.Ponentes.NomApe.Contains(search) || s.TituloConf.Contains(search)).ToList();
                return View("Conferencias", conferencias);
            }

            return View("Conferencias", conferencias);
        }

        public ActionResult Detalle(int idConferencia)
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().Username != "LanRhXXX")
                ViewBag.Usuario = "LanRhXXX";

            var compra = context.GetCompras().Where(o => o.IdConferencia == idConferencia).ToList();
            ViewBag.Compra = compra.Count();

            ViewBag.Ponentes = context.GetPonentes();

            var conferencias = context.GetConferenciasOrderUC()
                .Where(o => o.Id == idConferencia)
                .FirstOrDefault();

            ViewBag.IdUser = claim.GetLoggedUser().Id;

            return View("Detalle", conferencias);
        }

        [HttpGet]
        public ActionResult Create()
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().Username != "LanRhXXX")
            {
                var conferencias = context.GetConferenciasOrderUC();
                return RedirectToAction("Conferencias", conferencias);
            }
            else
            {
                ViewBag.Ponentes = context.GetPonentes();
                return View("Create", new Conferencia());
            }
        }
        [HttpPost]
        public ActionResult Create(Conferencia conferencia, IFormFile image)
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().Username != "LanRhXXX")
            {
                var conferencias = context.GetConferenciasOrderUC();
                return RedirectToAction("Conferencias", conferencias);
            }
            else
            {
                if (conferencia == null)
                    ModelState.AddModelError("Conferencia", "Inserte los datos adecuados");
                if (ModelState.IsValid)
                {
                    context.NewConferencia(conferencia, image);
                    return RedirectToAction("Conferencias");
                }
                else
                {
                    ViewBag.Ponentes = context.GetPonentes();
                    return View("Create", conferencia);
                }
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().Username != "LanRhXXX")
            {
                var conferencias = context.GetConferenciasOrderUC()
                    .Where(o => o.Id == id)
                    .FirstOrDefault();
                return RedirectToAction("Conferencias", conferencias);
            }
            else
            {
                ViewBag.Ponentes = context.GetPonentes();
                var account = context.GetConferencias().Where(o => o.Id == id).FirstOrDefault();
                return View("Edit", account);
            }
        }
        [HttpPost]
        public ActionResult Edit(Conferencia conferencia, IFormFile image)
        {
            claim.SetHttpContext(HttpContext);
            if (conferencia == null)
                ModelState.AddModelError("Conferencia", "Inserte los datos adecuados");
            if (ModelState.IsValid)
            {
                context.EditConferencia(conferencia,image);
                return RedirectToAction("Conferencias");
            }
            else
            {
                ViewBag.Ponentes = context.GetPonentes();
                return View("Edit", conferencia);
            }
        }

        [HttpGet]
        public ActionResult Comprar(int idF, string titulo, decimal monto)
        {
            claim.SetHttpContext(HttpContext);
            var cuenta = context.GetCuentas().
                Where(o => o.UserId == claim.GetLoggedUser().Id).
                FirstOrDefault();
            if (cuenta.Amount >= monto )
            {
                var compra = context.GetCompras();

                foreach (var item in compra)
                {
                    if (item.IdUser == claim.GetLoggedUser().Id && item.IdConferencia == idF)
                    {
                        TempData["COMPRA"] = "Esta conferencia ya ha sido comprada";
                        ModelState.AddModelError("Error", "Conferencia ya comprada");
                    }
                }
                if (ModelState.IsValid)
                {
                    var transaccionCli = new Transaccion
                    {
                        CuentaId = cuenta.Id,
                        FechaHora = DateTime.Now,
                        Tipo = "Compra",
                        Amount = monto * -1,
                        Motivo = "Conferencia " + titulo + " Comprada"
                    };
                    var transaccionAd = new Transaccion
                    {
                        CuentaId = 1004,
                        FechaHora = DateTime.Now,
                        Tipo = "Venta",
                        Amount = monto,
                        Motivo = "Conferencia " + titulo + " Comprada"
                    };
                    context.AddTransaccion(transaccionCli, transaccionAd);
                    context.ModificaMontoCuenta(transaccionCli.CuentaId);
                    context.ModificaMontoCuenta(transaccionAd.CuentaId);
                    context.AddCompras(claim.GetLoggedUser().Id, idF);

                    return RedirectToAction("Conferencias");
                }
            }
            return RedirectToAction("Conferencias");
        }

        [HttpGet]
        public ActionResult Pasadas(string search)
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().Username != "LanRhXXX")
                ViewBag.Usuario = "LanRhXXX";

            var ponente = context.GetPonentes();

            var conferencia = context.GetConferenciasOrderUC();

            var compra = context.GetComprasOrderPasado(claim.GetLoggedUser().Id);

            DateTime fecha = DateTime.Today;
            ViewBag.fecha = fecha;

            ViewBag.Buscar = search;

            if (!string.IsNullOrEmpty(search))
            {
                compra = compra.Where(s => s.Conferencia.Ponentes.NomApe.Contains(search) || s.Conferencia.TituloConf.Contains(search)).ToList();
                return View("Pasadas", compra); ;
            }
            return View("Pasadas", compra); ;
        }
        [HttpGet]
        public ActionResult Futuras(string search)
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().Username != "LanRhXXX")
                ViewBag.Usuario = "LanRhXXX";

            var ponente = context.GetPonentes();

            var conferencia = context.GetConferenciasOrderUC();

            var compra = context.GetComprasOrderFuturas(claim.GetLoggedUser().Id);

            DateTime fecha = DateTime.Today;
            ViewBag.fecha = fecha;

            ViewBag.Buscar = search;

            if (!string.IsNullOrEmpty(search))
            {
                compra = compra.Where(s => s.Conferencia.Ponentes.NomApe.Contains(search) || s.Conferencia.TituloConf.Contains(search)).ToList();
                return View("Futuras", compra);
            }
            return View("Futuras", compra);
        }

    }
}
