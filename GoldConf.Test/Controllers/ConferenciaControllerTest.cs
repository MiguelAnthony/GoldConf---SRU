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
    class ConferenciaControllerTest
    {
        [Test]
        public void CasoConferenciaConferencias()
        {
            var repo = new Mock<IConferenciaRepository>();
            repo.Setup(o => o.GetPonentes()).Returns(new List<Ponente>());
            repo.Setup(o => o.GetConferencias()).Returns(new List<Conferencia>());
            repo.Setup(o => o.GetCuentas()).Returns(new List<Cuenta>());
            repo.Setup(o => o.GetConferenciasOrder()).Returns(new List<Conferencia>());
            repo.Setup(o => o.GetCompras()).Returns(new List<Comprar>());
            repo.Setup(o => o.GetConferenciasOrderUC()).Returns(new List<Conferencia>());
            
            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User());

            var controller = new ConferenciaController(repo.Object, claim.Object);
            var view = controller.Conferencias(null) as ViewResult;
            var views = view.StatusCode = 200;

            Assert.IsNotNull(view);
            Assert.AreEqual("Conferencias", view.ViewName);
            Assert.AreEqual(views, 200);
        }

        [Test]
        public void CasoConferenciaDetalle()
        {
            var repo = new Mock<IConferenciaRepository>();
            repo.Setup(o => o.GetPonentes()).Returns(new List<Ponente>());
            repo.Setup(o => o.GetCompras()).Returns(new List<Comprar>());
            repo.Setup(o => o.GetConferenciasOrderUC()).Returns(new List<Conferencia>());

            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User());

            var controller = new ConferenciaController(repo.Object, claim.Object);
            var view = controller.Detalle(1) as ViewResult;
            var views = view.StatusCode = 200;

            Assert.IsNotNull(view);
            Assert.AreEqual("Detalle", view.ViewName);
            Assert.AreEqual(views, 200);
        }

        [Test]
        public void CasoConferenciaCreateGetAdmin()
        {
            var repo = new Mock<IConferenciaRepository>();
            repo.Setup(o => o.GetPonentes()).Returns(new List<Ponente>());

            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User() { Username = "LanRhXXX" });

            var controller = new ConferenciaController(repo.Object, claim.Object);
            var view = controller.Create() as ViewResult;
            var views = view.StatusCode = 200;

            Assert.IsNotNull(view);
            Assert.AreEqual("Create", view.ViewName);
            Assert.AreEqual(views, 200);
        }

        [Test]
        public void CasoConferenciaCreateGetComun()
        {
            var repo = new Mock<IConferenciaRepository>();
            repo.Setup(o => o.GetConferenciasOrderUC()).Returns(new List<Conferencia>());

            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User() { Username = "User" });

            var controller = new ConferenciaController(repo.Object, claim.Object);
            var view = controller.Create() as RedirectToActionResult;

            Assert.AreEqual("Conferencias", view.ActionName);
        }

        [Test]
        public void CasoConferenciaCreatePostAdminBien()
        {
            var repo = new Mock<IConferenciaRepository>();
            repo.Setup(o => o.NewConferencia(new Conferencia(),null));

            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User() { Username = "LanRhXXX" });

            var controller = new ConferenciaController(repo.Object, claim.Object);
            var view = controller.Create(new Conferencia(),null) as RedirectToActionResult;

            Assert.AreEqual("Conferencias", view.ActionName);
        }

        [Test]
        public void CasoConferenciaCreatePostAdminMal()
        {
            var repo = new Mock<IConferenciaRepository>();
            repo.Setup(o => o.GetPonentes()).Returns(new List<Ponente>());

            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User() { Username = "LanRhXXX" });

            var controller = new ConferenciaController(repo.Object, claim.Object);
            var view = controller.Create(null, null) as ViewResult;

            Assert.AreEqual("Create", view.ViewName);
        }

        [Test]
        public void CasoConferenciaCreatePostUser()
        {
            var repo = new Mock<IConferenciaRepository>();
            repo.Setup(o => o.GetConferenciasOrderUC()).Returns(new List<Conferencia>());

            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User() { Username = "User" });

            var controller = new ConferenciaController(repo.Object, claim.Object);
            var view = controller.Create(null, null) as RedirectToActionResult;

            Assert.AreEqual("Conferencias", view.ActionName);
        }

        [Test]
        public void CasoConferenciaEditGetAdmin()
        {
            var repo = new Mock<IConferenciaRepository>();
            repo.Setup(o => o.GetPonentes()).Returns(new List<Ponente>());
            repo.Setup(o => o.GetConferencias()).Returns(new List<Conferencia>());

            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User() { Username = "LanRhXXX" });

            var controller = new ConferenciaController(repo.Object, claim.Object);
            var view = controller.Edit(1) as ViewResult;
            var views = view.StatusCode = 200;

            Assert.IsNotNull(view);
            Assert.AreEqual("Edit", view.ViewName);
            Assert.AreEqual(views, 200);
        }

        [Test]
        public void CasoConferenciaEditGetComun()
        {
            var repo = new Mock<IConferenciaRepository>();
            repo.Setup(o => o.GetConferenciasOrderUC()).Returns(new List<Conferencia>());

            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User() { Username = "User" });

            var controller = new ConferenciaController(repo.Object, claim.Object);
            var view = controller.Edit(1) as RedirectToActionResult;

            Assert.AreEqual("Conferencias", view.ActionName);
        }

        [Test]
        public void CasoConferenciaEditPostAdminBien()
        {
            var repo = new Mock<IConferenciaRepository>();
            repo.Setup(o => o.NewConferencia(new Conferencia(), null));

            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User() { Username = "LanRhXXX" });

            var controller = new ConferenciaController(repo.Object, claim.Object);
            var view = controller.Edit(new Conferencia(), null) as RedirectToActionResult;

            Assert.AreEqual("Conferencias", view.ActionName);
        }

        [Test]
        public void CasoConferenciaEditPostAdminMal()
        {
            var repo = new Mock<IConferenciaRepository>();
            repo.Setup(o => o.GetPonentes()).Returns(new List<Ponente>());

            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User() { Username = "LanRhXXX" });

            var controller = new ConferenciaController(repo.Object, claim.Object);
            var view = controller.Edit(null, null) as ViewResult;

            Assert.AreEqual("Edit", view.ViewName);
        }

        [Test]
        public void CasoConferenciaComprarVacio()
        {
            var repo = new Mock<IConferenciaRepository>();
            repo.Setup(o => o.GetCuentas()).Returns(new List<Cuenta>());
            
            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User());

            var controller = new ConferenciaController(repo.Object, claim.Object);
            var view = controller.Comprar(null,1,null,1.0m) as RedirectToActionResult;

            Assert.AreEqual("Conferencias", view.ActionName);
        }

        [Test]
        public void CasoConferenciaComprarElemento()
        {
            var repo = new Mock<IConferenciaRepository>();
            repo.Setup(o => o.GetCuentas()).Returns(new List<Cuenta>());
            repo.Setup(o => o.GetCompras()).Returns(new List<Comprar>());
            repo.Setup(o => o.AddTransaccion(new Transaccion(),new Transaccion()));
            repo.Setup(o => o.ModificaMontoCuenta(1));
            repo.Setup(o => o.ModificaMontoCuenta(2));
            repo.Setup(o => o.AddCompras(new Comprar(), 1, 1));

            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User());

            var controller = new ConferenciaController(repo.Object, claim.Object);
            var view = controller.Comprar(new Comprar(), 1, null, 1.0m) as RedirectToActionResult;

            Assert.AreEqual("Conferencias", view.ActionName);
        }

        [Test]
        public void CasoConferenciaPasadas()
        {
            var repo = new Mock<IConferenciaRepository>();
            repo.Setup(o => o.GetPonentes()).Returns(new List<Ponente>());
            repo.Setup(o => o.GetConferenciasOrderUC()).Returns(new List<Conferencia>());
            repo.Setup(o => o.GetComprasOrderPasado(1)).Returns(new List<Comprar>());

            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User());

            var controller = new ConferenciaController(repo.Object, claim.Object);
            var view = controller.Pasadas(null) as ViewResult;
            var views = view.StatusCode = 200;

            Assert.IsNotNull(view);
            Assert.AreEqual("Pasadas", view.ViewName);
            Assert.AreEqual(views, 200);
        }

        [Test]
        public void CasoConferenciaFuturas()
        {
            var repo = new Mock<IConferenciaRepository>();
            repo.Setup(o => o.GetPonentes()).Returns(new List<Ponente>());
            repo.Setup(o => o.GetConferenciasOrderUC()).Returns(new List<Conferencia>());
            repo.Setup(o => o.GetComprasOrderFuturas(1)).Returns(new List<Comprar>());

            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User());

            var controller = new ConferenciaController(repo.Object, claim.Object);
            var view = controller.Futuras(null) as ViewResult;
            var views = view.StatusCode = 200;

            Assert.IsNotNull(view);
            Assert.AreEqual("Futuras", view.ViewName);
            Assert.AreEqual(views, 200);
        }
    }
}
