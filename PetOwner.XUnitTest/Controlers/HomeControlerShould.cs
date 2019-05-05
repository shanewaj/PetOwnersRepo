using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using PetOwnerDatabase.Controllers;
using PetOwnerDatabase.Models;
using PetOwnerDatabase.ViewModel;
using Xunit;

namespace PetOwner.XUnitTest.Controlers
{
    public class HomeControlerShould
    {
        private readonly Mock<IOptions<AppSettings>> fakeConf = new Mock<IOptions<AppSettings>>();
        [Fact]
        public void ReturnViewForIndex()
        {

            HomeController sut = new HomeController(fakeConf.Object);

            IActionResult result = sut.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewForTestAPI()
        {
            
            HomeController sut = new HomeController(fakeConf.Object);

            IActionResult result = sut.TestAPI();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ReturnViewModelForTestAPI()
        {

            HomeController sut = new HomeController(fakeConf.Object);

            IActionResult result = sut.TestAPI();

            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<GenderViewModel>>(viewResult.Model);
        }
    }
}
