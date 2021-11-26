using GoldConf.Models;
using GoldConf.Repository;
using GoldConf.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldConf.Controllers
{
    [Authorize]
    public class CuentaController : Controller
    {
        private readonly ICuentaRepository context;
        private readonly IClaimService claim;

        public CuentaController(ICuentaRepository context, IClaimService claim)
        {
            this.context = context;
            this.claim = claim;
        }
        [HttpGet]
        public IActionResult Index()
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().Username != "LanRhXXX")
                ViewBag.Usuario = "LanRhXXX";

            var cuentas = context.GetCuentas()
                .Where(o => o.UserId == claim.GetLoggedUser().Id)
                .ToList();

            ViewBag.Types = new List<string> { "Efectivo", "Debito" };
            ViewBag.Currency = new List<string> { "Soles" };

            ViewBag.Total = cuentas.Sum(o => o.Amount);
            return View("Index", cuentas);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            claim.SetHttpContext(HttpContext);
            var cuentas = context.GetCuentas()
                .Where(o => o.UserId == claim.GetLoggedUser().Id)
                .ToList();
            if (cuentas.Count() == 0)
            {
                ViewBag.Types = new List<string> { "Efectivo", "Debito" };
                ViewBag.Currency = new List<string> { "Soles" };
                return View("Crear", new Cuenta());
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public IActionResult Crear(Cuenta cuenta)
        {
            claim.SetHttpContext(HttpContext);
            var cuentas = context.GetCuentas()
                .Where(o => o.UserId == claim.GetLoggedUser().Id)
                .ToList();
            if (cuentas.Count() == 0)
            {
                if (cuenta.Amount < 0)
                {
                    ModelState.AddModelError("Amount", "Valor negativo");
                }
                cuenta.UserId = claim.GetLoggedUser().Id;
                if (ModelState.IsValid)
                {
                    if (cuenta.Amount != 0)
                    {
                        cuenta.Transaccions = new List<Transaccion>
                        {
                            new Transaccion
                            {
                                FechaHora = DateTime.Now,
                                Tipo = "Ingreso",
                                Amount = cuenta.Amount,
                                Motivo = "Monto Inicial"
                            }
                        };
                    }
                    context.NewTransaccion(cuenta);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Types = new List<string> { "Efectivo", "Debito" };
                    ViewBag.Currency = new List<string> { "Soles" };
                    return View("Crear", cuenta);
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }

        [HttpGet]
        public ActionResult Transaccion(int id)
        {
            claim.SetHttpContext(HttpContext);
            if (claim.GetLoggedUser().Username != "LanRhXXX")
                ViewBag.Usuario = "LanRhXXX";

            var transacciones = context.GetTransaccions(id);
            ViewBag.Cuenta = context.GetCuentas(id);
            return View("Transaccion", transacciones);
        }
    }
}
