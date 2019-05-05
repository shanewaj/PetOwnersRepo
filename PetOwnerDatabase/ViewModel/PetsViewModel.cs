using PetOwnerDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetOwnerDatabase.ViewModel
{
    public class PetOwnerViewModel
    {
        public string Gender { get; set; }
        public IEnumerable<PetOwnersModel> PetOwners { get; set; }
    }

    public class GenderViewModel
    {
        public string Gender { get; set; }
        public IEnumerable<Pet> Pets { get; set; }
    }
}
