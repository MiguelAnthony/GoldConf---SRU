using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GoldConf.Models;
using GoldConf.Repository;
using GoldConf.Service;

namespace GoldConf.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeRepository context;

        public HomeController(IHomeRepository context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            try
            {
                var ponente = context.GetPonentes();
                ViewBag.Conferencia = context.GetConferencias();
                return View("Index", ponente);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
