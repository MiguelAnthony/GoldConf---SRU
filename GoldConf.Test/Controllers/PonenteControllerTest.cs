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
    class PonenteControllerTest
    {
        [Test]
        public void CasoPonenteIndex()
        {
            var repository = new Mock<IPonenteRepository>();
            var ponente = repository.Setup(o => o.GetPonentes()).Returns(new List<Ponente>());

            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User());

            var controller = new PonenteController(repository.Object,null,claim.Object);
            var view = controller.Ponente(null) as ViewResult;

            var views = view.StatusCode = 200;

            Assert.IsNotNull(view);
            Assert.AreEqual("Ponente", view.ViewName);
            Assert.AreEqual(views, 200);
        }

        [Test]
        public void CasoPonenteIndexSearch()
        {
            var repository = new Mock<IPonenteRepository>();
            var ponente = repository.Setup(o => o.GetPonentesSearch("hola")).Returns(new List<Ponente>());

            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User());

            var controller = new PonenteController(repository.Object, null, claim.Object);
            var view = controller.Ponente("hola") as ViewResult;

            var views = view.StatusCode = 200;

            Assert.IsNotNull(view);
            Assert.AreEqual("Ponente", view.ViewName);
            Assert.AreEqual(views, 200);
        }

        [Test]
        public void CasoPonenteDetalle()
        {
            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User() { Username = "LanRhXXX" });

            var repository = new Mock<IPonenteRepository>();
            var cuenta = repository.Setup(o => o.GetCuentaUser()).Returns(new List<Cuenta>());
            var compra = repository.Setup(o => o.GetCompras()).Returns(new List<Comprar>());
            var ponente = repository.Setup(o => o.GetPonente(1)).Returns(new Ponente());
            var conferencias = repository.Setup(o => o.GetConferenciasPonente(1)).Returns(new List<Conferencia>());

            var controller = new PonenteController(repository.Object, null, claim.Object);
            var view = controller.Detalle(1) as ViewResult;

            var views = view.StatusCode = 200;

            Assert.IsNotNull(view);
            Assert.AreEqual("Detalle", view.ViewName);
            Assert.AreEqual(views, 200);
        }

        [Test]
        public void CasoPonenteRegistrarA()
        {
            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User() { Username = "LanRhXXX" });

            var repository = new Mock<IPonenteRepository>();

            var controller = new PonenteController(repository.Object, null, claim.Object);
            var view = controller.Registrar() as ViewResult;

            var views = view.StatusCode = 200;

            Assert.IsNotNull(view);
            Assert.AreEqual("Registrar", view.ViewName);
            Assert.AreEqual(views, 200);
        }

        [Test]
        public void CasoPonenteRegistrarC()
        {
            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User() { Username = "User" });

            var repository = new Mock<IPonenteRepository>();
            repository.Setup(o => o.GetPonentes()).Returns(new List<Ponente>());

            var controller = new PonenteController(repository.Object, null, claim.Object);
            var view = controller.Registrar() as RedirectToActionResult;

            Assert.IsNotNull(view);
            Assert.AreEqual("Ponente", view.ActionName);
        }

        [Test]
        public void CasoPonenteRegistrarPostA()
        {
            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User() { Username = "LanRhXXX" });

            var repository = new Mock<IPonenteRepository>();
            var ponente = repository.Setup(o => o.GetPonentes()).Returns(new List<Ponente>());
            var nPonente = repository.Setup(o => o.NewPonente(new Ponente(), null));

            var controller = new PonenteController(repository.Object, null, claim.Object);
            var view = controller.Registrar(new Ponente(),null,"","") as RedirectToActionResult;

            Assert.IsNotNull(view);
            Assert.AreEqual("Ponente", view.ActionName);
        }

        [Test]
        public void CasoPonenteRegistrarPostC()
        {
            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User() { Username = "User" });

            var repository = new Mock<IPonenteRepository>();
            var ponente = repository.Setup(o => o.GetPonentes()).Returns(new List<Ponente>());

            var controller = new PonenteController(repository.Object, null, claim.Object);
            var view = controller.Registrar(new Ponente(), null, "", "") as RedirectToActionResult;

            Assert.IsNotNull(view);
            Assert.AreEqual("Ponente", view.ActionName);
        }

        [Test]
        public void CasoPonenteEditarA()
        {
            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User() { Username = "LanRhXXX" });

            var repository = new Mock<IPonenteRepository>();
            var ponente = repository.Setup(o => o.GetPonente(1)).Returns(new Ponente());

            var controller = new PonenteController(repository.Object, null, claim.Object);
            var view = controller.Edit(1) as ViewResult;

            Assert.IsNotNull(view);
            Assert.AreEqual("Edit", view.ViewName);
        }
        [Test]
        public void CasoPonenteEditarC()
        {
            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User() { Username = "User" });

            var repository = new Mock<IPonenteRepository>();
            var ponentes = repository.Setup(o => o.GetPonentes()).Returns(new List<Ponente>());

            var controller = new PonenteController(repository.Object, null, claim.Object);
            var view = controller.Edit(1) as RedirectToActionResult;

            Assert.IsNotNull(view);
            Assert.AreEqual("Ponente", view.ActionName);
        }

        [Test]
        public void CasoPonenteEditarPostA()
        {
            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.GetLoggedUser()).Returns(new User() { Username = "LanRhXXX" });

            var repository = new Mock<IPonenteRepository>();
            repository.Setup(o => o.EditPonente(new Ponente(), null));

            var controller = new PonenteController(repository.Object, null, claim.Object);
            var view = controller.Edit(new Ponente(), null) as RedirectToActionResult;

            Assert.IsNotNull(view);
            Assert.AreEqual("Ponente", view.ActionName);
        }
    }
}
