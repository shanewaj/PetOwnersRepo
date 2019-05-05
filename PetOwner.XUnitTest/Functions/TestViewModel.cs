using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Xunit;
using PetOwnerDatabase;
using PetOwnerDatabase.Models;
using System.Linq;
using PetOwnerDatabase.Helper;

namespace PetOwner.XUnitTest.Functions
{
    public class TestViewModel
    {

        [Fact]
        public void ConvertJSONModelToViewModel()
        {
            List<PetOwnersModel> PetOwnersList = new List<PetOwnersModel>();
            List<Pet> pets = new List<Pet>();

            pets.Add(new Pet { Name = "New", Type = "cat" });

            var model = new PetOwnerDatabase.Models.PetOwnersModel
            {
                Age = 20,
                Gender = "Male",
                Name = "Test",
                Pets = pets
            };

            PetOwnersList.Add(model);

            var viewmodel = HttpClientHelper.ConvertToViewModel(PetOwnersList);

            Assert.Equal("Male", viewmodel.FirstOrDefault().Gender);
        }
    }
}
