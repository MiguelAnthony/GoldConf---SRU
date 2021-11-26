using GoldConf.Controllers;
using GoldConf.Models;
using GoldConf.Repository;
using GoldConf.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldConf.Test.Controllers
{
    [TestFixture]
    class CuentaControllerTest
    {
        [Test]
        public void CasoCuentaIndex()
        {
            var repository = new Mock<ICuentaRepository>();
            var cuentas = repository.Setup(o => o.GetCuentas()).Returns(new List<Cuenta>());

            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User());

            var controller = new CuentaController(repository.Object, claim.Object);
            var view = controller.Index() as ViewResult;

            var views = view.StatusCode = 200;

            Assert.IsNotNull(view);
            Assert.AreEqual("Index", view.ViewName);
            Assert.AreEqual(views, 200);
        }
        
        [Test]
        public void CasoCuentaCrear()
        {
            var repository = new Mock<ICuentaRepository>();
            var cuentas = repository.Setup(o => o.GetCuentas()).Returns(new List<Cuenta>());

            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User() { Id = 5});

            var controller = new CuentaController(repository.Object, claim.Object);
            var view = controller.Crear() as ViewResult;

            var views = view.StatusCode = 200;

            Assert.IsNotNull(view);
            Assert.AreEqual("Crear", view.ViewName);
            Assert.AreEqual(views, 200);
        }

        [Test]
        public void CasoCuentaCrearE()
        {
            var repository = new Mock<ICuentaRepository>();
            repository.Setup(o => o.GetCuentas()).Returns(new List<Cuenta>() { new Cuenta() { UserId = 5 } });

            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User() { Id = 5 });

            var controller = new CuentaController(repository.Object, claim.Object);
            var view = controller.Crear() as RedirectToActionResult;

            Assert.IsNotNull(view);
            Assert.AreEqual("Index", view.ActionName);
        }

        [Test]
        public void CasoCuentaCrearPost() //TODO solucionar XD verificar un par de pruebas mas
        {
            var repository = new Mock<ICuentaRepository>();
            repository.Setup(o => o.GetCuentas()).Returns(new List<Cuenta>());

            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User() { Id = 5 });

            var controller = new CuentaController(repository.Object, claim.Object);
            var view = controller.Crear(new Cuenta()) as RedirectToActionResult;

            Assert.IsNotNull(view);
            Assert.AreEqual("Index", view.ActionName);
        }

        [Test]
        public void CasoCuentaCrearPostIFTrue()
        {
            var repository = new Mock<ICuentaRepository>();
            repository.Setup(o => o.GetCuentas()).Returns(new List<Cuenta>(){new Cuenta(){UserId = 1}});
            
            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User(){Id = 1});

            var controller = new CuentaController(repository.Object, claim.Object);

            var view = controller.Crear(new Cuenta()) as RedirectToActionResult;

            Assert.IsNotNull(view);
            Assert.AreEqual("Index", view.ActionName);
        }

        [Test]
        public void CasoCuentaTransaccion()
        {
            var repository = new Mock<ICuentaRepository>();
            repository.Setup(o => o.GetTransaccions(1)).Returns(new List<Transaccion>());
            repository.Setup(o => o.GetCuentas(1)).Returns(new Cuenta());

            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User());

            var controller = new CuentaController(repository.Object, claim.Object);
            var view = controller.Transaccion(1) as ViewResult;
            Assert.IsNotNull(view);
            Assert.AreEqual("Transaccion", view.ViewName);
        }
    }
}
