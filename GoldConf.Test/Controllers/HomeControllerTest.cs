using GoldConf.Controllers;
using GoldConf.Models;
using GoldConf.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace GoldConf.Test.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void CasoIndex()
        {
            var repository = new Mock<IHomeRepository>();
            var ponente = repository.Setup(o => o.GetPonentes()).Returns(new List<Ponente>());
            var conferencia = repository.Setup(o => o.GetConferencias()).Returns(new List<Conferencia>());

            var controller = new HomeController(repository.Object);
            var view = controller.Index() as ViewResult;

            var views = view.StatusCode = 200;

            Assert.IsNotNull(view);
            Assert.AreEqual("Index", view.ViewName);
            Assert.AreEqual(views, 200);
        }
    }
}
