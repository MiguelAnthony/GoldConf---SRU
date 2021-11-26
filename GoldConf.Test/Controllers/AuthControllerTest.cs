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
    class AuthControllerTest
    {
        [Test]
        public void CasoAuthLogin()
        {
            var controller = new AuthController(null,null);
            var view = controller.Login() as ViewResult;
            var views = view.StatusCode = 200;

            Assert.IsNotNull(view);
            Assert.AreEqual("Login", view.ViewName);
            Assert.AreEqual(views, 200);
        }

        [Test]
        public void CasoAuthLoginPostCorrecto()
        {
            var repo = new Mock<IAuthRepository>();
            repo.Setup(o => o.GetUsuario("Hola", "Mundo")).Returns(new User() { Username = "Hola"});

            var claim = new Mock<IClaimService>();

            var controller = new AuthController(repo.Object, claim.Object);
            var view = controller.Login("Hola", "Mundo") as RedirectToActionResult;

            Assert.AreEqual("Index", view.ActionName);
        }

        [Test]
        public void CasoAuthLoginPostError()
        {
            var repo = new Mock<IAuthRepository>();
            repo.Setup(o => o.GetUsuario(null, null)).Returns(new User() { Username = null});

            var controller = new AuthController(repo.Object,null);
            var view = controller.Login("Hola", "Mundo") as ViewResult;

            Assert.AreEqual("Login", view.ViewName);
        }

        [Test]
        public void CasoLogOut()
        {
            var claim = new Mock<IClaimService>();
            claim.Setup(o => o.Logout());

            var controller = new AuthController(null, claim.Object);
            var view = controller.Logout() as RedirectToActionResult;

            Assert.AreEqual("Login", view.ActionName);
        }
        [Test]
        public void CasoAuthRegistrar()
        {
            var controller = new AuthController(null, null);
            var view = controller.Registrar() as ViewResult;
            var views = view.StatusCode = 200;

            Assert.IsNotNull(view);
            Assert.AreEqual("Registrar", view.ViewName);
            Assert.AreEqual(views, 200);
        }

        [Test]
        public void CasoAuthRegistrarGetBien()
        {
            var repo = new Mock<IAuthRepository>();
            repo.Setup(o => o.GetUsuarios()).Returns(new List<User>());
            repo.Setup(o => o.SaveUsuario(new User()));

            var controller = new AuthController(repo.Object, null);
            var view = controller.Registrar(new User() { Password = "Hola" }, "Hola") as RedirectToActionResult;

            Assert.AreEqual("Login", view.ActionName);
        }

        [Test]
        public void CasoAuthRegistrarGetError()
        {
            var repo = new Mock<IAuthRepository>();
            repo.Setup(o => o.GetUsuarios()).Returns(new List<User>());

            var controller = new AuthController(repo.Object, null);
            var view = controller.Registrar(new User(), "Hola") as ViewResult;

            Assert.IsNotNull(view);
            Assert.AreEqual("Registrar", view.ViewName);
        }
    }
}
